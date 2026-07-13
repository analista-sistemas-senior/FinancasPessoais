namespace FinancasPessoais.Domain.Entities
{
    public class DespesaTipo
    {
        public int IDDespesaTipo { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMDespesaTipo { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Despesa> Despesas { get; private set; } = [];

        public DespesaTipo() {}
        public DespesaTipo(int idDespesaTipo, int idUsuario, string nmDespesaTipo)
        {
            IDDespesaTipo = idDespesaTipo;
            IDUsuario = idUsuario;
            NMDespesaTipo = nmDespesaTipo;
        }
    }
}