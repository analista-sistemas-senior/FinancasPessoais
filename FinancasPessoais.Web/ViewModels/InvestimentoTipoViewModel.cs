using FinancasPessoais.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class InvestimentoTipoViewModel
    {
        [Required]
        public int IDInvestimentoTipo { get; set; }

        public string? IDInvestimentoTipoCriptografado { get; set; }

        [Display(Name = "Indexador")]
        public int? IDIndexador { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do tipo de investimento deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do tipo de investimento")]
        public string NMInvestimentoTipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O nome da sigla do investimento deve ter entre 2 e 100 caracteres")]
        [Display(Name = "Sigla do tipo de investimento")]
        public string SGInvestimentoTipo { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo de rentabilidade")]
        public TipoRentabilidade INTipoRentabilidade { get; set; }


        [Display(Name = "Indexador")]
        public IndexadorViewModel? IndexadorViewModel { get; set; }


        public InvestimentoTipoViewModel() {}
        public InvestimentoTipoViewModel(int idInvestimentoTipo, string? idInvestimentoTipoCriptografado, int? idIndexador, int idUsuario, string nmInvestimentoTipo, string sgInvestimentoTipo, TipoRentabilidade inTipoRentabilidade, IndexadorViewModel? indexadorViewModel)
        {
            IDInvestimentoTipo = idInvestimentoTipo;
            IDInvestimentoTipoCriptografado = idInvestimentoTipoCriptografado;
            IDIndexador = idIndexador;
            IDUsuario = idUsuario;
            NMInvestimentoTipo = nmInvestimentoTipo;
            SGInvestimentoTipo = sgInvestimentoTipo;
            INTipoRentabilidade = inTipoRentabilidade;
            IndexadorViewModel = indexadorViewModel;
        }
    }
}