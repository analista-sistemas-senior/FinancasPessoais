using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IInvestimentoHistoricoService
    {
        Task<InvestimentoHistoricoDTO?> RetornarInvestimentoHistoricoPorId(int idInvestimentoHistorico);
        Task<InvestimentoHistoricoDTO?> RetornarInvestimentoHistoricoPorIdEIdUsuario(int idInvestimentoHistorico, int idUsuario);
        Task<List<InvestimentoHistoricoDTO>> RetornarInvestimentosHistoricosPorIdUsuario(int idUsuario);
        Task<Resultado<InvestimentoHistoricoDTO>> CadastrarInvestimentoHistorico(InvestimentoHistoricoDTO investimentoHistorico);
        Task<Resultado<InvestimentoHistoricoDTO>> AtualizarInvestimentoHistorico(InvestimentoHistoricoDTO investimentoHistorico);
        Task<bool> ExcluirInvestimentoHistorico(int idInvestimentoHistorico, int idInvestimento, int idUsuario);
        Task<Resultado<InvestimentoHistoricoDTO>> RetornarInvestimentoHistoricoAutentico(int idInvestimentoHistorico, int idUsuario);
        Task<List<InvestimentoHistoricoDTO>> RetornarInvestimentosHistoricosPorIdInvestimento(int idInvestimento);
        Task AtualizarInvestimentoSaldo(int idInvestimento);
    }
}