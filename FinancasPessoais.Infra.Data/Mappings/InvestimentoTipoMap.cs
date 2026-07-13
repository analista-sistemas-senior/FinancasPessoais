using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class InvestimentoTipoMap : IEntityTypeConfiguration<InvestimentoTipo>
    {
        public void Configure(EntityTypeBuilder<InvestimentoTipo> builder)
        {
            builder.ToTable("InvestimentoTipo");
            builder.HasKey(it => it.IDInvestimentoTipo);
            builder.Property(it => it.NMInvestimentoTipo).HasMaxLength(255).IsRequired();
            builder.Property(it => it.SGInvestimentoTipo).HasMaxLength(100).IsRequired();
            builder.Property(it => it.INTipoRentabilidade).HasColumnType("smallint").IsRequired();
        }
    }
}