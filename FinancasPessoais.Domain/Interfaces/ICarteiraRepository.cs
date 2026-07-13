using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface ICarteiraRepository
    {
        Task<Carteira?> RetornarCarteiraPorId(int idCarteira);
        Task<Carteira?> RetornarCarteiraPorIdEIdUsuario(int idCarteira, int idUsuario);
        Task<List<Carteira>> RetornarCarteirasPorIdUsuario(int idUsuario);
        Task<Carteira> CadastrarCarteira(Carteira carteira);
        Task<Carteira> AtualizarCarteira(Carteira carteira);
        Task<bool> ExcluirCarteira(int idCarteira, int idUsuario);
    }
}