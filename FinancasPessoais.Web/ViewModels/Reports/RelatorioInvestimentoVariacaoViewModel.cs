using FinancasPessoais.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels.Reports
{
    public class RelatorioInvestimentoVariacaoViewModel
    {
        public int IDInvestimento { get; set; }

        [Display(Name = "Nome do investimento")]
        public string NMInvestimento { get; set; } = string.Empty;

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do investimento")]
        public DateTime DTInvestimento { get; set; }

        [Display(Name = "Valor atual")]
        public decimal VLSaldo { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do vencimento")]
        public DateTime? DTVencimento { get; set; }

        [Display(Name = "Rentabilidade")]
        public decimal? PCTaxaRentabilidade { get; set; }

        [Display(Name = "Liquidado")]
        public bool? FLLiquidado { get; set; }

        [Display(Name = "Variação absoluta")]
        public decimal VLInvestimentoVariacao { get; set; }

        [Display(Name = "Variação percentual")]
        public decimal PCInvestimentoVariacao { get; set; }

        [Display(Name = "Período da taxa")]
        public TaxaPeriodicidade? INTaxaPeriodicidade { get; set; }
    }
}