using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IDespesaFonteRepository
    {
        Task<DespesaFonte?> RetornarDespesaFontePorId(int idDespesaFonte);
        Task<DespesaFonte?> RetornarDespesaFontePorIdEIdUsuario(int idDespesaFonte, int idUsuario);
        Task<List<DespesaFonte>> RetornarDespesasFontesPorIdUsuario(int idUsuario);
        Task<DespesaFonte> CadastrarDespesaFonte(DespesaFonte despesaFonte);
        Task<DespesaFonte> AtualizarDespesaFonte(DespesaFonte despesaFonte);
        Task<bool> ExcluirDespesaFonte(int idDespesaFonte, int idUsuario);
    }
}