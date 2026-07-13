using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Domain.Enums
{
    public enum TaxaPeriodicidade : byte
    {
        [Display(Name = "ao dia")]
        AoDia = 1,

        [Display(Name = "ao mês")]
        AoMes = 2,

        [Display(Name = "ao ano")]
        AoAno = 3
    }
}