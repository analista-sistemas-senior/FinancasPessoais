namespace FinancasPessoais.Service.DTOs
{
    public record DespesaDTO(int IDDespesa, int IDCarteira, int IDDespesaTipo, int IDDespesaFonte, int IDUsuario, string NMDespesa, string? DSDespesa, DateTime DTDespesa, decimal VLDespesa, CarteiraDTO? CarteiraDTO, DespesaTipoDTO? DespesaTipoDTO, DespesaFonteDTO? DespesaFonteDTO);
}