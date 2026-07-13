using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class ReceitaFonteViewModel
    {
        [Required]
        public int IDReceitaFonte { get; set; }

        public string? IDReceitaFonteCriptografada { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome da fonte de receita deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da fonte de receita")]
        public string NMReceitaFonte { get; set; } = string.Empty;


        public ReceitaFonteViewModel() {}
        public ReceitaFonteViewModel(int idReceitaFonte, string? idReceitaFonteCriptografada, int idUsuario, string nmReceitaFonte)
        {
            IDReceitaFonte = idReceitaFonte;
            IDReceitaFonteCriptografada = idReceitaFonteCriptografada;
            IDUsuario = idUsuario;
            NMReceitaFonte = nmReceitaFonte;
        }
    }
}