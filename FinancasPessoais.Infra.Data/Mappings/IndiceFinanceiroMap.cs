using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class IndiceFinanceiroMap : IEntityTypeConfiguration<IndiceFinanceiro>
    {
        public void Configure(EntityTypeBuilder<IndiceFinanceiro> builder)
        {
            builder.ToTable("IndiceFinanceiro");
            builder.HasKey(inf => inf.IDIndiceFinanceiro);
            builder.Property(inf => inf.NMIndiceFinanceiro).HasMaxLength(255).IsRequired();
            builder.Property(inf => inf.VLIndiceFinanceiro).HasColumnType("numeric(10,2)").IsRequired();
            builder.Property(inf => inf.INTaxaPeriodicidade).HasColumnType("smallint").IsRequired();
        }
    }
}