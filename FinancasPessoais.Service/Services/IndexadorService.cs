using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class IndexadorService(IIndexadorRepository indexadorRepository) : IIndexadorService
    {
        private readonly IIndexadorRepository _indexadorRepository = indexadorRepository;

        public async Task<IndexadorDTO?> RetornarIndexadorPorId(int idIndexador)
        {
            var indexador = await _indexadorRepository.RetornarIndexadorPorId(idIndexador);
            return indexador?.ParaDTO();
        }

        public async Task<IndexadorDTO?> RetornarIndexadorPorIdEIdUsuario(int idIndexador, int idUsuario)
        {
            var indexador = await _indexadorRepository.RetornarIndexadorPorIdEIdUsuario(idIndexador, idUsuario);
            return indexador?.ParaDTO();
        }

        public async Task<List<IndexadorDTO>> RetornarIndexadoresPorIdUsuario(int idUsuario)
        {
            var indexadors = await _indexadorRepository.RetornarIndexadoresPorIdUsuario(idUsuario);
            return indexadors.ParaDTOs();
        }

        public async Task<Resultado<IndexadorDTO>> CadastrarIndexador(IndexadorDTO indexador)
        {
            var indexadorNova = await _indexadorRepository.CadastrarIndexador(indexador.PraEntidade());
            return Resultado<IndexadorDTO>.Ok(indexadorNova.ParaDTO());
        }

        public async Task<Resultado<IndexadorDTO>> AtualizarIndexador(IndexadorDTO indexador)
        {
            try
            {
                var indexadorAtualizada = await _indexadorRepository.AtualizarIndexador(indexador.PraEntidade());
                return Resultado<IndexadorDTO>.Ok(indexadorAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<IndexadorDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirIndexador(int idIndexador, int idUsuario)
        {
            return await _indexadorRepository.ExcluirIndexador(idIndexador, idUsuario);
        }

        public async Task<Resultado<IndexadorDTO>> RetornarIndexadorAutentico(int idIndexador, int idUsuario)
        {
            var indexadorExistente = await RetornarIndexadorPorIdEIdUsuario(idIndexador, idUsuario);
            if (indexadorExistente == null) return Resultado<IndexadorDTO>.Falha("Indexador não encontrado");

            return Resultado<IndexadorDTO>.Ok(indexadorExistente);
        }
    }
}