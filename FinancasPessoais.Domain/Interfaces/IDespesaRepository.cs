using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IDespesaRepository
    {
        Task<Despesa?> RetornarDespesaPorId(int idDespesa);
        Task<Despesa?> RetornarDespesaPorIdEIdUsuario(int idDespesa, int idUsuario);
        Task<List<Despesa>> RetornarDespesasPorIdUsuario(int idUsuario);
        Task<Despesa> CadastrarDespesa(Despesa despesa);
        Task<Despesa> AtualizarDespesa(Despesa despesa);
        Task<bool> ExcluirDespesa(int idDespesa, int idUsuario);
    }
}