using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class InvestimentoService(IInvestimentoRepository investimentoRepository) : IInvestimentoService
    {
        private readonly IInvestimentoRepository _investimentoRepository = investimentoRepository;

        public async Task<InvestimentoDTO?> RetornarInvestimentoPorId(int idInvestimento)
        {
            var investimento = await _investimentoRepository.RetornarInvestimentoPorId(idInvestimento);
            return investimento?.ParaDTO();
        }

        public async Task<InvestimentoDTO?> RetornarInvestimentoPorIdEIdUsuario(int idInvestimento, int idUsuario)
        {
            var investimento = await _investimentoRepository.RetornarInvestimentoPorIdEIdUsuario(idInvestimento, idUsuario);
            return investimento?.ParaDTO();
        }

        public async Task<List<InvestimentoDTO>> RetornarInvestimentosPorIdUsuario(int idUsuario)
        {
            var investimentos = await _investimentoRepository.RetornarInvestimentosPorIdUsuario(idUsuario);
            return investimentos.ParaDTOs();
        }

        public async Task<Resultado<InvestimentoDTO>> CadastrarInvestimento(InvestimentoDTO investimento)
        {
            var investimentoNova = await _investimentoRepository.CadastrarInvestimento(investimento.ParaEntidade());
            return Resultado<InvestimentoDTO>.Ok(investimentoNova.ParaDTO());
        }

        public async Task<Resultado<InvestimentoDTO>> AtualizarInvestimento(InvestimentoDTO investimento)
        {
            try
            {
                var investimentoAtualizada = await _investimentoRepository.AtualizarInvestimento(investimento.ParaEntidade());
                return Resultado<InvestimentoDTO>.Ok(investimentoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<InvestimentoDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirInvestimento(int idInvestimento, int idUsuario)
        {
            return await _investimentoRepository.ExcluirInvestimento(idInvestimento, idUsuario);
        }

        public async Task<Resultado<InvestimentoDTO>> RetornarInvestimentoAutentico(int idInvestimento, int idUsuario)
        {
            var investimentoExistente = await RetornarInvestimentoPorIdEIdUsuario(idInvestimento, idUsuario);
            if (investimentoExistente == null) return Resultado<InvestimentoDTO>.Falha("Investimento não encontrado");

            return Resultado<InvestimentoDTO>.Ok(investimentoExistente);
        }
    }
}