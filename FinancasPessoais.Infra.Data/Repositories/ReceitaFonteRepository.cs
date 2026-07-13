using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class ReceitaFonteRepository(FinancasPessoaisDbContext context) : IReceitaFonteRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<ReceitaFonte?> RetornarReceitaFontePorId(int idReceitaFonte)
        {
            return await _context.ReceitaFonte.AsNoTracking().FirstOrDefaultAsync(rf => rf.IDReceitaFonte == idReceitaFonte);
        }

        public async Task<ReceitaFonte?> RetornarReceitaFontePorIdEIdUsuario(int idReceitaFonte, int idUsuario)
        {
            return await _context.ReceitaFonte.AsNoTracking().FirstOrDefaultAsync(rf => rf.IDReceitaFonte == idReceitaFonte && rf.IDUsuario == idUsuario);
        }

        public async Task<List<ReceitaFonte>> RetornarReceitasFontesPorIdUsuario(int idUsuario)
        {
            return await _context.ReceitaFonte.AsNoTracking().Where(rf => rf.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ReceitaFonte> CadastrarReceitaFonte(ReceitaFonte receitaFonte)
        {
            _context.ReceitaFonte.Add(receitaFonte);
            await _context.SaveChangesAsync();
            return receitaFonte;
        }

        public async Task<ReceitaFonte> AtualizarReceitaFonte(ReceitaFonte receitaFonte)
        {
            _context.ReceitaFonte.Update(receitaFonte);
            await _context.SaveChangesAsync();
            return receitaFonte;
        }

        public async Task<bool> ExcluirReceitaFonte(int idReceitaFonte, int idUsuario)
        {
            try { return await _context.ReceitaFonte.Where(rf => rf.IDReceitaFonte == idReceitaFonte && rf.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}