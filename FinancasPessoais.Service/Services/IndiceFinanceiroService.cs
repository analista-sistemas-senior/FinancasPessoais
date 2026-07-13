using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class IndiceFinanceiroService(IIndiceFinanceiroRepository indiceFinanceiroRepository) : IIndiceFinanceiroService
    {
        private readonly IIndiceFinanceiroRepository _indiceFinanceiroRepository = indiceFinanceiroRepository;

        public async Task<IndiceFinanceiroDTO?> RetornarIndiceFinanceiroPorId(int idIndiceFinanceiro)
        {
            var indiceFinanceiro = await _indiceFinanceiroRepository.RetornarIndiceFinanceiroPorId(idIndiceFinanceiro);
            return indiceFinanceiro?.ParaDTO();
        }

        public async Task<IndiceFinanceiroDTO?> RetornarIndiceFinanceiroPorIdEIdUsuario(int idIndiceFinanceiro, int idUsuario)
        {
            var indiceFinanceiro = await _indiceFinanceiroRepository.RetornarIndiceFinanceiroPorIdEIdUsuario(idIndiceFinanceiro, idUsuario);
            return indiceFinanceiro?.ParaDTO();
        }

        public async Task<List<IndiceFinanceiroDTO>> RetornarIndicesFinanceirosPorIdUsuario(int idUsuario)
        {
            var indiceFinanceiros = await _indiceFinanceiroRepository.RetornarIndicesFinanceirosPorIdUsuario(idUsuario);
            return indiceFinanceiros.ParaDTOs();
        }

        public async Task<Resultado<IndiceFinanceiroDTO>> CadastrarIndiceFinanceiro(IndiceFinanceiroDTO indiceFinanceiro)
        {
            var indiceFinanceiroNova = await _indiceFinanceiroRepository.CadastrarIndiceFinanceiro(indiceFinanceiro.PraEntidade());
            return Resultado<IndiceFinanceiroDTO>.Ok(indiceFinanceiroNova.ParaDTO());
        }

        public async Task<Resultado<IndiceFinanceiroDTO>> AtualizarIndiceFinanceiro(IndiceFinanceiroDTO indiceFinanceiro)
        {
            try
            {
                var indiceFinanceiroAtualizada = await _indiceFinanceiroRepository.AtualizarIndiceFinanceiro(indiceFinanceiro.PraEntidade());
                return Resultado<IndiceFinanceiroDTO>.Ok(indiceFinanceiroAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<IndiceFinanceiroDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirIndiceFinanceiro(int idIndiceFinanceiro, int idUsuario)
        {
            return await _indiceFinanceiroRepository.ExcluirIndiceFinanceiro(idIndiceFinanceiro, idUsuario);
        }

        public async Task<Resultado<IndiceFinanceiroDTO>> RetornarIndiceFinanceiroAutentico(int idIndiceFinanceiro, int idUsuario)
        {
            var indiceFinanceiroExistente = await RetornarIndiceFinanceiroPorIdEIdUsuario(idIndiceFinanceiro, idUsuario);
            if (indiceFinanceiroExistente == null) return Resultado<IndiceFinanceiroDTO>.Falha("Índice financeiro não encontrado");

            return Resultado<IndiceFinanceiroDTO>.Ok(indiceFinanceiroExistente);
        }
    }
}