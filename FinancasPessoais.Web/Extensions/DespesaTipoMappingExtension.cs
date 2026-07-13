using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class DespesaTipoMappingExtension
    {
        public static DespesaTipoViewModel ParaViewModel(this DespesaTipoDTO despesaTipoDTO, IDataProtector? protetor = null)
        {
            return new DespesaTipoViewModel(despesaTipoDTO.IDDespesaTipo, protetor?.Protect(despesaTipoDTO.IDDespesaTipo.ToString()), despesaTipoDTO.IDUsuario, despesaTipoDTO.NMDespesaTipo);
        }

        public static IEnumerable<DespesaTipoViewModel> ParaViewModels(this IEnumerable<DespesaTipoDTO> despesasTiposDTOs, IDataProtector protetor)
        {
            return [.. despesasTiposDTOs.Select(dt => dt.ParaViewModel(protetor))];
        }

        public static DespesaTipoDTO ParaDTO(this DespesaTipoViewModel despesaTipoViewModel)
        {
            return new DespesaTipoDTO(despesaTipoViewModel.IDDespesaTipo, despesaTipoViewModel.IDUsuario, despesaTipoViewModel.NMDespesaTipo);
        }
    }
}