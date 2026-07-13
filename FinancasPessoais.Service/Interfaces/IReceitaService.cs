using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IReceitaService
    {
        Task<ReceitaDTO?> RetornarReceitaPorId(int idReceita);
        Task<ReceitaDTO?> RetornarReceitaPorIdEIdUsuario(int idReceita, int idUsuario);
        Task<List<ReceitaDTO>> RetornarReceitasPorIdUsuario(int idUsuario);
        Task<Resultado<ReceitaDTO>> CadastrarReceita(ReceitaDTO receita);
        Task<Resultado<ReceitaDTO>> AtualizarReceita(ReceitaDTO receita);
        Task<bool> ExcluirReceita(int idReceita, int idUsuario);
        Task<Resultado<ReceitaDTO>> RetornarReceitaAutentica(int idReceita, int idUsuario);
    }
}