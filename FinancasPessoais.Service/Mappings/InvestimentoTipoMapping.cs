using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class InvestimentoTipoMapping
{
    public static InvestimentoTipoDTO ParaDTO(this InvestimentoTipo investimentoTipo)
    {
        return new InvestimentoTipoDTO(investimentoTipo.IDInvestimentoTipo, investimentoTipo.IDIndexador, investimentoTipo.IDUsuario, investimentoTipo.NMInvestimentoTipo, investimentoTipo.SGInvestimentoTipo, investimentoTipo.INTipoRentabilidade, investimentoTipo.Indexador?.ParaDTO());
    }

    public static List<InvestimentoTipoDTO> ParaDTOs(this List<InvestimentoTipo> investimentosTipos)
    {
        return [.. investimentosTipos.Select(it => it.ParaDTO()).ToList()];
    }

    public static InvestimentoTipo PraEntidade(this InvestimentoTipoDTO investimentoTipoDTO)
    {
        return new InvestimentoTipo(investimentoTipoDTO.IDInvestimentoTipo, investimentoTipoDTO.IDIndexador, investimentoTipoDTO.IDUsuario, investimentoTipoDTO.NMInvestimentoTipo, investimentoTipoDTO.SGInvestimentoTipo, investimentoTipoDTO.INTipoRentabilidade);    
    }
}