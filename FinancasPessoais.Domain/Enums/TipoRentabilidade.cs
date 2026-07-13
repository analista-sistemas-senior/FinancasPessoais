using System.ComponentModel.DataAnnotations;

namespace FinancasPessoais.Domain.Enums
{
    public enum TipoRentabilidade : byte
    {
        [Display(Name = "Não aplicável")]
        NA = 1,

        [Display(Name = "Pré-Fixado")]
        PreFixado = 2,

        [Display(Name = "Pós-Fixado")]
        PosFixado = 3,

        [Display(Name = "Híbrido")]
        Hibrido = 4,
    }
}