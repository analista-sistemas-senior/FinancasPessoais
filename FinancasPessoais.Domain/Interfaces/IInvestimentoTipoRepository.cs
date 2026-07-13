using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IInvestimentoTipoRepository
    {
        Task<InvestimentoTipo?> RetornarInvestimentoTipoPorId(int idInvestimentoTipo);
        Task<InvestimentoTipo?> RetornarInvestimentoTipoPorIdEIdUsuario(int idInvestimentoTipo, int idUsuario);
        Task<List<InvestimentoTipo>> RetornarInvestimentosTiposPorIdUsuario(int idUsuario);
        Task<InvestimentoTipo> CadastrarInvestimentoTipo(InvestimentoTipo investimentoTipo);
        Task<InvestimentoTipo> AtualizarInvestimentoTipo(InvestimentoTipo investimentoTipo);
        Task<bool> ExcluirInvestimentoTipo(int idInvestimentoTipo, int idUsuario);
    }
}