using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class DespesaTipoMap : IEntityTypeConfiguration<DespesaTipo>
    {
        public void Configure(EntityTypeBuilder<DespesaTipo> builder)
        {
            builder.ToTable("DespesaTipo");
            builder.HasKey(dt => dt.IDDespesaTipo);
            builder.Property(dt => dt.NMDespesaTipo).HasMaxLength(255).IsRequired();
        }
    }
}