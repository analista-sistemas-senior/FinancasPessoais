namespace FinancasPessoais.Domain.Entities
{
    public class ReceitaTipo
    {
        public int IDReceitaTipo { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMReceitaTipo { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Receita> Receitas { get; private set; } = [];

        public ReceitaTipo() {}
        public ReceitaTipo(int idReceitaTipo, int idUsuario, string nmReceitaTipo)
        {
            IDReceitaTipo = idReceitaTipo;
            IDUsuario = idUsuario;
            NMReceitaTipo = nmReceitaTipo;
        }
    }
}