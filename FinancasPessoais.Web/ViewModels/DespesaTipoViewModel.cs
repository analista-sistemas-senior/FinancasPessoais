using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class DespesaTipoViewModel
    {
        [Required]
        public int IDDespesaTipo { get; set; }

        public string? IDDespesaTipoCriptografado { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome do tipo da despesa deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome do tipo da despesa")]
        public string NMDespesaTipo { get; set; } = string.Empty;


        public DespesaTipoViewModel() {}
        public DespesaTipoViewModel(int idDespesaTipo, string? idDespesaTipoCriptografado, int idUsuario, string nmDespesaTipo)
        {
            IDDespesaTipo = idDespesaTipo;
            IDDespesaTipoCriptografado = idDespesaTipoCriptografado;
            IDUsuario = idUsuario;
            NMDespesaTipo = nmDespesaTipo;
        }
    }
}