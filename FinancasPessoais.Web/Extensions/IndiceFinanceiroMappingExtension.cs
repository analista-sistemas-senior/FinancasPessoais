using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class IndiceFinanceiroMappingExtension
    {
        public static IndiceFinanceiroViewModel ParaViewModel(this IndiceFinanceiroDTO indiceFinanceiroDTO, IDataProtector? protetor = null)
        {
            return new IndiceFinanceiroViewModel(indiceFinanceiroDTO.IDIndiceFinanceiro, protetor?.Protect(indiceFinanceiroDTO.IDIndiceFinanceiro.ToString()), indiceFinanceiroDTO.IDUsuario, indiceFinanceiroDTO.NMIndiceFinanceiro, indiceFinanceiroDTO.VLIndiceFinanceiro, indiceFinanceiroDTO.INTaxaPeriodicidade);
        }

        public static IEnumerable<IndiceFinanceiroViewModel> ParaViewModels(this IEnumerable<IndiceFinanceiroDTO> indicesFinanceirosDTOs, IDataProtector protetor)
        {
            return [.. indicesFinanceirosDTOs.Select(inf => inf.ParaViewModel(protetor))];
        }

        public static IndiceFinanceiroDTO ParaDTO(this IndiceFinanceiroViewModel indiceFinanceiroViewModel)
        {
            return new IndiceFinanceiroDTO(indiceFinanceiroViewModel.IDIndiceFinanceiro, indiceFinanceiroViewModel.IDUsuario, indiceFinanceiroViewModel.NMIndiceFinanceiro, indiceFinanceiroViewModel.VLIndiceFinanceiro, indiceFinanceiroViewModel.INTaxaPeriodicidade);
        }
    }
}