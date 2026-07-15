using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class ReceitaFonteService(IReceitaFonteRepository receitaFonteRepository) : IReceitaFonteService
    {
        private readonly IReceitaFonteRepository _receitaFonteRepository = receitaFonteRepository;

        public async Task<ReceitaFonteDTO?> RetornarReceitaFontePorId(int idReceitaFonte)
        {
            var receitaFonte = await _receitaFonteRepository.RetornarReceitaFontePorId(idReceitaFonte);
            return receitaFonte?.ParaDTO();
        }

        public async Task<ReceitaFonteDTO?> RetornarReceitaFontePorIdEIdUsuario(int idReceitaFonte, int idUsuario)
        {
            var receitaFonte = await _receitaFonteRepository.RetornarReceitaFontePorIdEIdUsuario(idReceitaFonte, idUsuario);
            return receitaFonte?.ParaDTO();
        }

        public async Task<List<ReceitaFonteDTO>> RetornarReceitasFontesPorIdUsuario(int idUsuario)
        {
            var receitaFontes = await _receitaFonteRepository.RetornarReceitasFontesPorIdUsuario(idUsuario);
            return receitaFontes.ParaDTOs();
        }

        public async Task<Resultado<ReceitaFonteDTO>> CadastrarReceitaFonte(ReceitaFonteDTO receitaFonte)
        {
            var receitaFonteNova = await _receitaFonteRepository.CadastrarReceitaFonte(receitaFonte.ParaEntidade());
            return Resultado<ReceitaFonteDTO>.Ok(receitaFonteNova.ParaDTO());
        }

        public async Task<Resultado<ReceitaFonteDTO>> AtualizarReceitaFonte(ReceitaFonteDTO receitaFonte)
        {
            try
            {
                var receitaFonteAtualizada = await _receitaFonteRepository.AtualizarReceitaFonte(receitaFonte.ParaEntidade());
                return Resultado<ReceitaFonteDTO>.Ok(receitaFonteAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<ReceitaFonteDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirReceitaFonte(int idReceitaFonte, int idUsuario)
        {
            return await _receitaFonteRepository.ExcluirReceitaFonte(idReceitaFonte, idUsuario);
        }

        public async Task<Resultado<ReceitaFonteDTO>> RetornarReceitaFonteAutentica(int idReceitaFonte, int idUsuario)
        {
            var receitaFonteExistente = await RetornarReceitaFontePorIdEIdUsuario(idReceitaFonte, idUsuario);
            if (receitaFonteExistente == null) return Resultado<ReceitaFonteDTO>.Falha("ReceitaFonte não encontrada");

            return Resultado<ReceitaFonteDTO>.Ok(receitaFonteExistente);
        }
    }
}