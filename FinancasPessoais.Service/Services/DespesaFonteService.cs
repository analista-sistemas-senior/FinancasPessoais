using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class DespesaFonteService(IDespesaFonteRepository despesaFonteRepository) : IDespesaFonteService
    {
        private readonly IDespesaFonteRepository _despesaFonteRepository = despesaFonteRepository;

        public async Task<DespesaFonteDTO?> RetornarDespesaFontePorId(int idDespesaFonte)
        {
            var despesaFonte = await _despesaFonteRepository.RetornarDespesaFontePorId(idDespesaFonte);
            return despesaFonte?.ParaDTO();
        }

        public async Task<DespesaFonteDTO?> RetornarDespesaFontePorIdEIdUsuario(int idDespesaFonte, int idUsuario)
        {
            var despesaFonte = await _despesaFonteRepository.RetornarDespesaFontePorIdEIdUsuario(idDespesaFonte, idUsuario);
            return despesaFonte?.ParaDTO();
        }

        public async Task<List<DespesaFonteDTO>> RetornarDespesasFontesPorIdUsuario(int idUsuario)
        {
            var despesasFontes = await _despesaFonteRepository.RetornarDespesasFontesPorIdUsuario(idUsuario);
            return despesasFontes.ParaDTOs();
        }

        public async Task<Resultado<DespesaFonteDTO>> CadastrarDespesaFonte(DespesaFonteDTO despesaFonte)
        {
            var despesaFonteNova = await _despesaFonteRepository.CadastrarDespesaFonte(despesaFonte.ParaEntidade());
            return Resultado<DespesaFonteDTO>.Ok(despesaFonteNova.ParaDTO());
        }

        public async Task<Resultado<DespesaFonteDTO>> AtualizarDespesaFonte(DespesaFonteDTO despesaFonte)
        {
            try
            {
                var despesaFonteAtualizada = await _despesaFonteRepository.AtualizarDespesaFonte(despesaFonte.ParaEntidade());
                return Resultado<DespesaFonteDTO>.Ok(despesaFonteAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<DespesaFonteDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirDespesaFonte(int idDespesaFonte, int idUsuario)
        {
            return await _despesaFonteRepository.ExcluirDespesaFonte(idDespesaFonte, idUsuario);
        }

        public async Task<Resultado<DespesaFonteDTO>> RetornarDespesaFonteAutentica(int idDespesaFonte, int idUsuario)
        {
            var despesaFonteExistente = await RetornarDespesaFontePorIdEIdUsuario(idDespesaFonte, idUsuario);
            if (despesaFonteExistente == null) return Resultado<DespesaFonteDTO>.Falha("Fonte de despesa não encontrada");
            
            return Resultado<DespesaFonteDTO>.Ok(despesaFonteExistente);
        }
    }
}