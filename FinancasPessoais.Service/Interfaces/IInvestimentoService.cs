using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IInvestimentoService
    {
        Task<InvestimentoDTO?> RetornarInvestimentoPorId(int idInvestimento);
        Task<InvestimentoDTO?> RetornarInvestimentoPorIdEIdUsuario(int idInvestimento, int idUsuario);
        Task<List<InvestimentoDTO>> RetornarInvestimentosPorIdUsuario(int idUsuario);
        Task<Resultado<InvestimentoDTO>> CadastrarInvestimento(InvestimentoDTO investimento);
        Task<Resultado<InvestimentoDTO>> AtualizarInvestimento(InvestimentoDTO investimento);
        Task<bool> ExcluirInvestimento(int idInvestimento, int idUsuario);
        Task<Resultado<InvestimentoDTO>> RetornarInvestimentoAutentico(int idInvestimento, int idUsuario);
    }
}