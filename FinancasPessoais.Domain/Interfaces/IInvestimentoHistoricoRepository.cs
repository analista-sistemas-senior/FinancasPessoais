using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IInvestimentoHistoricoRepository
    {
        Task<InvestimentoHistorico?> RetornarInvestimentoHistoricoPorId(int idInvestimentoHistorico);
        Task<InvestimentoHistorico?> RetornarInvestimentoHistoricoPorIdEIdUsuario(int idInvestimentoHistorico, int idUsuario);
        Task<List<InvestimentoHistorico>> RetornarInvestimentosHistoricosPorIdUsuario(int idUsuario);
        Task<InvestimentoHistorico> CadastrarInvestimentoHistorico(InvestimentoHistorico investimentoHistorico);
        Task<InvestimentoHistorico> AtualizarInvestimentoHistorico(InvestimentoHistorico investimentoHistorico);
        Task<bool> ExcluirInvestimentoHistorico(int idInvestimentoHistorico, int idUsuario);
        Task<List<InvestimentoHistorico>> RetornarInvestimentosHistoricosPorIdInvestimento(int idInvestimento);
    }
}