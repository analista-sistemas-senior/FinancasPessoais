using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class DespesaMap : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder)
        {
            builder.ToTable("Despesa");
            builder.HasKey(d => d.IDDespesa);
            builder.Property(d => d.NMDespesa).HasMaxLength(255).IsRequired();
            builder.Property(d => d.DSDespesa).HasMaxLength(1024).IsRequired(false);
            builder.Property(d => d.DTDespesa).IsRequired();
            builder.Property(d => d.VLDespesa).HasColumnType("numeric(18,2)").IsRequired();

            builder.HasOne(d => d.Carteira).WithMany(c => c.Despesas).HasForeignKey(d => d.IDCarteira).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.DespesaTipo).WithMany(dt => dt.Despesas).HasForeignKey(d => d.IDDespesaTipo).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(d => d.DespesaFonte).WithMany(df => df.Despesas).HasForeignKey(d => d.IDDespesaFonte).OnDelete(DeleteBehavior.Restrict);
        }
    }
}