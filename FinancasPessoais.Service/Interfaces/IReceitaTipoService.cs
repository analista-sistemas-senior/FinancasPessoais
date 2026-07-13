using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IReceitaTipoService
    {
        Task<ReceitaTipoDTO?> RetornarReceitaTipoPorId(int idReceitaTipo);
        Task<ReceitaTipoDTO?> RetornarReceitaTipoPorIdEIdUsuario(int idReceitaTipo, int idUsuario);
        Task<List<ReceitaTipoDTO>> RetornarReceitasTiposPorIdUsuario(int idUsuario);
        Task<Resultado<ReceitaTipoDTO>> CadastrarReceitaTipo(ReceitaTipoDTO receitaTipo);
        Task<Resultado<ReceitaTipoDTO>> AtualizarReceitaTipo(ReceitaTipoDTO receitaTipo);
        Task<bool> ExcluirReceitaTipo(int idReceitaTipo, int idUsuario);
        Task<Resultado<ReceitaTipoDTO>> RetornarReceitaTipoAutentico(int idReceitaTipo, int idUsuario);
    }
}