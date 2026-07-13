using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IReceitaFonteService
    {
        Task<ReceitaFonteDTO?> RetornarReceitaFontePorId(int idReceitaFonte);
        Task<ReceitaFonteDTO?> RetornarReceitaFontePorIdEIdUsuario(int idReceitaFonte, int idUsuario);
        Task<List<ReceitaFonteDTO>> RetornarReceitasFontesPorIdUsuario(int idUsuario);
        Task<Resultado<ReceitaFonteDTO>> CadastrarReceitaFonte(ReceitaFonteDTO receitaFonte);
        Task<Resultado<ReceitaFonteDTO>> AtualizarReceitaFonte(ReceitaFonteDTO receitaFonte);
        Task<bool> ExcluirReceitaFonte(int idReceitaFonte, int idUsuario);
        Task<Resultado<ReceitaFonteDTO>> RetornarReceitaFonteAutentica(int idReceitaFonte, int idUsuario);
    }
}