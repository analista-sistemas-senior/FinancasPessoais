using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Domain.Enums
{
    public enum TipoInvestimentoHistorico : byte
    {
        [Display(Name = "Saldo na data")]
        Saldo  = 1,

        [Display(Name = "Aporte na data")]
        Aporte = 2,

        [Display(Name = "Saque na data")]
        Saque  = 3
    }
}