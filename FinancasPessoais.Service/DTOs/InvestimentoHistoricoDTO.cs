using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Service.DTOs
{
    public record InvestimentoHistoricoDTO(int IDInvestimentoHistorico, int IDInvestimento, DateTime DTInvestimentoHistorico, decimal VLInvestimentoHistorico, TipoInvestimentoHistorico INInvestimentoHistorico, InvestimentoDTO? InvestimentoDTO);
}