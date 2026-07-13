using FinancasPessoais.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class IndiceFinanceiroViewModel
    {
        [Required]
        public int IDIndiceFinanceiro { get; set; }

        public string? IDIndiceFinanceiroCriptografado { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do índice financeiro deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do índice")]
        public string NMIndiceFinanceiro { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(-999999999999.99, 999999999999.99, ErrorMessage = "O valor deve ser numérico")]
        [Display(Name = "Valor do índice em porcentagem")]
        public decimal VLIndiceFinanceiro { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Período da taxa")]
        public TaxaPeriodicidade INTaxaPeriodicidade { get; set; }


        public IndiceFinanceiroViewModel() {}
        public IndiceFinanceiroViewModel(int idIndiceFinanceiro, string? idIndiceFinanceiroCriptografado, int idUsuario, string nmIndiceFinanceiro, decimal vlIndiceFinanceiro, TaxaPeriodicidade inTaxaJuro)
        {
            IDIndiceFinanceiro = idIndiceFinanceiro;
            IDIndiceFinanceiroCriptografado = idIndiceFinanceiroCriptografado;
            IDUsuario = idUsuario;
            NMIndiceFinanceiro = nmIndiceFinanceiro;
            VLIndiceFinanceiro = vlIndiceFinanceiro;
            INTaxaPeriodicidade = inTaxaJuro;
        }
    }
}