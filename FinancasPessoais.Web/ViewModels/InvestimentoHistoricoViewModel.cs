using FinancasPessoais.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class InvestimentoHistoricoViewModel
    {
        [Required]
        public int IDInvestimentoHistorico { get; set; }

        public string? IDInvestimentoHistoricoCriptografado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Investimento")]
        public int IDInvestimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do histórico")]
        public DateTime DTInvestimentoHistorico { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(-999999999999.99, 999999999999.99, ErrorMessage = "O valor deve ser numérico")]
        [Display(Name = "Valor do histórico")]
        public decimal VLInvestimentoHistorico { get; set; }

        [Display(Name = "Tipo do histórico")]
        public TipoInvestimentoHistorico INInvestimentoHistorico { get; set; }


        [Display(Name = "Investimento")]
        public InvestimentoViewModel? InvestimentoViewModel { get; set; }


        public InvestimentoHistoricoViewModel() {}
        public InvestimentoHistoricoViewModel(int idInvestimentoHistorico, string? idInvestimentoHistoricoCriptografado, int idInvestimento, DateTime dtInvestimentoHistorico, decimal vlInvestimentoHistorico, TipoInvestimentoHistorico inInvestimentoHistorico, InvestimentoViewModel? investimentoViewModel)
        {
            IDInvestimentoHistorico = idInvestimentoHistorico;
            IDInvestimentoHistoricoCriptografado = idInvestimentoHistoricoCriptografado;
            IDInvestimento = idInvestimento;
            DTInvestimentoHistorico = dtInvestimentoHistorico;
            VLInvestimentoHistorico = vlInvestimentoHistorico;
            INInvestimentoHistorico = inInvestimentoHistorico;
            InvestimentoViewModel = investimentoViewModel;
        }
    }
}