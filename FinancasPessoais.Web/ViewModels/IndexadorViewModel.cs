using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class IndexadorViewModel
    {
        [Required]
        public int IDIndexador { get; set; }

        public string? IDIndexadorCriptografado { get; set; }

        [Required]
        [Display(Name = "Índice financeiro")]
        public int IDIndiceFinanceiro { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do indexador deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do indexador")]
        public string NMIndexador { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "A sigla do indexador deve ter entre 2 e 100 caracteres")]
        [Display(Name = "Sigla do indexador")]
        public string SGIndexador { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0.00, 9999.99, ErrorMessage = "O valor deve ser numérico")]
        [Display(Name = "Porcentagem do índice financeiro")]
        public decimal PCIndiceFinanceiro { get; set; }


        [Display(Name = "Índice financeiro")]
        public IndiceFinanceiroViewModel? IndiceFinanceiroViewModel { get; set; }


        public IndexadorViewModel() {}
        public IndexadorViewModel(int idIndexador, string? idIndexadorCriptografado, int idIndiceFinanceiro, int idUsuario, string nmIndexador, string sgIndexador, decimal pcIndiceFinanceiro, IndiceFinanceiroViewModel? indiceFinanceiroViewModel)
        {
            IDIndexador = idIndexador;
            IDIndexadorCriptografado = idIndexadorCriptografado;
            IDIndiceFinanceiro = idIndiceFinanceiro;
            IDUsuario = idUsuario;
            NMIndexador = nmIndexador;
            SGIndexador = sgIndexador;
            PCIndiceFinanceiro = pcIndiceFinanceiro;
            IndiceFinanceiroViewModel = indiceFinanceiroViewModel;
        }
    }
}