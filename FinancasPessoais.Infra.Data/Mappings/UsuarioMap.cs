using FinancasPessoais.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FinancasPessoais.Infra.Data.Mapping
{
    public class UsuarioMap : IEntityTypeConfiguration<Usuario>
    {
        public void Configure(EntityTypeBuilder<Usuario> builder)
        {
            builder.ToTable("Usuario");
            builder.HasKey(u => u.IDUsuario);
            builder.Property(u => u.NMUsuario).HasMaxLength(255).IsRequired();
            builder.Property(u => u.NMLogin).HasMaxLength(255).IsRequired();
            builder.Property(u => u.CDSenha).HasMaxLength(512).IsRequired();

            builder.HasMany(u => u.Investimentos).WithOne(i => i.Usuario).HasForeignKey(i => i.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Carteiras).WithOne(c => c.Usuario).HasForeignKey(c => c.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Despesas).WithOne(d => d.Usuario).HasForeignKey(d => d.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.DespesasTipos).WithOne(dt => dt.Usuario).HasForeignKey(dt => dt.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.DespesasFontes).WithOne(df => df.Usuario).HasForeignKey(df => df.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Emissores).WithOne(e => e.Usuario).HasForeignKey(e => e.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.IndicesFinanceiros).WithOne(inf => inf.Usuario).HasForeignKey(inf => inf.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.InvestimentosTipos).WithOne(it => it.Usuario).HasForeignKey(it => it.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Indexadores).WithOne(ix => ix.Usuario).HasForeignKey(ix => ix.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.Receitas).WithOne(r => r.Usuario).HasForeignKey(r => r.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.ReceitasTipos).WithOne(rt => rt.Usuario).HasForeignKey(rt => rt.IDUsuario).OnDelete(DeleteBehavior.Cascade);
            builder.HasMany(u => u.ReceitasFontes).WithOne(rf => rf.Usuario).HasForeignKey(rf => rf.IDUsuario).OnDelete(DeleteBehavior.Cascade);
        }
    }
}