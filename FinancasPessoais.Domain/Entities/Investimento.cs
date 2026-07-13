using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities
{
    public class Investimento
    {
        public int IDInvestimento { get; private set; }
        public int IDInvestimentoTipo { get; private set; }
        public int IDEmissor { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMInvestimento { get; private set; } = string.Empty;
        public decimal VLInvestimento { get; private set; }
        public DateTime DTInvestimento { get; private set; }
        public decimal VLSaldo { get; private set; }
        public DateTime? DTVencimento { get; private set; }
        public decimal? PCTaxaRentabilidade { get; private set; }
        public TaxaPeriodicidade? INTaxaPeriodicidade { get; private set; }
        public string? TXAnotacao { get; private set; }
        public bool? FLLiquidado { get; private set; }

        public virtual InvestimentoTipo InvestimentoTipo { get; private set; } = null!;
        public virtual Emissor Emissor { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<InvestimentoHistorico> InvestimentosHistoricos { get; private set; } = [];

        public Investimento() {}
        public Investimento(int idInvestimento, int idInvestimentoTipo, int idEmissor, int idUsuario, string nmInvestimento, decimal vlInvestimento, DateTime dtInvestimento, decimal vLSaldo, DateTime? dtVencimento, decimal? pcTaxaRentabilidade, TaxaPeriodicidade? inTaxaPeriodicidade, string? txAnotacao, bool? flLiquidado)
        {
            IDInvestimento = idInvestimento;
            IDInvestimentoTipo = idInvestimentoTipo;
            IDEmissor = idEmissor;
            IDUsuario = idUsuario;
            NMInvestimento = nmInvestimento;
            VLInvestimento = vlInvestimento;
            DTInvestimento = dtInvestimento;
            VLSaldo = vLSaldo;
            DTVencimento = dtVencimento;
            PCTaxaRentabilidade = pcTaxaRentabilidade;
            INTaxaPeriodicidade = inTaxaPeriodicidade;
            TXAnotacao = txAnotacao;
            FLLiquidado = flLiquidado;
        }
        public void AtualizarSaldo(decimal vLSaldo)
        {
            VLSaldo = vLSaldo;
        }
    }
}