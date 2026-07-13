using FinancasPessoais.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class InvestimentoViewModel
    {
        [Required]
        public int IDInvestimento { get; set; }

        public string? IDInvestimentoCriptografado { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo de investimento")]
        public int IDInvestimentoTipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Emissor")]
        public int IDEmissor { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do investimento deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do investimento")]
        public string NMInvestimento { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0.01, 999999999999.99, ErrorMessage = "O valor deve ser numérico positivo")]
        [Display(Name = "Valor do investimento")]
        public decimal VLInvestimento { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do investimento")]
        public DateTime DTInvestimento { get; set; }

        [Range(-999999999999.99, 999999999999.99, ErrorMessage = "O valor deve ser numérico")]
        [Display(Name = "Saldo")]
        public decimal VLSaldo { get; set; }

        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data do vencimento")]
        public DateTime? DTVencimento { get; set; }

        [Range(0.01, 999999999999.99, ErrorMessage = "O valor deve ser numérico positivo")]
        [Display(Name = "Taxa de rentabilidade")]
        public decimal? PCTaxaRentabilidade { get; set; }

        [Display(Name = "Período da taxa")]
        public TaxaPeriodicidade? INTaxaPeriodicidade { get; set; }

        [Display(Name = "Anotação")]
        public string? TXAnotacao { get; set; }

        [Display(Name = "Liquidado")]
        public bool? FLLiquidado { get; set; }


        [Display(Name = "Tipo de investimento")]
        public InvestimentoTipoViewModel? InvestimentoTipoViewModel { get; set; }

        [Display(Name = "Emissor")]
        public EmissorViewModel? EmissorViewModel { get; set; }


        public InvestimentoViewModel() {}
        public InvestimentoViewModel(int idInvestimento, string? idInvestimentoCriptografado, int idInvestimentoTipo, int idEmissor, int idUsuario, string nmInvestimento, decimal vlInvestimento, DateTime dtInvestimento, decimal vlSaldo, DateTime? dtVencimento, decimal? pcTaxaRentabilidade, TaxaPeriodicidade? inTaxaPeriodicidade, string? txAnotacao, bool? flLiquidado, InvestimentoTipoViewModel? investimentoTipoViewModel, EmissorViewModel? emissorViewModel)
        {
            IDInvestimento = idInvestimento;
            IDInvestimentoCriptografado = idInvestimentoCriptografado;
            IDInvestimentoTipo = idInvestimentoTipo;
            IDEmissor = idEmissor;
            IDUsuario = idUsuario;
            NMInvestimento = nmInvestimento;
            DTInvestimento = dtInvestimento;
            VLInvestimento = vlInvestimento;
            DTVencimento = dtVencimento;
            PCTaxaRentabilidade = pcTaxaRentabilidade;
            INTaxaPeriodicidade = inTaxaPeriodicidade;
            TXAnotacao = txAnotacao;
            FLLiquidado = flLiquidado;
            VLSaldo = vlSaldo;
            InvestimentoTipoViewModel = investimentoTipoViewModel;
            EmissorViewModel = emissorViewModel;
        }
    }
}