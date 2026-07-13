namespace FinancasPessoais.Service.DTOs
{
    public record ReceitaDTO(int IDReceita, int IDCarteira, int IDReceitaTipo, int IDReceitaFonte, int IDUsuario, string NMReceita, string? DSReceita, DateTime DTReceita, decimal VLReceita, CarteiraDTO? CarteiraDTO, ReceitaTipoDTO? ReceitaTipoDTO, ReceitaFonteDTO? ReceitaFonteDTO);
}