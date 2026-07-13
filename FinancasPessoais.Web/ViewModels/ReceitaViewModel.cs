using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class ReceitaViewModel
    {
        [Required]
        public int IDReceita { get; set; }

        public string? IDReceitaCriptografada { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Entrou na carteira")]
        public int IDCarteira { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo da receita")]
        public int IDReceitaTipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Fonte da receita")]
        public int IDReceitaFonte { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome da receita deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da receita")]
        public string NMReceita { get; set; } = string.Empty;

        [StringLength(1024, MinimumLength = 3, ErrorMessage = "A descrição da receita deve ter entre 3 e 1024 caracteres")]
        [Display(Name = "Descrição da receita")]
        public string? DSReceita { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da receita")]
        public DateTime DTReceita { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0.01, 999999999999.99, ErrorMessage = "O valor deve ser numérico positivo")]
        [Display(Name = "Valor da receita")]
        public decimal VLReceita { get; set; }


        [Display(Name = "Carteira")]
        public CarteiraViewModel? CarteiraViewModel { get; set; }

        [Display(Name = "Tipo da receita")]
        public ReceitaTipoViewModel? ReceitaTipoViewModel { get; set; }

        [Display(Name = "Fonte da receita")]
        public ReceitaFonteViewModel? ReceitaFonteViewModel { get; set; }


        public ReceitaViewModel() {}
        public ReceitaViewModel(int idReceita, string? idReceitaCriptografada, int idCarteira, int idReceitaTipo, int idReceitaFonte, int idUsuario, string nmReceita, string? dsReceita, DateTime dtReceita, decimal vlReceita, CarteiraViewModel? carteiraViewModel, ReceitaTipoViewModel? receitaTipoViewModel, ReceitaFonteViewModel? receitaFonteViewModel)
        {
            IDReceita = idReceita;
            IDReceitaCriptografada = idReceitaCriptografada;
            IDCarteira = idCarteira;
            IDReceitaTipo = idReceitaTipo;
            IDReceitaFonte = idReceitaFonte;
            IDUsuario = idUsuario;
            NMReceita = nmReceita;
            DSReceita = dsReceita;
            DTReceita = dtReceita;
            VLReceita = vlReceita;
            ReceitaTipoViewModel = receitaTipoViewModel;
            ReceitaFonteViewModel = receitaFonteViewModel;
            CarteiraViewModel = carteiraViewModel;
        }
    }
}