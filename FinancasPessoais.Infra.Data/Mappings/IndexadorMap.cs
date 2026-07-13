using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class IndexadorMap : IEntityTypeConfiguration<Indexador>
    {
        public void Configure(EntityTypeBuilder<Indexador> builder)
        {
            builder.ToTable("Indexador");
            builder.HasKey(ix => ix.IDIndexador);
            builder.Property(ix => ix.NMIndexador).HasMaxLength(255).IsRequired();
            builder.Property(ix => ix.SGIndexador).HasMaxLength(100).IsRequired();
            builder.Property(ix => ix.PCIndiceFinanceiro).HasColumnType("numeric(10,2)").IsRequired();

            builder.HasOne(ix => ix.IndiceFinanceiro).WithMany(idf => idf.Indexadores).HasForeignKey(ix => ix.IDIndiceFinanceiro).OnDelete(DeleteBehavior.Restrict);
            builder.HasMany(ix => ix.InvestimentosTipos).WithOne(it => it.Indexador).HasForeignKey(it => it.IDIndexador).OnDelete(DeleteBehavior.Restrict);
        }    
    }
}