using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Service.DTOs
{
    public record IndiceFinanceiroDTO(int IDIndiceFinanceiro, int IDUsuario, string NMIndiceFinanceiro, decimal VLIndiceFinanceiro, TaxaPeriodicidade INTaxaPeriodicidade);
}