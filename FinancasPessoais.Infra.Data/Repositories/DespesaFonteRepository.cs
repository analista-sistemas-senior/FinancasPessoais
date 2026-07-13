using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class DespesaFonteRepository(FinancasPessoaisDbContext context) : IDespesaFonteRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<DespesaFonte?> RetornarDespesaFontePorId(int idDespesaFonte)
        {
            return await _context.DespesaFonte.AsNoTracking().FirstOrDefaultAsync(df => df.IDDespesaFonte == idDespesaFonte);
        }

        public async Task<DespesaFonte?> RetornarDespesaFontePorIdEIdUsuario(int idDespesaFonte, int idUsuario)
        {
            return await _context.DespesaFonte.AsNoTracking().FirstOrDefaultAsync(df => df.IDDespesaFonte == idDespesaFonte && df.IDUsuario == idUsuario);
        }

        public async Task<List<DespesaFonte>> RetornarDespesasFontesPorIdUsuario(int idUsuario)
        {
            return await _context.DespesaFonte.AsNoTracking().Where(df => df.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<DespesaFonte> CadastrarDespesaFonte(DespesaFonte despesaFonte)
        {
            _context.DespesaFonte.Add(despesaFonte);
            await _context.SaveChangesAsync();
            return despesaFonte;
        }

        public async Task<DespesaFonte> AtualizarDespesaFonte(DespesaFonte despesaFonte)
        {
            _context.DespesaFonte.Update(despesaFonte);
            await _context.SaveChangesAsync();
            return despesaFonte;
        }

        public async Task<bool> ExcluirDespesaFonte(int idDespesaFonte, int idUsuario)
        {
            try { return await _context.DespesaFonte.Where(df => df.IDDespesaFonte == idDespesaFonte && df.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}