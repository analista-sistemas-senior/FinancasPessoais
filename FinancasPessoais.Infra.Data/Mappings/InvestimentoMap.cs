using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class InvestimentoMap : IEntityTypeConfiguration<Investimento>
    {
        public void Configure(EntityTypeBuilder<Investimento> builder)
        {
            builder.ToTable("Investimento");
            builder.HasKey(i => i.IDInvestimento);
            builder.Property(i => i.NMInvestimento).HasMaxLength(255).IsRequired();
            builder.Property(i => i.VLInvestimento).HasColumnType("numeric(18,2)").IsRequired();
            builder.Property(i => i.DTInvestimento).IsRequired();
            builder.Property(i => i.DTVencimento).HasColumnType("date").IsRequired(false);
            builder.Property(i => i.PCTaxaRentabilidade).HasColumnType("numeric(10,2)").IsRequired(false);
            builder.Property(i => i.INTaxaPeriodicidade).HasColumnType("smallint").IsRequired(false);
            builder.Property(i => i.TXAnotacao).HasMaxLength(1024).IsRequired(false);
            builder.Property(i => i.FLLiquidado).HasColumnType("smallint").IsRequired(false);

            builder.HasOne(i => i.Emissor).WithMany(e => e.Investimentos).HasForeignKey(i => i.IDEmissor).OnDelete(DeleteBehavior.Restrict);
            builder.HasOne(i => i.InvestimentoTipo).WithMany(it => it.Investimentos).HasForeignKey(i => i.IDInvestimentoTipo).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(i => i.InvestimentosHistoricos).WithOne(ih => ih.Investimento).HasForeignKey(ih => ih.IDInvestimento).OnDelete(DeleteBehavior.Cascade);
        }
    }
}