using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Service.DTOs
{
    public record InvestimentoTipoDTO(int IDInvestimentoTipo, int? IDIndexador, int IDUsuario, string NMInvestimentoTipo, string SGInvestimentoTipo, TipoRentabilidade INTipoRentabilidade, IndexadorDTO? IndexadorDTO);
}