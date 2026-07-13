using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IInvestimentoTipoService
    {
        Task<InvestimentoTipoDTO?> RetornarInvestimentoTipoPorId(int idInvestimentoTipo);
        Task<InvestimentoTipoDTO?> RetornarInvestimentoTipoPorIdEIdUsuario(int idInvestimentoTipo, int idUsuario);
        Task<List<InvestimentoTipoDTO>> RetornarInvestimentosTiposPorIdUsuario(int idUsuario);
        Task<Resultado<InvestimentoTipoDTO>> CadastrarInvestimentoTipo(InvestimentoTipoDTO investimentoTipo);
        Task<Resultado<InvestimentoTipoDTO>> AtualizarInvestimentoTipo(InvestimentoTipoDTO investimentoTipo);
        Task<bool> ExcluirInvestimentoTipo(int idInvestimentoTipo, int idUsuario);
        Task<Resultado<InvestimentoTipoDTO>> RetornarInvestimentoTipoAutentico(int idInvestimentoTipo, int idUsuario);
    }
}