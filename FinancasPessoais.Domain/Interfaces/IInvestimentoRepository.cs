using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IInvestimentoRepository
    {
        Task<Investimento?> RetornarInvestimentoPorId(int idInvestimento);
        Task<Investimento?> RetornarInvestimentoPorIdEIdUsuario(int idInvestimento, int idUsuario);
        Task<List<Investimento>> RetornarInvestimentosPorIdUsuario(int idUsuario);
        Task<Investimento> CadastrarInvestimento(Investimento investimento);
        Task<Investimento> AtualizarInvestimento(Investimento investimento);
        Task<bool> ExcluirInvestimento(int idInvestimento, int idUsuario);
    }
}