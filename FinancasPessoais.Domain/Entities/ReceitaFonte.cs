namespace FinancasPessoais.Domain.Entities
{
    public class ReceitaFonte
    {
        public int IDReceitaFonte { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMReceitaFonte { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Receita> Receitas { get; private set; } = [];

        public ReceitaFonte() {}
        public ReceitaFonte(int idReceitaFonte, int idUsuario, string nmReceitaFonte)
        {
            IDReceitaFonte = idReceitaFonte;
            IDUsuario = idUsuario;
            NMReceitaFonte = nmReceitaFonte;
        }
    }
}