using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class ReceitaMappingExtension
    {
        public static ReceitaViewModel ParaViewModel(this ReceitaDTO receitaDTO, IDataProtector? protetor = null)
        {
            var carteiraViewModel = receitaDTO.CarteiraDTO?.ParaViewModel();
            var receitaTipoViewModel = receitaDTO.ReceitaTipoDTO?.ParaViewModel();
            var receitaFonteViewModel = receitaDTO.ReceitaFonteDTO?.ParaViewModel();

            return new ReceitaViewModel(receitaDTO.IDReceita, protetor?.Protect(receitaDTO.IDReceita.ToString()), receitaDTO.IDCarteira, receitaDTO.IDReceitaTipo, receitaDTO.IDReceitaFonte, receitaDTO.IDUsuario, receitaDTO.NMReceita, receitaDTO.DSReceita, receitaDTO.DTReceita, receitaDTO.VLReceita, carteiraViewModel, receitaTipoViewModel, receitaFonteViewModel);
        }

        public static IEnumerable<ReceitaViewModel> ParaViewModels(this IEnumerable<ReceitaDTO> receitasDTOs, IDataProtector protetor)
        {
            return [.. receitasDTOs.Select(d => d.ParaViewModel(protetor))];
        }

        public static ReceitaDTO ParaDTO(this ReceitaViewModel receitaViewModel)
        {
            return new ReceitaDTO(receitaViewModel.IDReceita, receitaViewModel.IDCarteira, receitaViewModel.IDReceitaTipo, receitaViewModel.IDReceitaFonte, receitaViewModel.IDUsuario, receitaViewModel.NMReceita, receitaViewModel.DSReceita, ConverteData(receitaViewModel.DTReceita), receitaViewModel.VLReceita, null, null, null);
        }

        private static DateTime ConverteData(DateTime viewModelData)
        {
            return DateTime.SpecifyKind(viewModelData, DateTimeKind.Utc);
        }
    }
}