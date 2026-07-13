using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class DespesaFonteViewModel
    {
        [Required]
        public int IDDespesaFonte { get; set; }

        public string? IDDespesaFonteCriptografada { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome da fonte de despesa deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da fonte de despesa")]
        public string NMDespesaFonte { get; set; } = string.Empty;


        public DespesaFonteViewModel() {}
        public DespesaFonteViewModel(int idDespesaFonte, string? idDespesaFonteCriptografada, int idUsuario, string nmDespesaFonte)
        {
            IDDespesaFonte = idDespesaFonte;
            IDDespesaFonteCriptografada = idDespesaFonteCriptografada;
            IDUsuario = idUsuario;
            NMDespesaFonte = nmDespesaFonte;
        }
    }
}