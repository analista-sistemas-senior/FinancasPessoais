using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class ReceitaTipoService(IReceitaTipoRepository receitaTipoRepository) : IReceitaTipoService
    {
        private readonly IReceitaTipoRepository _receitaTipoRepository = receitaTipoRepository;

        public async Task<ReceitaTipoDTO?> RetornarReceitaTipoPorId(int idReceitaTipo)
        {
            var receitaTipo = await _receitaTipoRepository.RetornarReceitaTipoPorId(idReceitaTipo);
            return receitaTipo?.ParaDTO();
        }

        public async Task<ReceitaTipoDTO?> RetornarReceitaTipoPorIdEIdUsuario(int idReceitaTipo, int idUsuario)
        {
            var receitaTipo = await _receitaTipoRepository.RetornarReceitaTipoPorIdEIdUsuario(idReceitaTipo, idUsuario);
            return receitaTipo?.ParaDTO();
        }

        public async Task<List<ReceitaTipoDTO>> RetornarReceitasTiposPorIdUsuario(int idUsuario)
        {
            var receitaTipos = await _receitaTipoRepository.RetornarReceitasTiposPorIdUsuario(idUsuario);
            return receitaTipos.ParaDTOs();
        }

        public async Task<Resultado<ReceitaTipoDTO>> CadastrarReceitaTipo(ReceitaTipoDTO receitaTipo)
        {
            var receitaTipoNova = await _receitaTipoRepository.CadastrarReceitaTipo(receitaTipo.ParaEntidade());
            return Resultado<ReceitaTipoDTO>.Ok(receitaTipoNova.ParaDTO());
        }

        public async Task<Resultado<ReceitaTipoDTO>> AtualizarReceitaTipo(ReceitaTipoDTO receitaTipo)
        {
            try
            {
                var receitaTipoAtualizada = await _receitaTipoRepository.AtualizarReceitaTipo(receitaTipo.ParaEntidade());
                return Resultado<ReceitaTipoDTO>.Ok(receitaTipoAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ReceitaTipoDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirReceitaTipo(int idReceitaTipo, int idUsuario)
        {
            return await _receitaTipoRepository.ExcluirReceitaTipo(idReceitaTipo, idUsuario);
        }

        public async Task<Resultado<ReceitaTipoDTO>> RetornarReceitaTipoAutentico(int idReceitaTipo, int idUsuario)
        {
            var receitaTipoExistente = await RetornarReceitaTipoPorIdEIdUsuario(idReceitaTipo, idUsuario);
            if (receitaTipoExistente == null) return Resultado<ReceitaTipoDTO>.Falha("Tipo de receita não encontrado");

            return Resultado<ReceitaTipoDTO>.Ok(receitaTipoExistente);
        }
    }
}