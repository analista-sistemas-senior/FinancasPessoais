using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class ReceitaFonteMap : IEntityTypeConfiguration<ReceitaFonte>
    {
        public void Configure(EntityTypeBuilder<ReceitaFonte> builder)
        {
            builder.ToTable("ReceitaFonte");
            builder.HasKey(rf => rf.IDReceitaFonte);
            builder.Property(rf => rf.NMReceitaFonte).HasMaxLength(255).IsRequired();
        }
    }
}