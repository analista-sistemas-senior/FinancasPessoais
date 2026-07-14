namespace FinancasPessoais.Domain.Entities
{
    public class Carteira
    {
        public int IDCarteira { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMCarteira { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Receita> Receitas { get; private set; } = [];
        public virtual ICollection<Despesa> Despesas { get; private set; } = [];

        public Carteira() {}
        public Carteira(int idCarteira, int idUsuario, string nmCarteira)
        {
            IDCarteira = idCarteira;
            IDUsuario = idUsuario;
            NMCarteira = nmCarteira;
        }
    }
}