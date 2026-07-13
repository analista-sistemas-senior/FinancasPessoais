using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class ReceitaFonteMappingExtension
    {
        public static ReceitaFonteViewModel ParaViewModel(this ReceitaFonteDTO receitaFonteDTO, IDataProtector? protetor = null)
        {
            return new ReceitaFonteViewModel(receitaFonteDTO.IDReceitaFonte, protetor?.Protect(receitaFonteDTO.IDReceitaFonte.ToString()), receitaFonteDTO.IDUsuario, receitaFonteDTO.NMReceitaFonte);
        }

        public static IEnumerable<ReceitaFonteViewModel> ParaViewModels(this IEnumerable<ReceitaFonteDTO> despesasFontesDTOs, IDataProtector protetor)
        {
            return [.. despesasFontesDTOs.Select(df => df.ParaViewModel(protetor))];
        }

        public static ReceitaFonteDTO ParaDTO(this ReceitaFonteViewModel receitaFonteViewModel)
        {
            return new ReceitaFonteDTO(receitaFonteViewModel.IDReceitaFonte, receitaFonteViewModel.IDUsuario, receitaFonteViewModel.NMReceitaFonte);
        }
    }
}