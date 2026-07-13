using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class InvestimentoMappingExtension
    {
        public static InvestimentoViewModel ParaViewModel(this InvestimentoDTO investimentoDTO, IDataProtector? protetor = null)
        {
            var investimentoTipoViewModel = investimentoDTO.InvestimentoTipoDTO?.ParaViewModel();
            var emissorViewModel = investimentoDTO.EmissorDTO?.ParaViewModel();

            return new InvestimentoViewModel(investimentoDTO.IDInvestimento, protetor?.Protect(investimentoDTO.IDInvestimento.ToString()), investimentoDTO.IDInvestimentoTipo, investimentoDTO.IDEmissor, investimentoDTO.IDUsuario, investimentoDTO.NMInvestimento, investimentoDTO.VLInvestimento, investimentoDTO.DTInvestimento, investimentoDTO.VLSaldo, investimentoDTO.DTVencimento, investimentoDTO.PCTaxaRentabilidade, investimentoDTO.INTaxaPeriodicidade, investimentoDTO.TXAnotacao, investimentoDTO.FLLiquidado, investimentoTipoViewModel, emissorViewModel);
        }

        public static IEnumerable<InvestimentoViewModel> ParaViewModels(this IEnumerable<InvestimentoDTO> investimentosDTOs, IDataProtector protetor)
        {
            return [.. investimentosDTOs.Select(i => i.ParaViewModel(protetor))];
        }

        public static InvestimentoDTO ParaDTO(this InvestimentoViewModel investimentoViewModel)
        {
            return new InvestimentoDTO(investimentoViewModel.IDInvestimento, investimentoViewModel.IDInvestimentoTipo, investimentoViewModel.IDEmissor, investimentoViewModel.IDUsuario, investimentoViewModel.NMInvestimento, investimentoViewModel.VLInvestimento, ConverteData(investimentoViewModel.DTInvestimento), investimentoViewModel.VLSaldo, ConverteDataOpcional(investimentoViewModel.DTVencimento), investimentoViewModel.PCTaxaRentabilidade, investimentoViewModel.INTaxaPeriodicidade, investimentoViewModel.TXAnotacao, investimentoViewModel.FLLiquidado, null, null);
        }

        private static DateTime ConverteData(DateTime viewModelData)
        {
            return DateTime.SpecifyKind(viewModelData, DateTimeKind.Utc);
        }

        private static DateTime? ConverteDataOpcional(DateTime? viewModelData)
        {
            if (viewModelData == null) return null;
            return DateTime.SpecifyKind(viewModelData.Value.Date, DateTimeKind.Utc);
        }
    }
}