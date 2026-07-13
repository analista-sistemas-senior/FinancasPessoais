using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class CarteiraService(ICarteiraRepository carteiraRepository) : ICarteiraService
    {
        private readonly ICarteiraRepository _carteiraRepository = carteiraRepository;

        public async Task<CarteiraDTO?> RetornarCarteiraPorId(int idCarteira)
        {
            var carteira = await _carteiraRepository.RetornarCarteiraPorId(idCarteira);
            return carteira?.ParaDTO();
        }

        public async Task<CarteiraDTO?> RetornarCarteiraPorIdEIdUsuario(int idCarteira, int idUsuario)
        {
            var carteira = await _carteiraRepository.RetornarCarteiraPorIdEIdUsuario(idCarteira, idUsuario);
            return carteira?.ParaDTO();
        }

        public async Task<List<CarteiraDTO>> RetornarCarteirasPorIdUsuario(int idUsuario)
        {
            var carteiras = await _carteiraRepository.RetornarCarteirasPorIdUsuario(idUsuario);
            return carteiras.ParaDTOs();
        }

        public async Task<Resultado<CarteiraDTO>> CadastrarCarteira(CarteiraDTO carteira)
        {
            var carteiraNova = await _carteiraRepository.CadastrarCarteira(carteira.PraEntidade());
            return Resultado<CarteiraDTO>.Ok(carteiraNova.ParaDTO());
        }

        public async Task<Resultado<CarteiraDTO>> AtualizarCarteira(CarteiraDTO carteira)
        {
            try {
                var carteiraAtualizada = await _carteiraRepository.AtualizarCarteira(carteira.PraEntidade());
                return Resultado<CarteiraDTO>.Ok(carteiraAtualizada.ParaDTO());
            }  catch (DbUpdateConcurrencyException) { return Resultado<CarteiraDTO>.Falha("Falhou"); }
        }

        public async Task<bool> ExcluirCarteira(int idCarteira, int idUsuario)
        {
            return await _carteiraRepository.ExcluirCarteira(idCarteira, idUsuario);
        }

        public async Task<Resultado<CarteiraDTO>> RetornarCarteiraAutentica(int idCarteira, int idUsuario)
        {
            var carteiraExistente = await RetornarCarteiraPorIdEIdUsuario(idCarteira, idUsuario);
            if (carteiraExistente == null) return Resultado<CarteiraDTO>.Falha("Carteira não encontrada");

            return Resultado<CarteiraDTO>.Ok(carteiraExistente);
        }
    }
}