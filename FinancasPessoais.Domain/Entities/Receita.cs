namespace FinancasPessoais.Domain.Entities
{
    public class Receita
    {
        public int IDReceita { get; private set; }
        public int IDCarteira { get; private set; }
        public int IDReceitaTipo { get; private set; }
        public int IDReceitaFonte { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMReceita { get; private set; } = string.Empty;
        public string? DSReceita { get; private set; }
        public DateTime DTReceita { get; private set; }
        public decimal VLReceita { get; private set; }

        public virtual Carteira Carteira { get; private set; } = null!;
        public virtual ReceitaTipo ReceitaTipo { get; private set; } = null!;
        public virtual ReceitaFonte ReceitaFonte { get; private set; } = null!;
        public virtual Usuario Usuario { get; private set; } = null!;

        public Receita() {}
        public Receita(int idReceita, int idCarteira, int idReceitaTipo, int idReceitaFonte, int idUsuario, string nmReceita, string? dsReceita, DateTime dtReceita, decimal vlReceita)
        {
            IDReceita = idReceita;
            IDCarteira = idCarteira;
            IDReceitaTipo = idReceitaTipo;
            IDReceitaFonte = idReceitaFonte;
            IDUsuario = idUsuario;
            NMReceita = nmReceita;
            DSReceita = dsReceita;
            DTReceita = dtReceita;
            VLReceita = vlReceita;
        }
    }
}