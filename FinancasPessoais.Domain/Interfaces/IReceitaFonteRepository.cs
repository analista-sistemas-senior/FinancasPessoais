using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IReceitaFonteRepository
    {
        Task<ReceitaFonte?> RetornarReceitaFontePorId(int idReceitaFonte);
        Task<ReceitaFonte?> RetornarReceitaFontePorIdEIdUsuario(int idReceitaFonte, int idUsuario);
        Task<List<ReceitaFonte>> RetornarReceitasFontesPorIdUsuario(int idUsuario);
        Task<ReceitaFonte> CadastrarReceitaFonte(ReceitaFonte receitaFonte);
        Task<ReceitaFonte> AtualizarReceitaFonte(ReceitaFonte receitaFonte);
        Task<bool> ExcluirReceitaFonte(int idReceitaFonte, int idUsuario);
    }
}