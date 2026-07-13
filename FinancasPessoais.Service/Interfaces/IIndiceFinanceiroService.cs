using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IIndiceFinanceiroService
    {
        Task<IndiceFinanceiroDTO?> RetornarIndiceFinanceiroPorId(int idIndiceFinanceiro);
        Task<IndiceFinanceiroDTO?> RetornarIndiceFinanceiroPorIdEIdUsuario(int idIndiceFinanceiro, int idUsuario);
        Task<List<IndiceFinanceiroDTO>> RetornarIndicesFinanceirosPorIdUsuario(int idUsuario);
        Task<Resultado<IndiceFinanceiroDTO>> CadastrarIndiceFinanceiro(IndiceFinanceiroDTO indiceFinanceiro);
        Task<Resultado<IndiceFinanceiroDTO>> AtualizarIndiceFinanceiro(IndiceFinanceiroDTO indiceFinanceiro);
        Task<bool> ExcluirIndiceFinanceiro(int idIndiceFinanceiro, int idUsuario);
        Task<Resultado<IndiceFinanceiroDTO>> RetornarIndiceFinanceiroAutentico(int idIndiceFinanceiro, int idUsuario);
    }
}