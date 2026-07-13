using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class ReceitaTipoMappingExtension
    {
        public static ReceitaTipoViewModel ParaViewModel(this ReceitaTipoDTO receitaTipoDTO, IDataProtector? protetor = null)
        {
            return new ReceitaTipoViewModel(receitaTipoDTO.IDReceitaTipo, protetor?.Protect(receitaTipoDTO.IDReceitaTipo.ToString()), receitaTipoDTO.IDUsuario, receitaTipoDTO.NMReceitaTipo);
        }

        public static IEnumerable<ReceitaTipoViewModel> ParaViewModels(this IEnumerable<ReceitaTipoDTO> despesasTiposDTOs, IDataProtector protetor)
        {
            return [.. despesasTiposDTOs.Select(dt => dt.ParaViewModel(protetor))];
        }

        public static ReceitaTipoDTO ParaDTO(this ReceitaTipoViewModel receitaTipoViewModel)
        {
            return new ReceitaTipoDTO(receitaTipoViewModel.IDReceitaTipo, receitaTipoViewModel.IDUsuario, receitaTipoViewModel.NMReceitaTipo);
        }
    }
}