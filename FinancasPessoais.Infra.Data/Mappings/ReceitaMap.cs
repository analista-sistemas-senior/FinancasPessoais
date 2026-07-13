using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class ReceitaMap : IEntityTypeConfiguration<Receita>
    {
        public void Configure(EntityTypeBuilder<Receita> builder)
        {
            builder.ToTable("Receita");
            builder.HasKey(r => r.IDReceita);
            builder.Property(r => r.NMReceita).HasMaxLength(255).IsRequired();
            builder.Property(r => r.DSReceita).HasMaxLength(1024).IsRequired(false);
            builder.Property(r => r.DTReceita).IsRequired();
            builder.Property(r => r.VLReceita).HasColumnType("numeric(18,2)").IsRequired();

            builder.HasOne(r => r.Carteira).WithMany(c => c.Receitas).HasForeignKey(r => r.IDCarteira).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.ReceitaTipo).WithMany(rt => rt.Receitas).HasForeignKey(r => r.IDReceitaTipo).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(r => r.ReceitaFonte).WithMany(rf => rf.Receitas).HasForeignKey(r => r.IDReceitaFonte).OnDelete(DeleteBehavior.Restrict);
        }
    }
}