using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class InvestimentoMapping
{
    public static InvestimentoDTO ParaDTO(this Investimento investimento)
    {
        return new InvestimentoDTO(investimento.IDInvestimento, investimento.IDInvestimentoTipo, investimento.IDEmissor, investimento.IDUsuario, investimento.NMInvestimento, investimento.VLInvestimento, investimento.DTInvestimento, investimento.VLSaldo, investimento.DTVencimento, investimento.PCTaxaRentabilidade, investimento.INTaxaPeriodicidade, investimento.TXAnotacao, investimento.FLLiquidado, investimento.InvestimentoTipo?.ParaDTO(), investimento.Emissor?.ParaDTO());
    }

    public static List<InvestimentoDTO> ParaDTOs(this List<Investimento> investimentos)
    {
        return [.. investimentos.Select(i => i.ParaDTO()).ToList()];
    }

    public static Investimento ParaEntidade(this InvestimentoDTO investimentoDTO)
    {
        return new Investimento(investimentoDTO.IDInvestimento, investimentoDTO.IDInvestimentoTipo, investimentoDTO.IDEmissor, investimentoDTO.IDUsuario, investimentoDTO.NMInvestimento, investimentoDTO.VLInvestimento, investimentoDTO.DTInvestimento, investimentoDTO.VLSaldo, investimentoDTO.DTVencimento, investimentoDTO.PCTaxaRentabilidade, investimentoDTO.INTaxaPeriodicidade, investimentoDTO.TXAnotacao, investimentoDTO.FLLiquidado);
    }
}