using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class ReceitaTipoRepository(FinancasPessoaisDbContext context) : IReceitaTipoRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<ReceitaTipo?> RetornarReceitaTipoPorId(int idReceitaTipo)
        {
            return await _context.ReceitaTipo.AsNoTracking().FirstOrDefaultAsync(rt => rt.IDReceitaTipo == idReceitaTipo);
        }

        public async Task<ReceitaTipo?> RetornarReceitaTipoPorIdEIdUsuario(int idReceitaTipo, int idUsuario)
        {
            return await _context.ReceitaTipo.AsNoTracking().FirstOrDefaultAsync(rt => rt.IDReceitaTipo == idReceitaTipo && rt.IDUsuario == idUsuario);
        }

        public async Task<List<ReceitaTipo>> RetornarReceitasTiposPorIdUsuario(int idUsuario)
        {
            return await _context.ReceitaTipo.AsNoTracking().Where(rt => rt.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<ReceitaTipo> CadastrarReceitaTipo(ReceitaTipo receitaTipo)
        {
            _context.ReceitaTipo.Add(receitaTipo);
            await _context.SaveChangesAsync();
            return receitaTipo;
        }

        public async Task<ReceitaTipo> AtualizarReceitaTipo(ReceitaTipo receitaTipo)
        {
            _context.ReceitaTipo.Update(receitaTipo);
            await _context.SaveChangesAsync();
            return receitaTipo;
        }

        public async Task<bool> ExcluirReceitaTipo(int idReceitaTipo, int idUsuario)
        {
            try { return await _context.ReceitaTipo.Where(rt => rt.IDReceitaTipo == idReceitaTipo && rt.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}