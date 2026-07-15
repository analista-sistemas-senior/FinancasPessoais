using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class DespesaTipoService(IDespesaTipoRepository despesaTipoRepository) : IDespesaTipoService
    {
        private readonly IDespesaTipoRepository _despesaTipoRepository = despesaTipoRepository;

        public async Task<DespesaTipoDTO?> RetornarDespesaTipoPorId(int idDespesaTipo)
        {
            var despesaTipo = await _despesaTipoRepository.RetornarDespesaTipoPorId(idDespesaTipo);
            return despesaTipo?.ParaDTO();
        }

        public async Task<DespesaTipoDTO?> RetornarDespesaTipoPorIdEIdUsuario(int idDespesaTipo, int idUsuario)
        {
            var despesaTipo = await _despesaTipoRepository.RetornarDespesaTipoPorIdEIdUsuario(idDespesaTipo, idUsuario);
            return despesaTipo?.ParaDTO();
        }

        public async Task<List<DespesaTipoDTO>> RetornarDespesasTiposPorIdUsuario(int idUsuario)
        {
            var despesaTipos = await _despesaTipoRepository.RetornarDespesasTiposPorIdUsuario(idUsuario);
            return despesaTipos.ParaDTOs();
        }

        public async Task<Resultado<DespesaTipoDTO>> CadastrarDespesaTipo(DespesaTipoDTO despesaTipo)
        {
            var despesaTipoNova = await _despesaTipoRepository.CadastrarDespesaTipo(despesaTipo.ParaEntidade());
            return Resultado<DespesaTipoDTO>.Ok(despesaTipoNova.ParaDTO());
        }

        public async Task<Resultado<DespesaTipoDTO>> AtualizarDespesaTipo(DespesaTipoDTO despesaTipo)
        {
            try
            {
                var despesaTipoAtualizada = await _despesaTipoRepository.AtualizarDespesaTipo(despesaTipo.ParaEntidade());
                return Resultado<DespesaTipoDTO>.Ok(despesaTipoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<DespesaTipoDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirDespesaTipo(int idDespesaTipo, int idUsuario)
        {
            return await _despesaTipoRepository.ExcluirDespesaTipo(idDespesaTipo, idUsuario);
        }

        public async Task<Resultado<DespesaTipoDTO>> RetornarDespesaTipoAutentica(int idDespesaTipo, int idUsuario)
        {
            var despesaTipoExistente = await RetornarDespesaTipoPorIdEIdUsuario(idDespesaTipo, idUsuario);
            if (despesaTipoExistente == null) return Resultado<DespesaTipoDTO>.Falha("Tipo de despesa não encontrado");

            return Resultado<DespesaTipoDTO>.Ok(despesaTipoExistente);
        }
    }
}