using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class EmissorViewModel
    {
        [Required]
        public int IDEmissor { get; set; }

        public string? IDEmissorCriptografado { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do emissor deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da emissor")]
        public string NMEmissor { get; set; } = string.Empty;


        public EmissorViewModel() {}
        public EmissorViewModel(int idEmissor, string? idEmissorCriptografado, int idUsuario, string nmEmissor)
        {
            IDEmissor = idEmissor;
            IDEmissorCriptografado = idEmissorCriptografado;
            IDUsuario = idUsuario;
            NMEmissor = nmEmissor;
        }
    }
}