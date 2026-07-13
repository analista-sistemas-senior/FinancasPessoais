using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Web.ViewModels
{
    public class DespesaViewModel
    {
        [Required]
        public int IDDespesa { get; set; }

        public string? IDDespesaCriptografada { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Saiu da carteira")]
        public int IDCarteira { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Tipo da despesa")]
        public int IDDespesaTipo { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Display(Name = "Fonte da despesa")]
        public int IDDespesaFonte { get; set; }

        [Required]
        public int IDUsuario { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [StringLength(255, MinimumLength = 3, ErrorMessage = "O nome da despesa deve ter entre 3 e 255 caracteres")]
        [Display(Name = "Nome da despesa")]
        public string NMDespesa { get; set; } = string.Empty;

        [StringLength(1024, MinimumLength = 3, ErrorMessage = "A descrição da despesa deve ter entre 3 e 1024 caracteres")]
        [Display(Name = "Descrição da despesa")]
        public string? DSDespesa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [DataType(DataType.Date, ErrorMessage = "Data em formato inválido")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-ddTHH:mm}", ApplyFormatInEditMode = true)]
        [Display(Name = "Data da despesa")]
        public DateTime DTDespesa { get; set; }

        [Required(ErrorMessage = "Campo obrigatório")]
        [Range(0.01, 999999999999.99, ErrorMessage = "O valor deve ser numérico positivo")]
        [Display(Name = "Valor da despesa")]
        public decimal VLDespesa { get; set; }


        [Display(Name = "Carteira")]
        public CarteiraViewModel? CarteiraViewModel { get; set; }

        [Display(Name = "Tipo da despesa")]
        public DespesaTipoViewModel? DespesaTipoViewModel { get; set; }

        [Display(Name = "Fonte da despesa")]
        public DespesaFonteViewModel? DespesaFonteViewModel { get; set; }


        public DespesaViewModel() {}
        public DespesaViewModel(int idDespesa, string? idDespesaCriptografada, int idCarteira, int idDespesaTipo, int idDespesaFonte, int idUsuario, string nmDespesa, string? dsDespesa, DateTime dtDespesa, decimal vlDespesa, CarteiraViewModel? carteiraViewModel, DespesaTipoViewModel? despesaTipoViewModel, DespesaFonteViewModel? despesaFonteViewModel)
        {
            IDDespesa = idDespesa;
            IDDespesaCriptografada = idDespesaCriptografada;
            IDCarteira = idCarteira;
            IDDespesaTipo = idDespesaTipo;
            IDDespesaFonte = idDespesaFonte;
            IDUsuario = idUsuario;
            NMDespesa = nmDespesa;
            DSDespesa = dsDespesa;
            DTDespesa = dtDespesa;
            VLDespesa = vlDespesa;
            CarteiraViewModel = carteiraViewModel;
            DespesaTipoViewModel = despesaTipoViewModel;
            DespesaFonteViewModel = despesaFonteViewModel;
        }
    }
}