using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class InvestimentoHistoricoMappingExtension
    {
        public static InvestimentoHistoricoViewModel ParaViewModel(this InvestimentoHistoricoDTO investimentoHistoricoDTO, IDataProtector? protetor = null)
        {
            var investimentoViewModel = investimentoHistoricoDTO.InvestimentoDTO?.ParaViewModel();

            return new InvestimentoHistoricoViewModel(investimentoHistoricoDTO.IDInvestimentoHistorico, protetor?.Protect(investimentoHistoricoDTO.IDInvestimentoHistorico.ToString()), investimentoHistoricoDTO.IDInvestimento, investimentoHistoricoDTO.DTInvestimentoHistorico, investimentoHistoricoDTO.VLInvestimentoHistorico, investimentoHistoricoDTO.INInvestimentoHistorico, investimentoViewModel);
        }

        public static IEnumerable<InvestimentoHistoricoViewModel> ParaViewModels(this IEnumerable<InvestimentoHistoricoDTO> investimentosHistoricosDTOs, IDataProtector protetor)
        {
            return [.. investimentosHistoricosDTOs.Select(ih => ih.ParaViewModel(protetor))];
        }

        public static InvestimentoHistoricoDTO ParaDTO(this InvestimentoHistoricoViewModel investimentoHistoricoViewModel)
        {
            return new InvestimentoHistoricoDTO(investimentoHistoricoViewModel.IDInvestimentoHistorico, investimentoHistoricoViewModel.IDInvestimento, ConverteData(investimentoHistoricoViewModel.DTInvestimentoHistorico), investimentoHistoricoViewModel.VLInvestimentoHistorico, investimentoHistoricoViewModel.INInvestimentoHistorico, null);
        }

        private static DateTime ConverteData(DateTime viewModelData)
        {
            return DateTime.SpecifyKind(viewModelData, DateTimeKind.Utc);
        }
    }
}