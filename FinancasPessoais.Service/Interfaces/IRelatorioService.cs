using FinancasPessoais.Service.DTOs.Reports;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IRelatorioService
    {
        Task<(decimal totalInvestimento, decimal totalJuro, decimal totalAporte, decimal totalSaque)> RetornarInvestimentosTotais(int idUsuario);
        Task<Dictionary<string, decimal>> RetornarInvestimentosTotais12Meses(int idUsuario);
        Task<Dictionary<string, (decimal Receita, decimal Despesa)>> RetornarFluxoCaixa12Meses(int idUsuario);
        Task<decimal> RetornarDespesasTotais(int idUsuario);
        Task<decimal> RetornarReceitasTotais(int idUsuario);
        Task<List<RelatorioHomeDTO>> RetornarInvestimentosPorTipo(int idUsuario);
        Task<List<RelatorioHomeDTO>> RetornarDespesasPorFontes(int idUsuario);
        Task<List<RelatorioInvestimentoVariacaoDTO>> RetornarInvestimentosVariacoes(int idUsuario);
    }
}