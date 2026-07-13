using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IIndiceFinanceiroRepository
    {
        Task<IndiceFinanceiro?> RetornarIndiceFinanceiroPorId(int idIndiceFinanceiro);
        Task<IndiceFinanceiro?> RetornarIndiceFinanceiroPorIdEIdUsuario(int idIndiceFinanceiro, int idUsuario);
        Task<List<IndiceFinanceiro>> RetornarIndicesFinanceirosPorIdUsuario(int idUsuario);
        Task<IndiceFinanceiro> CadastrarIndiceFinanceiro(IndiceFinanceiro indiceFinanceiro);
        Task<IndiceFinanceiro> AtualizarIndiceFinanceiro(IndiceFinanceiro indiceFinanceiro);
        Task<bool> ExcluirIndiceFinanceiro(int idIndiceFinanceiro, int idUsuario);
    }
}