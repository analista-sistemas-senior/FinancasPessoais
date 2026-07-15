using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class DespesaTipoMapping
{
    public static DespesaTipoDTO ParaDTO(this DespesaTipo despesaTipo)
    {
        return new DespesaTipoDTO(despesaTipo.IDDespesaTipo, despesaTipo.IDUsuario, despesaTipo.NMDespesaTipo);
    }

    public static List<DespesaTipoDTO> ParaDTOs(this List<DespesaTipo> despesasTipos)
    {
        return [.. despesasTipos.Select(dt => dt.ParaDTO()).ToList()];
    }

    public static DespesaTipo ParaEntidade(this DespesaTipoDTO despesaTipoDTO)
    {
        return new DespesaTipo(despesaTipoDTO.IDDespesaTipo, despesaTipoDTO.IDUsuario, despesaTipoDTO.NMDespesaTipo);
    }
}