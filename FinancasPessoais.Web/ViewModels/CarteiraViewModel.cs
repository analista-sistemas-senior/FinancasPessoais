using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class CarteiraViewModel
    {
        [Required]
        public int IDCarteira { get; set; }

        public string? IDCarteiraCriptografada { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome da carteira deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da carteira")]
        public string NMCarteira { get; set; } = string.Empty;


        public CarteiraViewModel() {}
        public CarteiraViewModel(int idCarteira, string? idCarteiraCriptografada, int idUsuario, string nmCarteira)
        {
            IDCarteira = idCarteira;
            IDCarteiraCriptografada = idCarteiraCriptografada;
            IDUsuario = idUsuario;
            NMCarteira = nmCarteira;
        }
    }
}