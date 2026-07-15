using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class IndiceFinanceiroMapping
{
    public static IndiceFinanceiroDTO ParaDTO(this IndiceFinanceiro indiceFinanceiro)
    {
        return new IndiceFinanceiroDTO(indiceFinanceiro.IDIndiceFinanceiro, indiceFinanceiro.IDUsuario, indiceFinanceiro.NMIndiceFinanceiro, indiceFinanceiro.VLIndiceFinanceiro, indiceFinanceiro.INTaxaPeriodicidade);
    }

    public static List<IndiceFinanceiroDTO> ParaDTOs(this List<IndiceFinanceiro> indicesFinanceiros)
    {
        return [.. indicesFinanceiros.Select(inf => inf.ParaDTO()).ToList()];
    }

    public static IndiceFinanceiro ParaEntidade(this IndiceFinanceiroDTO indiceFinanceiroDTO)
    {
        return new IndiceFinanceiro(indiceFinanceiroDTO.IDIndiceFinanceiro, indiceFinanceiroDTO.IDUsuario, indiceFinanceiroDTO.NMIndiceFinanceiro, indiceFinanceiroDTO.VLIndiceFinanceiro, indiceFinanceiroDTO.INTaxaPeriodicidade);
    }
}