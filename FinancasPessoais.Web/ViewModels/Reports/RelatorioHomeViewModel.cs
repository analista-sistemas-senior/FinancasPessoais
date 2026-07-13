namespace FinancasPessoais.Web.ViewModels.Reports
{
    public class RelatorioHomeViewModel
    {
        public string TotalInvestimento { get; set; } = string.Empty;
        public string TotalJuro { get; set; } = string.Empty;
        public string TotalAporte { get; set; } = string.Empty;
        public string TotalSaque { get; set; } = string.Empty;
        public string TotalDespesa { get; set; } = string.Empty;
        public string TotalReceita { get; set; } = string.Empty;

        public string Investimentos12MesesRotulosJson { get; set; } = "[]";
        public string Investimentos12MesesValoresJson { get; set; } = "[]";
        public string FluxoCaixa12MesesRotulosJson { get; set; } = "[]";
        public string FluxoCaixa12MesesReceitasJson { get; set; } = "[]";
        public string FluxoCaixa12MesesDespesasJson { get; set; } = "[]";
        public string InvestimentosPorTiposJson { get; set; } = "[]";
        public string DespesasPorFontesJson { get; set; } = "[]";
    }
}