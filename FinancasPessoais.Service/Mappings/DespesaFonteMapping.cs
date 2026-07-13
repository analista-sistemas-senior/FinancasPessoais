using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class DespesaFonteMapping
{
    public static DespesaFonteDTO ParaDTO(this DespesaFonte despesaFonte)
    {
        return new DespesaFonteDTO(despesaFonte.IDDespesaFonte, despesaFonte.IDUsuario, despesaFonte.NMDespesaFonte);
    }

    public static List<DespesaFonteDTO> ParaDTOs(this List<DespesaFonte> despesasFontes)
    {
        return [.. despesasFontes.Select(df => df.ParaDTO()).ToList()];
    }

    public static DespesaFonte PraEntidade(this DespesaFonteDTO despesaFonteDTO)
    {
        return new DespesaFonte(despesaFonteDTO.IDDespesaFonte, despesaFonteDTO.IDUsuario, despesaFonteDTO.NMDespesaFonte);
    }
}