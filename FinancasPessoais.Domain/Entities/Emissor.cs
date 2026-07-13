namespace FinancasPessoais.Domain.Entities
{
    public class Emissor
    {
        public int IDEmissor { get; private set; }
        public int IDUsuario { get; private set; }
        public string NMEmissor { get; private set; } = string.Empty;

        public virtual Usuario Usuario { get; private set; } = null!;
        public virtual ICollection<Investimento> Investimentos { get; private set; } = [];

        public Emissor() {}
        public Emissor(int idEmissor, int idUsuario, string nmEmissor)
        {
            IDEmissor = idEmissor;
            IDUsuario = idUsuario;
            NMEmissor = nmEmissor;
        }
    }
}