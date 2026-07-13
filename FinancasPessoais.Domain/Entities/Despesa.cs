namespace FinancasPessoais.Domain.Entities
{
    public class Despesa
    {
        public int IDDespesa { get; private set; }
        public int IDCarteira { get; private set; }
        public int IDDespesaTipo { get; private set; }
        public int IDDespesaFonte { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMDespesa { get; private set; } = string.Empty;
        public string? DSDespesa { get; private set; }
        public DateTime DTDespesa { get; private set; }
        public decimal VLDespesa { get; private set; }

        public virtual Carteira Carteira { get; private set; } = null!;
        public virtual DespesaTipo DespesaTipo { get; private set; } = null!;
        public virtual DespesaFonte DespesaFonte { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;

        public Despesa() {}
        public Despesa(int idDespesa, int idCarteira, int idDespesaTipo, int idDespesaFonte, int idUsuario, string nmDespesa, string? dsDespesa, DateTime dtDespesa, decimal vlDespesa)
        {
            IDDespesa = idDespesa;
            IDCarteira = idCarteira;
            IDDespesaTipo = idDespesaTipo;
            IDDespesaFonte = idDespesaFonte;
            IDUsuario = idUsuario;
            NMDespesa = nmDespesa;
            DSDespesa = dsDespesa;
            DTDespesa = dtDespesa;
            VLDespesa = vlDespesa;
        }
    }
}