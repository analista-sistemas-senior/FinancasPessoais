using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class ReceitaService(IReceitaRepository receitaRepository) : IReceitaService
    {
        private readonly IReceitaRepository _receitaRepository = receitaRepository;

        public async Task<ReceitaDTO?> RetornarReceitaPorId(int idReceita)
        {
            var receita = await _receitaRepository.RetornarReceitaPorId(idReceita);
            return receita?.ParaDTO();
        }

        public async Task<ReceitaDTO?> RetornarReceitaPorIdEIdUsuario(int idReceita, int idUsuario)
        {
            var receita = await _receitaRepository.RetornarReceitaPorIdEIdUsuario(idReceita, idUsuario);
            return receita?.ParaDTO();
        }

        public async Task<List<ReceitaDTO>> RetornarReceitasPorIdUsuario(int idUsuario)
        {
            var receitas = await _receitaRepository.RetornarReceitasPorIdUsuario(idUsuario);
            return receitas.ParaDTOs();
        }

        public async Task<Resultado<ReceitaDTO>> CadastrarReceita(ReceitaDTO receita)
        {
            var receitaNova = await _receitaRepository.CadastrarReceita(receita.PraEntidade());
            return Resultado<ReceitaDTO>.Ok(receitaNova.ParaDTO());
        }

        public async Task<Resultado<ReceitaDTO>> AtualizarReceita(ReceitaDTO receita)
        {
            try
            {
                var receitaAtualizada = await _receitaRepository.AtualizarReceita(receita.PraEntidade());
                return Resultado<ReceitaDTO>.Ok(receitaAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ReceitaDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirReceita(int idReceita, int idUsuario)
        {
            return await _receitaRepository.ExcluirReceita(idReceita, idUsuario);
        }

        public async Task<Resultado<ReceitaDTO>> RetornarReceitaAutentica(int idReceita, int idUsuario)
        {
            var receitaExistente = await RetornarReceitaPorIdEIdUsuario(idReceita, idUsuario);
            if (receitaExistente == null) return Resultado<ReceitaDTO>.Falha("Receita não encontrada");

            return Resultado<ReceitaDTO>.Ok(receitaExistente);
        }
    }
}