using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class ReceitaTipoMapping
{
    public static ReceitaTipoDTO ParaDTO(this ReceitaTipo receitaTipo)
    {
        return new ReceitaTipoDTO(receitaTipo.IDReceitaTipo, receitaTipo.IDUsuario, receitaTipo.NMReceitaTipo);
    }

    public static List<ReceitaTipoDTO> ParaDTOs(this List<ReceitaTipo> receitasTipos)
    {
        return [.. receitasTipos.Select(rt => rt.ParaDTO()).ToList()];
    }

    public static ReceitaTipo PraEntidade(this ReceitaTipoDTO receitaTipoDTO)
    {
        return new ReceitaTipo(receitaTipoDTO.IDReceitaTipo, receitaTipoDTO.IDUsuario, receitaTipoDTO.NMReceitaTipo);
    }
}