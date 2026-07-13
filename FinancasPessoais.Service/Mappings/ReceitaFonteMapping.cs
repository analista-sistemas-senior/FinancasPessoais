using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class ReceitaFonteMapping
{
    public static ReceitaFonteDTO ParaDTO(this ReceitaFonte receitaFonte)
    {
        return new ReceitaFonteDTO(receitaFonte.IDReceitaFonte, receitaFonte.IDUsuario, receitaFonte.NMReceitaFonte);
    }

    public static List<ReceitaFonteDTO> ParaDTOs(this List<ReceitaFonte> receitasFontes)
    {
        return [.. receitasFontes.Select(rf => rf.ParaDTO()).ToList()];
    }

    public static ReceitaFonte PraEntidade(this ReceitaFonteDTO receitaFonteDTO)
    {
        return new ReceitaFonte(receitaFonteDTO.IDReceitaFonte, receitaFonteDTO.IDUsuario, receitaFonteDTO.NMReceitaFonte);
    }
}