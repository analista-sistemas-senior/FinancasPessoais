using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class ReceitaTipoMap : IEntityTypeConfiguration<ReceitaTipo>
    {
        public void Configure(EntityTypeBuilder<ReceitaTipo> builder)
        {
            builder.ToTable("ReceitaTipo");
            builder.HasKey(rt => rt.IDReceitaTipo);
            builder.Property(rt => rt.NMReceitaTipo).HasMaxLength(255).IsRequired();
        }
    }
}