using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class InvestimentoHistoricoMapping
{
    public static InvestimentoHistoricoDTO ParaDTO(this InvestimentoHistorico investimentoHistorico)
    {
        return new InvestimentoHistoricoDTO(investimentoHistorico.IDInvestimentoHistorico, investimentoHistorico.IDInvestimento, investimentoHistorico.DTInvestimentoHistorico, investimentoHistorico.VLInvestimentoHistorico, investimentoHistorico.INInvestimentoHistorico, investimentoHistorico.Investimento?.ParaDTO());
    }

    public static List<InvestimentoHistoricoDTO> ParaDTOs(this List<InvestimentoHistorico> investimentosHistoricos)
    {
        return [.. investimentosHistoricos.Select(ih => ih.ParaDTO()).ToList()];
    }

    public static InvestimentoHistorico ParaEntidade(this InvestimentoHistoricoDTO investimentoHistoricoDTO)
    {
        return new InvestimentoHistorico(investimentoHistoricoDTO.IDInvestimentoHistorico, investimentoHistoricoDTO.IDInvestimento, investimentoHistoricoDTO.DTInvestimentoHistorico, investimentoHistoricoDTO.VLInvestimentoHistorico, investimentoHistoricoDTO.INInvestimentoHistorico);
    }
}