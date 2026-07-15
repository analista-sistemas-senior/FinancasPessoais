using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class InvestimentoHistoricoService(IInvestimentoHistoricoRepository investimentoHistoricoRepository, IInvestimentoRepository investimentoRepository) : IInvestimentoHistoricoService
    {
        private readonly IInvestimentoHistoricoRepository _investimentoHistoricoRepository = investimentoHistoricoRepository;
        private readonly IInvestimentoRepository _investimentoRepository = investimentoRepository;

        public async Task<InvestimentoHistoricoDTO?> RetornarInvestimentoHistoricoPorId(int idInvestimentoHistorico)
        {
            var investimentoHistorico = await _investimentoHistoricoRepository.RetornarInvestimentoHistoricoPorId(idInvestimentoHistorico);
            return investimentoHistorico?.ParaDTO();
        }

        public async Task<InvestimentoHistoricoDTO?> RetornarInvestimentoHistoricoPorIdEIdUsuario(int idInvestimentoHistorico, int idUsuario)
        {
            var investimentoHistorico = await _investimentoHistoricoRepository.RetornarInvestimentoHistoricoPorIdEIdUsuario(idInvestimentoHistorico, idUsuario);
            return investimentoHistorico?.ParaDTO();
        }

        public async Task<List<InvestimentoHistoricoDTO>> RetornarInvestimentosHistoricosPorIdUsuario(int idUsuario)
        {
            var investimentosHistoricos = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdUsuario(idUsuario);
            return investimentosHistoricos.ParaDTOs();
        }

        public async Task<Resultado<InvestimentoHistoricoDTO>> CadastrarInvestimentoHistorico(InvestimentoHistoricoDTO investimentoHistorico)
        {
            var investimentoHistoricoNovo = await _investimentoHistoricoRepository.CadastrarInvestimentoHistorico(investimentoHistorico.ParaEntidade());
            
            await AtualizarInvestimentoSaldo(investimentoHistoricoNovo.IDInvestimento);

            return Resultado<InvestimentoHistoricoDTO>.Ok(investimentoHistoricoNovo.ParaDTO());
        }

        public async Task<Resultado<InvestimentoHistoricoDTO>> AtualizarInvestimentoHistorico(InvestimentoHistoricoDTO investimentoHistorico)
        {
            try
            {
                var investimentoHistoricoAtualizado = await _investimentoHistoricoRepository.AtualizarInvestimentoHistorico(investimentoHistorico.ParaEntidade());

                await AtualizarInvestimentoSaldo(investimentoHistorico.IDInvestimento);

                return Resultado<InvestimentoHistoricoDTO>.Ok(investimentoHistoricoAtualizado.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<InvestimentoHistoricoDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirInvestimentoHistorico(int idInvestimentoHistorico, int idInvestimento, int idUsuario)
        {
            var investimentoHistoricoApagado = await _investimentoHistoricoRepository.ExcluirInvestimentoHistorico(idInvestimentoHistorico, idUsuario);
            if (investimentoHistoricoApagado == true) await AtualizarInvestimentoSaldo(idInvestimento);

            return investimentoHistoricoApagado;
        }

        public async Task<Resultado<InvestimentoHistoricoDTO>> RetornarInvestimentoHistoricoAutentico(int idInvestimentoHistorico, int idUsuario)
        {
            var investimentoHistoricoExistente = await RetornarInvestimentoHistoricoPorIdEIdUsuario(idInvestimentoHistorico, idUsuario);
            if (investimentoHistoricoExistente == null) return Resultado<InvestimentoHistoricoDTO>.Falha("Histórico não encontrado");

            return Resultado<InvestimentoHistoricoDTO>.Ok(investimentoHistoricoExistente);
        }

        public async Task<List<InvestimentoHistoricoDTO>> RetornarInvestimentosHistoricosPorIdInvestimento(int idInvestimento)
        {
            var investimentosHistoricos = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdInvestimento(idInvestimento);
            return investimentosHistoricos.ParaDTOs();
        }

        public async Task AtualizarInvestimentoSaldo(int idInvestimento)
        {
            var investimentoHistorico = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdInvestimento(idInvestimento);
            if (investimentoHistorico == null) return;

            var primeiroHistorico = investimentoHistorico.FirstOrDefault();
            if (primeiroHistorico == null) return;

            decimal saldo = primeiroHistorico.Investimento.VLInvestimento;

            var investimentoOrdenado = investimentoHistorico.OrderBy(ih => ih.DTInvestimentoHistorico);

            foreach (var itemInvestimentoHistorico in investimentoOrdenado)
            {
                if (itemInvestimentoHistorico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saldo)
                {
                    saldo = itemInvestimentoHistorico.VLInvestimentoHistorico;
                }
                else
                {
                    saldo += itemInvestimentoHistorico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque ? -1 * itemInvestimentoHistorico.VLInvestimentoHistorico : itemInvestimentoHistorico.VLInvestimentoHistorico;
                }
            }

            var investimento = await _investimentoRepository.RetornarInvestimentoPorId(idInvestimento);
            if (investimento == null) return;

            investimento.AtualizarSaldo(saldo);

            await _investimentoRepository.AtualizarInvestimento(investimento);
        }
    }
}