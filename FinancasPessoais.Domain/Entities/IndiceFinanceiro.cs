using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities
{
    public class IndiceFinanceiro
    {
        public int IDIndiceFinanceiro { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMIndiceFinanceiro { get; private set; } = string.Empty;
        public decimal VLIndiceFinanceiro { get; private set; }
        public TaxaPeriodicidade INTaxaPeriodicidade { get; private set; }

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Indexador> Indexadores { get; private set; } = [];

        public IndiceFinanceiro() {}
        public IndiceFinanceiro(int idIndiceFinanceiro, int idUsuario, string nmIndiceFinanceiro, decimal vlIndiceFinanceiro, TaxaPeriodicidade inTaxaPeriodicidade)
        {
            IDIndiceFinanceiro = idIndiceFinanceiro;
            IDUsuario = idUsuario;
            NMIndiceFinanceiro = nmIndiceFinanceiro;
            VLIndiceFinanceiro = vlIndiceFinanceiro;
            INTaxaPeriodicidade = inTaxaPeriodicidade;
        }
    }
}