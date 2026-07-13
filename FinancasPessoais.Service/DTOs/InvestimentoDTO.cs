using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Service.DTOs
{
    public record InvestimentoDTO(int IDInvestimento, int IDInvestimentoTipo, int IDEmissor, int IDUsuario, string NMInvestimento, decimal VLInvestimento, DateTime DTInvestimento, decimal VLSaldo, DateTime? DTVencimento, decimal? PCTaxaRentabilidade, TaxaPeriodicidade? INTaxaPeriodicidade, string? TXAnotacao, bool? FLLiquidado, InvestimentoTipoDTO? InvestimentoTipoDTO, EmissorDTO? EmissorDTO);
}