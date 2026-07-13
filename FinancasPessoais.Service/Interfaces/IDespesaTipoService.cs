using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IDespesaTipoService
    {
        Task<DespesaTipoDTO?> RetornarDespesaTipoPorId(int idDespesaTipo);
        Task<DespesaTipoDTO?> RetornarDespesaTipoPorIdEIdUsuario(int idDespesaTipo, int idUsuario);
        Task<List<DespesaTipoDTO>> RetornarDespesasTiposPorIdUsuario(int idUsuario);
        Task<Resultado<DespesaTipoDTO>> CadastrarDespesaTipo(DespesaTipoDTO despesaTipo);
        Task<Resultado<DespesaTipoDTO>> AtualizarDespesaTipo(DespesaTipoDTO despesaTipo);
        Task<bool> ExcluirDespesaTipo(int idDespesaTipo, int idUsuario);
        Task<Resultado<DespesaTipoDTO>> RetornarDespesaTipoAutentica(int idDespesaTipo, int idUsuario);
    }
}