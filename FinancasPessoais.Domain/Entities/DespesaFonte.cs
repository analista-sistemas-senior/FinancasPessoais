namespace FinancasPessoais.Domain.Entities
{
    public class DespesaFonte
    {
        public int IDDespesaFonte { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMDespesaFonte { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Despesa> Despesas { get; private set; } = [];

        public DespesaFonte() {}
        public DespesaFonte(int idDespesaFonte, int idUsuario, string nmDespesaFonte)
        {
            IDDespesaFonte = idDespesaFonte;
            IDUsuario = idUsuario;
            NMDespesaFonte = nmDespesaFonte;
        }
    }
}