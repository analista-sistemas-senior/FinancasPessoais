using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IReceitaTipoRepository
    {
        Task<ReceitaTipo?> RetornarReceitaTipoPorId(int idReceitaTipo);
        Task<ReceitaTipo?> RetornarReceitaTipoPorIdEIdUsuario(int idReceitaTipo, int idUsuario);
        Task<List<ReceitaTipo>> RetornarReceitasTiposPorIdUsuario(int idUsuario);
        Task<ReceitaTipo> CadastrarReceitaTipo(ReceitaTipo receitaTipo);
        Task<ReceitaTipo> AtualizarReceitaTipo(ReceitaTipo receitaTipo);
        Task<bool> ExcluirReceitaTipo(int idReceitaTipo, int idUsuario);
    }
}