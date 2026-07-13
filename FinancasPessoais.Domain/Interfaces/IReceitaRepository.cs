using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IReceitaRepository
    {
        Task<Receita?> RetornarReceitaPorId(int idReceita);
        Task<Receita?> RetornarReceitaPorIdEIdUsuario(int idReceita, int idUsuario);
        Task<List<Receita>> RetornarReceitasPorIdUsuario(int idUsuario);
        Task<Receita> CadastrarReceita(Receita receita);
        Task<Receita> AtualizarReceita(Receita receita);
        Task<bool> ExcluirReceita(int idReceita, int idUsuario);
    }
}