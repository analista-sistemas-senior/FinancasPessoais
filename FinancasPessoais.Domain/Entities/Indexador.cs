namespace FinancasPessoais.Domain.Entities
{
    public class Indexador
    {
        public int IDIndexador { get; private set; }
        public int IDIndiceFinanceiro { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMIndexador { get; private set; } = string.Empty;
        public string SGIndexador { get; private set; } = string.Empty;
        public decimal PCIndiceFinanceiro { get; private set; }

        public virtual IndiceFinanceiro IndiceFinanceiro { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<InvestimentoTipo> InvestimentosTipos { get; private set; } = [];

        public Indexador() {}
        public Indexador(int idIndexador, int idIndiceFinanceiro, int idUsuario, string nmIndexador, string sgIndexador, decimal pcIndiceFianceiro)
        {
            IDIndexador = idIndexador;
            IDIndiceFinanceiro = idIndiceFinanceiro;
            IDUsuario = idUsuario;
            NMIndexador = nmIndexador;
            SGIndexador = sgIndexador;
            PCIndiceFinanceiro = pcIndiceFianceiro;
        }
    }
}