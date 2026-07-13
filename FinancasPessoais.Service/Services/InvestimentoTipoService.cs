using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class InvestimentoTipoService(IInvestimentoTipoRepository investimentoTipoRepository) : IInvestimentoTipoService
    {
        private readonly IInvestimentoTipoRepository _investimentoTipoRepository = investimentoTipoRepository;

        public async Task<InvestimentoTipoDTO?> RetornarInvestimentoTipoPorId(int idInvestimentoTipo)
        {
            var investimentoTipo = await _investimentoTipoRepository.RetornarInvestimentoTipoPorId(idInvestimentoTipo);
            return investimentoTipo?.ParaDTO();
        }

        public async Task<InvestimentoTipoDTO?> RetornarInvestimentoTipoPorIdEIdUsuario(int idInvestimentoTipo, int idUsuario)
        {
            var investimentoTipo = await _investimentoTipoRepository.RetornarInvestimentoTipoPorIdEIdUsuario(idInvestimentoTipo, idUsuario);
            return investimentoTipo?.ParaDTO();
        }

        public async Task<List<InvestimentoTipoDTO>> RetornarInvestimentosTiposPorIdUsuario(int idUsuario)
        {
            var investimentoTipos = await _investimentoTipoRepository.RetornarInvestimentosTiposPorIdUsuario(idUsuario);
            return investimentoTipos.ParaDTOs();
        }

        public async Task<Resultado<InvestimentoTipoDTO>> CadastrarInvestimentoTipo(InvestimentoTipoDTO investimentoTipo)
        {
            var investimentoTipoNova = await _investimentoTipoRepository.CadastrarInvestimentoTipo(investimentoTipo.PraEntidade());
            return Resultado<InvestimentoTipoDTO>.Ok(investimentoTipoNova.ParaDTO());
        }

        public async Task<Resultado<InvestimentoTipoDTO>> AtualizarInvestimentoTipo(InvestimentoTipoDTO investimentoTipo)
        {
            try
            {
                var investimentoTipoAtualizada = await _investimentoTipoRepository.AtualizarInvestimentoTipo(investimentoTipo.PraEntidade());
                return Resultado<InvestimentoTipoDTO>.Ok(investimentoTipoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<InvestimentoTipoDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirInvestimentoTipo(int idInvestimentoTipo, int idUsuario)
        {
            return await _investimentoTipoRepository.ExcluirInvestimentoTipo(idInvestimentoTipo, idUsuario);
        }

        public async Task<Resultado<InvestimentoTipoDTO>> RetornarInvestimentoTipoAutentico(int idInvestimentoTipo, int idUsuario)
        {
            var investimentoTipoExistente = await RetornarInvestimentoTipoPorIdEIdUsuario(idInvestimentoTipo, idUsuario);
            if (investimentoTipoExistente == null) return Resultado<InvestimentoTipoDTO>.Falha("Tipo de investimento não encontrado");

            return Resultado<InvestimentoTipoDTO>.Ok(investimentoTipoExistente);
        }
    }
}