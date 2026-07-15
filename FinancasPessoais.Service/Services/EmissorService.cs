using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class EmissorService(IEmissorRepository emissorRepository) : IEmissorService
    {
        private readonly IEmissorRepository _emissorRepository = emissorRepository;

        public async Task<EmissorDTO?> RetornarEmissorPorId(int idEmissor)
        {
            var emissor = await _emissorRepository.RetornarEmissorPorId(idEmissor);
            return emissor?.ParaDTO();
        }

        public async Task<EmissorDTO?> RetornarEmissorPorIdEIdUsuario(int idEmissor, int idUsuario)
        {
            var emissor = await _emissorRepository.RetornarEmissorPorIdEIdUsuario(idEmissor, idUsuario);
            return emissor?.ParaDTO();
        }

        public async Task<List<EmissorDTO>> RetornarEmissoresPorIdUsuario(int idUsuario)
        {
            var emissors = await _emissorRepository.RetornarEmissoresPorIdUsuario(idUsuario);
            return emissors.ParaDTOs();
        }

        public async Task<Resultado<EmissorDTO>> CadastrarEmissor(EmissorDTO emissor)
        {
            var emissorNova = await _emissorRepository.CadastrarEmissor(emissor.ParaEntidade());
            return Resultado<EmissorDTO>.Ok(emissorNova.ParaDTO());
        }

        public async Task<Resultado<EmissorDTO>> AtualizarEmissor(EmissorDTO emissor)
        {
            try
            {
                var emissorAtualizada = await _emissorRepository.AtualizarEmissor(emissor.ParaEntidade());
                return Resultado<EmissorDTO>.Ok(emissorAtualizada.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<EmissorDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirEmissor(int idEmissor, int idUsuario)
        {
            return await _emissorRepository.ExcluirEmissor(idEmissor, idUsuario);
        }

        public async Task<Resultado<EmissorDTO>> RetornarEmissorAutentico(int idEmissor, int idUsuario)
        {
            var emissorExistente = await RetornarEmissorPorIdEIdUsuario(idEmissor, idUsuario);
            if (emissorExistente == null) return Resultado<EmissorDTO>.Falha("Emissor não encontrado");

            return Resultado<EmissorDTO>.Ok(emissorExistente);
        }
    }
}