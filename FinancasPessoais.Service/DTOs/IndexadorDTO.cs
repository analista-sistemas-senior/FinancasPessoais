namespace FinancasPessoais.Service.DTOs
{
    public record IndexadorDTO(int IDIndexador, int IDIndiceFinanceiro, int IDUsuario, string NMIndexador, string SGIndexador, decimal PCIndiceFinanceiro, IndiceFinanceiroDTO? IndiceFinanceiroDTO);
}