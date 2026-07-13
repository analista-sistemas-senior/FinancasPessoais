using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class ReceitaMapping
{
    public static ReceitaDTO ParaDTO(this Receita receita)
    {
        return new ReceitaDTO(receita.IDReceita, receita.IDCarteira, receita.IDReceitaTipo, receita.IDReceitaFonte, receita.IDUsuario, receita.NMReceita, receita.DSReceita, receita.DTReceita, receita.VLReceita, receita.Carteira?.ParaDTO(), receita.ReceitaTipo?.ParaDTO(), receita.ReceitaFonte?.ParaDTO());
    }

    public static List<ReceitaDTO> ParaDTOs(this List<Receita> receitas)
    {
        return [.. receitas.Select(r => r.ParaDTO()).ToList()];
    }

    public static Receita PraEntidade(this ReceitaDTO receitaDTO)
    {
        return new Receita(receitaDTO.IDReceita, receitaDTO.IDCarteira, receitaDTO.IDReceitaTipo, receitaDTO.IDReceitaFonte, receitaDTO.IDUsuario, receitaDTO.NMReceita, receitaDTO.DSReceita, receitaDTO.DTReceita, receitaDTO.VLReceita);
    }
}