using FinancasPessoais.Domain.Enums;

namespace FinancasPessoais.Domain.Entities
{
    public class InvestimentoTipo
    {
        public int IDInvestimentoTipo { get; private set; }
        public int? IDIndexador { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMInvestimentoTipo { get; private set; } = string.Empty;
        public string SGInvestimentoTipo { get; private set; } = string.Empty;
        public TipoRentabilidade INTipoRentabilidade { get; private set; }

        public virtual Indexador Indexador { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Investimento> Investimentos { get; private set; } = [];

        public InvestimentoTipo() {}
        public InvestimentoTipo(int idInvestimentoTipo, int? idIndexador, int idUsuario, string nmInvestimentoTipo, string sgInvestimentoTipo, TipoRentabilidade inTipoRentabilidade)
        {
            IDInvestimentoTipo = idInvestimentoTipo;
            IDIndexador = idIndexador;
            IDUsuario = idUsuario;
            NMInvestimentoTipo = nmInvestimentoTipo;
            SGInvestimentoTipo = sgInvestimentoTipo;
            INTipoRentabilidade = inTipoRentabilidade;
        }
    }
}