using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class InvestimentoTipoMappingExtension
    {
        public static InvestimentoTipoViewModel ParaViewModel(this InvestimentoTipoDTO investimentoTipoDTO, IDataProtector? protetor = null)
        {
            var indexadorViewModel = investimentoTipoDTO.IndexadorDTO?.ParaViewModel();

            return new InvestimentoTipoViewModel(investimentoTipoDTO.IDInvestimentoTipo, protetor?.Protect(investimentoTipoDTO.IDInvestimentoTipo.ToString()), investimentoTipoDTO.IDIndexador, investimentoTipoDTO.IDUsuario, investimentoTipoDTO.NMInvestimentoTipo, investimentoTipoDTO.SGInvestimentoTipo, investimentoTipoDTO.INTipoRentabilidade, indexadorViewModel);
        }

        public static IEnumerable<InvestimentoTipoViewModel> ParaViewModels(this IEnumerable<InvestimentoTipoDTO> investimentoTiposDTOs, IDataProtector protetor)
        {
            return [.. investimentoTiposDTOs.Select(it => it.ParaViewModel(protetor))];
        }

        public static InvestimentoTipoDTO ParaDTO(this InvestimentoTipoViewModel investimentoTipoViewModel)
        {
            return new InvestimentoTipoDTO(investimentoTipoViewModel.IDInvestimentoTipo, investimentoTipoViewModel.IDIndexador, investimentoTipoViewModel.IDUsuario, investimentoTipoViewModel.NMInvestimentoTipo, investimentoTipoViewModel.SGInvestimentoTipo, investimentoTipoViewModel.INTipoRentabilidade, null);
        }
    }
}