using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IDespesaFonteService
    {
        Task<DespesaFonteDTO?> RetornarDespesaFontePorId(int idDespesaFonte);
        Task<DespesaFonteDTO?> RetornarDespesaFontePorIdEIdUsuario(int idDespesaFonte, int idUsuario);
        Task<List<DespesaFonteDTO>> RetornarDespesasFontesPorIdUsuario(int idUsuario);
        Task<Resultado<DespesaFonteDTO>> CadastrarDespesaFonte(DespesaFonteDTO despesaFonte);
        Task<Resultado<DespesaFonteDTO>> AtualizarDespesaFonte(DespesaFonteDTO despesaFonte);
        Task<bool> ExcluirDespesaFonte(int idDespesaFonte, int idUsuario);
        Task<Resultado<DespesaFonteDTO>> RetornarDespesaFonteAutentica(int idDespesaFonte, int idUsuario);
    }
}