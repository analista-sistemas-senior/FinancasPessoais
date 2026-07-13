using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class CarteiraMappingExtension
    {
        public static CarteiraViewModel ParaViewModel(this CarteiraDTO carteiraDTO, IDataProtector? protetor = null)
        {
            return new CarteiraViewModel(carteiraDTO.IDCarteira, protetor?.Protect(carteiraDTO.IDCarteira.ToString()), carteiraDTO.IDUsuario, carteiraDTO.NMCarteira);
        }

        public static IEnumerable<CarteiraViewModel> ParaViewModels(this IEnumerable<CarteiraDTO> carteirasDTOs, IDataProtector protetor)
        {
            return [.. carteirasDTOs.Select(c => c.ParaViewModel(protetor))];
        }

        public static CarteiraDTO ParaDTO(this CarteiraViewModel carteiraViewModel)
        {
            return new CarteiraDTO(carteiraViewModel.IDCarteira, carteiraViewModel.IDUsuario, carteiraViewModel.NMCarteira);
        }
    }
}