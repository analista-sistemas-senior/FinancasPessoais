using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Service.DTOs.Reports
{
    public record RelatorioInvestimentoVariacaoDTO(int IDInvestimento, string NMInvestimento, DateTime DTInvestimento, decimal VLSaldo, DateTime? DTVencimento, decimal? PCTaxaRentabilidade, bool? FLLiquidado, decimal VLInvestimentoVariacao, decimal PCInvestimentoVariacao, TaxaPeriodicidade? INTaxaPeriodicidade);
}