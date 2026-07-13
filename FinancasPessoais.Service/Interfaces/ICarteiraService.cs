using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface ICarteiraService
    {
        Task<CarteiraDTO?> RetornarCarteiraPorId(int idCarteira);
        Task<CarteiraDTO?> RetornarCarteiraPorIdEIdUsuario(int idCarteira, int idUsuario);
        Task<List<CarteiraDTO>> RetornarCarteirasPorIdUsuario(int idUsuario);
        Task<Resultado<CarteiraDTO>> CadastrarCarteira(CarteiraDTO carteira);
        Task<Resultado<CarteiraDTO>> AtualizarCarteira(CarteiraDTO carteira);
        Task<bool> ExcluirCarteira(int idCarteira, int idUsuario);
        Task<Resultado<CarteiraDTO>> RetornarCarteiraAutentica(int idCarteira, int idUsuario);
    }
}