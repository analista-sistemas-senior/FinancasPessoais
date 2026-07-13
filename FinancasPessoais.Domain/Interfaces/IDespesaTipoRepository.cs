using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IDespesaTipoRepository
    {
        Task<DespesaTipo?> RetornarDespesaTipoPorId(int idDespesaTipo);
        Task<DespesaTipo?> RetornarDespesaTipoPorIdEIdUsuario(int idDespesaTipo, int idUsuario);
        Task<List<DespesaTipo>> RetornarDespesasTiposPorIdUsuario(int idUsuario);
        Task<DespesaTipo> CadastrarDespesaTipo(DespesaTipo despesaTipo);
        Task<DespesaTipo> AtualizarDespesaTipo(DespesaTipo despesaTipo);
        Task<bool> ExcluirDespesaTipo(int idCarteira, int idUsuario);
    }
}