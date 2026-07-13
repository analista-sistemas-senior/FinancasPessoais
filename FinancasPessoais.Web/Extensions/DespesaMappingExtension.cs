using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class DespesaMappingExtension
    {
        public static DespesaViewModel ParaViewModel(this DespesaDTO despesaDTO, IDataProtector? protetor = null)
        {
            var carteiraViewModel = despesaDTO.CarteiraDTO?.ParaViewModel();
            var despesaTipoViewModel = despesaDTO.DespesaTipoDTO?.ParaViewModel();
            var despesaFonteViewModel = despesaDTO.DespesaFonteDTO?.ParaViewModel();

            return new DespesaViewModel(despesaDTO.IDDespesa, protetor?.Protect(despesaDTO.IDDespesa.ToString()), despesaDTO.IDCarteira, despesaDTO.IDDespesaTipo, despesaDTO.IDDespesaFonte, despesaDTO.IDUsuario, despesaDTO.NMDespesa, despesaDTO.DSDespesa, despesaDTO.DTDespesa, despesaDTO.VLDespesa, carteiraViewModel, despesaTipoViewModel, despesaFonteViewModel);
        }

        public static IEnumerable<DespesaViewModel> ParaViewModels(this IEnumerable<DespesaDTO> despesasDTOs, IDataProtector protetor)
        {
            return [.. despesasDTOs.Select(d => d.ParaViewModel(protetor))];
        }

        public static DespesaDTO ParaDTO(this DespesaViewModel despesaViewModel)
        {
            return new DespesaDTO(despesaViewModel.IDDespesa, despesaViewModel.IDCarteira, despesaViewModel.IDDespesaTipo, despesaViewModel.IDDespesaFonte, despesaViewModel.IDUsuario, despesaViewModel.NMDespesa, despesaViewModel.DSDespesa, ConverteData(despesaViewModel.DTDespesa), despesaViewModel.VLDespesa, null, null,null);
        }

        private static DateTime ConverteData(DateTime viewModelData)
        {
            return DateTime.SpecifyKind(viewModelData, DateTimeKind.Utc);
        }
    }
}