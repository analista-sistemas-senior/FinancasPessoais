using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class InvestimentoHistoricoMap : IEntityTypeConfiguration<InvestimentoHistorico>
    {
        public void Configure(EntityTypeBuilder<InvestimentoHistorico> builder)
        {
            builder.ToTable("InvestimentoHistorico");
            builder.HasKey(i => i.IDInvestimentoHistorico);
            builder.Property(i => i.DTInvestimentoHistorico).IsRequired();
            builder.Property(i => i.VLInvestimentoHistorico).HasColumnType("numeric(18,2)").IsRequired();
            builder.Property(i => i.INInvestimentoHistorico).HasColumnType("smallint").IsRequired();
        }
    }
}