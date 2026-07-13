using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities
{
    public class InvestimentoHistorico
    {
        public int IDInvestimentoHistorico { get; private set; }
        public int IDInvestimento { get; private set; }
        public DateTime DTInvestimentoHistorico { get; private set; }
        public decimal VLInvestimentoHistorico { get; private set; }
        public TipoInvestimentoHistorico INInvestimentoHistorico { get; private set; }

        public virtual Investimento Investimento { get; private set; } = null!;

        public InvestimentoHistorico() {}
        public InvestimentoHistorico(int idInvestimentoHistorico, int idInvestimento, DateTime dtInvestimentoHistorico, decimal vlInvestimentoHistorico, TipoInvestimentoHistorico inInvestimentoHistorico)
        {
            IDInvestimentoHistorico = idInvestimentoHistorico;
            IDInvestimento = idInvestimento;
            DTInvestimentoHistorico = dtInvestimentoHistorico;
            VLInvestimentoHistorico = vlInvestimentoHistorico;
            INInvestimentoHistorico = inInvestimentoHistorico;
        }
    }
}