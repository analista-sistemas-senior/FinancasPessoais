using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IDespesaService
    {
        Task<DespesaDTO?> RetornarDespesaPorId(int idDespesa);
        Task<DespesaDTO?> RetornarDespesaPorIdEIdUsuario(int idDespesa, int idUsuario);
        Task<List<DespesaDTO>> RetornarDespesasPorIdUsuario(int idUsuario);
        Task<Resultado<DespesaDTO>> CadastrarDespesa(DespesaDTO despesa);
        Task<Resultado<DespesaDTO>> AtualizarDespesa(DespesaDTO despesa);
        Task<bool> ExcluirDespesa(int idDespesa, int idUsuario);
        Task<Resultado<DespesaDTO>> RetornarDespesaAutentica(int idDespesa, int idUsuario);
    }
}