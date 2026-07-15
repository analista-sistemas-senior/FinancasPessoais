using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class CarteiraMapping
{
    public static CarteiraDTO ParaDTO(this Carteira carteira)
    {
        return new CarteiraDTO(carteira.IDCarteira, carteira.IDUsuario, carteira.NMCarteira);
    }

    public static List<CarteiraDTO> ParaDTOs(this List<Carteira> carteiras)
    {
        return [.. carteiras.Select(c => c.ParaDTO()).ToList()];
    }

    public static Carteira ParaEntidade(this CarteiraDTO carteiraDTO)
    {
        return new Carteira(carteiraDTO.IDCarteira, carteiraDTO.IDUsuario, carteiraDTO.NMCarteira);
    }
}