using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class DespesaFonteMappingExtension
    {
        public static DespesaFonteViewModel ParaViewModel(this DespesaFonteDTO despesaFonteDTO, IDataProtector? protetor = null)
        {
            return new DespesaFonteViewModel(despesaFonteDTO.IDDespesaFonte, protetor?.Protect(despesaFonteDTO.IDDespesaFonte.ToString()), despesaFonteDTO.IDUsuario, despesaFonteDTO.NMDespesaFonte);
        }

        public static IEnumerable<DespesaFonteViewModel> ParaViewModels(this IEnumerable<DespesaFonteDTO> despesasFontesDTOs, IDataProtector protetor)
        {
            return [.. despesasFontesDTOs.Select(df => df.ParaViewModel(protetor))];
        }

        public static DespesaFonteDTO ParaDTO(this DespesaFonteViewModel despesaFonteViewModel)
        {
            return new DespesaFonteDTO(despesaFonteViewModel.IDDespesaFonte, despesaFonteViewModel.IDUsuario, despesaFonteViewModel.NMDespesaFonte);
        }
    }
}