using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class DespesaFonteMap : IEntityTypeConfiguration<DespesaFonte>
    {
        public void Configure(EntityTypeBuilder<DespesaFonte> builder)
        {
            builder.ToTable("DespesaFonte");
            builder.HasKey(df => df.IDDespesaFonte);
            builder.Property(df => df.NMDespesaFonte).HasMaxLength(255).IsRequired();
        }
    }
}