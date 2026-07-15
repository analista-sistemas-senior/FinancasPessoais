using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class IndexadorMapping
{
    public static IndexadorDTO ParaDTO(this Indexador indexador)
    {
        return new IndexadorDTO(indexador.IDIndexador, indexador.IDIndiceFinanceiro, indexador.IDUsuario, indexador.NMIndexador, indexador.SGIndexador, indexador.PCIndiceFinanceiro, indexador.IndiceFinanceiro?.ParaDTO());
    }

    public static List<IndexadorDTO> ParaDTOs(this List<Indexador> indexadores)
    {
        return [.. indexadores.Select(inx => inx.ParaDTO()).ToList()];
    }

    public static Indexador ParaEntidade(this IndexadorDTO indexadorDTO)
    {
        return new Indexador(indexadorDTO.IDIndexador, indexadorDTO.IDIndiceFinanceiro, indexadorDTO.IDUsuario, indexadorDTO.NMIndexador, indexadorDTO.SGIndexador, indexadorDTO.PCIndiceFinanceiro);
    }
}