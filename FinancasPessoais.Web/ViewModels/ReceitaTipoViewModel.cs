using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class ReceitaTipoViewModel
    {
        [Required]
        public int IDReceitaTipo { get; set; }

        public string? IDReceitaTipoCriptografada { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do tipo da receita deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do tipo da receita")]
        public string NMReceitaTipo { get; set; } = string.Empty;


        public ReceitaTipoViewModel() {}
        public ReceitaTipoViewModel(int idReceitaTipo, string? idReceitaTipoCriptografada, int idUsuario, string nmReceitaTipo)
        {
            IDReceitaTipo = idReceitaTipo;
            IDReceitaTipoCriptografada = idReceitaTipoCriptografada;
            IDUsuario = idUsuario;
            NMReceitaTipo = nmReceitaTipo;
        }
    }
}