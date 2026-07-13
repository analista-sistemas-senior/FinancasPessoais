using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class EmissorMap : IEntityTypeConfiguration<Emissor>
    {
        public void Configure(EntityTypeBuilder<Emissor> builder)
        {
            builder.ToTable("Emissor");
            builder.HasKey(e => e.IDEmissor);
            builder.Property(e => e.NMEmissor).HasMaxLength(255).IsRequired();
        }
    }
}