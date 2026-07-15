using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class DespesaService(IDespesaRepository despesaRepository) : IDespesaService
    {
        private readonly IDespesaRepository _despesaRepository = despesaRepository;

        public async Task<DespesaDTO?> RetornarDespesaPorId(int idDespesa)
        {
            var despesa = await _despesaRepository.RetornarDespesaPorId(idDespesa);
            return despesa?.ParaDTO();
        }

        public async Task<DespesaDTO?> RetornarDespesaPorIdEIdUsuario(int idDespesa, int idUsuario)
        {
            var despesa = await _despesaRepository.RetornarDespesaPorIdEIdUsuario(idDespesa, idUsuario);
            return despesa?.ParaDTO();
        }

        public async Task<List<DespesaDTO>> RetornarDespesasPorIdUsuario(int idUsuario)
        {
            var despesas = await _despesaRepository.RetornarDespesasPorIdUsuario(idUsuario);
            return despesas.ParaDTOs();
        }

        public async Task<Resultado<DespesaDTO>> CadastrarDespesa(DespesaDTO despesa)
        {
            var despesaNova = await _despesaRepository.CadastrarDespesa(despesa.ParaEntidade());
            return Resultado<DespesaDTO>.Ok(despesaNova.ParaDTO());
        }

        public async Task<Resultado<DespesaDTO>> AtualizarDespesa(DespesaDTO despesa)
        {
            try
            {
                var despesaAtualizada = await _despesaRepository.AtualizarDespesa(despesa.ParaEntidade());
                return Resultado<DespesaDTO>.Ok(despesaAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<DespesaDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirDespesa(int idDespesa, int idUsuario)
        {
            return await _despesaRepository.ExcluirDespesa(idDespesa, idUsuario);
        }

        public async Task<Resultado<DespesaDTO>> RetornarDespesaAutentica(int idDespesa, int idUsuario)
        {
            var despesaExistente = await RetornarDespesaPorIdEIdUsuario(idDespesa, idUsuario);
            if (despesaExistente == null) return Resultado<DespesaDTO>.Falha("Despesa não encontrada");

            return Resultado<DespesaDTO>.Ok(despesaExistente);
        }
    }
}