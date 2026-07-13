using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Context
{
    public class FinancasPessoaisDbContext(DbContextOptions<FinancasPessoaisDbContext> options) : DbContext(options)
    { 
        public DbSet<Usuario> Usuario { get; set; }
        public DbSet<Despesa> Despesa { get; set; }
        public DbSet<DespesaTipo> DespesaTipo { get; set; }
        public DbSet<DespesaFonte> DespesaFonte { get; set; }
        public DbSet<Carteira> Carteira { get; set; }
        public DbSet<Emissor> Emissor { get; set; }
        public DbSet<Investimento> Investimento { get; set; }
        public DbSet<InvestimentoTipo> InvestimentoTipo { get; set; }
        public DbSet<Indexador> Indexador { get; set; }
        public DbSet<IndiceFinanceiro> IndiceFinanceiro { get; set; }
        public DbSet<InvestimentoHistorico> InvestimentoHistorico { get; set; }
        public DbSet<Receita> Receita { get; set; }
        public DbSet<ReceitaTipo> ReceitaTipo { get; set; }
        public DbSet<ReceitaFonte> ReceitaFonte { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(FinancasPessoaisDbContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }
    }
}