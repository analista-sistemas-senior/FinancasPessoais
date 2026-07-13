namespace FinancasPessoais.Domain.Entities
{
    public class Usuario
    {
        public int IDUsuario { get; private set; }
        public string NMUsuario { get; private set; } = string.Empty;
        public string NMLogin { get; private set; } = string.Empty;
        public string CDSenha { get; private set; } = string.Empty;

        public virtual ICollection<Despesa> Despesas { get; private set; } = [];
        public virtual ICollection<DespesaFonte> DespesasFontes { get; private set; } = [];
        public virtual ICollection<DespesaTipo> DespesasTipos { get; private set; } = [];
        public virtual ICollection<Investimento> Investimentos { get; private set; } = [];
        public virtual ICollection<Carteira> Carteiras { get; private set; } = [];
        public virtual ICollection<Emissor> Emissores { get; private set; } = [];
        public virtual ICollection<IndiceFinanceiro> IndicesFinanceiros { get; private set; } = [];
        public virtual ICollection<InvestimentoTipo> InvestimentosTipos { get; private set; } = [];
        public virtual ICollection<Indexador> Indexadores { get; private set; } = [];
        public virtual ICollection<Receita> Receitas { get; private set; } = [];
        public virtual ICollection<ReceitaFonte> ReceitasFontes { get; private set; } = [];
        public virtual ICollection<ReceitaTipo> ReceitasTipos { get; private set; } = [];

        public Usuario() {}
        public Usuario(int idUsuario, string nmUsuario, string nmLogin, string cdSenha)
        {
            IDUsuario = idUsuario;
            NMUsuario = nmUsuario;
            NMLogin = nmLogin;
            CDSenha = cdSenha;
        }
        public void DefinirSenhaCriptografada(string cdSenha)
        {
            CDSenha = cdSenha;
        }
    }
}