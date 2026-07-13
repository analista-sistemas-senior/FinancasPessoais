using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class UsuarioViewModel
    {
        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 5, ErrorMessage = "O nome deve ter entre 5 e 255 caracteres")]
        [Display(Name = "Nome")]
        public string NMUsuario { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O login deve ter entre 3 e 255 caracteres")]
        [RegularExpression(@"^[a-zA-Z0-9]+$", ErrorMessage = "Use apenas letras e números")]
        [Display(Name = "Login")]
        public string NMLogin { get; set; } = string.Empty;

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(50, MinimumLength = 6, ErrorMessage = "A senha deve ter entre 6 e 50 caracteres")]
        [DataType(DataType.Password)]
        [Display(Name = "Senha")]
        public string CDSenha { get; set; } = string.Empty;

        [Required(ErrorMessage = "A confirmação da senha é obrigatória")]
        [DataType(DataType.Password)]
        [Display(Name = "Confirme a senha")]
        [Compare("CDSenha", ErrorMessage = "As senhas digitadas não conferem")]
        public string ConfirmarSenha { get; set; } = string.Empty;


        public UsuarioViewModel() {}
        public UsuarioViewModel(int idUsuario, string nmUsuario, string nmLogin, string cdSenha) {
            IDUsuario = idUsuario;
            NMUsuario = nmUsuario;
            NMLogin = nmLogin;
            CDSenha = cdSenha;
        }
    }
}