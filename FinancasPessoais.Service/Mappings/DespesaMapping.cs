using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class DespesaMapping
{
    public static DespesaDTO ParaDTO(this Despesa despesa)
    {
        return new DespesaDTO(despesa.IDDespesa, despesa.IDCarteira, despesa.IDDespesaTipo, despesa.IDDespesaFonte, despesa.IDUsuario, despesa.NMDespesa, despesa.DSDespesa, despesa.DTDespesa, despesa.VLDespesa, despesa.Carteira?.ParaDTO(), despesa.DespesaTipo?.ParaDTO(), despesa.DespesaFonte?.ParaDTO());
    }

    public static List<DespesaDTO> ParaDTOs(this List<Despesa> despesas)
    {
        return [.. despesas.Select(d => d.ParaDTO()).ToList()];
    }

    public static Despesa ParaEntidade(this DespesaDTO despesaDTO)
    {
        return new Despesa(despesaDTO.IDDespesa, despesaDTO.IDCarteira, despesaDTO.IDDespesaTipo, despesaDTO.IDDespesaFonte, despesaDTO.IDUsuario, despesaDTO.NMDespesa, despesaDTO.DSDespesa, despesaDTO.DTDespesa, despesaDTO.VLDespesa);
    }
}