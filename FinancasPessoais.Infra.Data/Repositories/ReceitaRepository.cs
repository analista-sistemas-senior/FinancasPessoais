using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class ReceitaRepository(FinancasPessoaisDbContext context) : IReceitaRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Receita?> RetornarReceitaPorId(int idReceita)
        {
            return await _context.Receita.AsNoTracking().Include(r => r.Carteira).Include(r => r.ReceitaTipo).Include(r => r.ReceitaFonte).FirstOrDefaultAsync(r => r.IDReceita == idReceita);
        }

        public async Task<Receita?> RetornarReceitaPorIdEIdUsuario(int idReceita, int idUsuario)
        {
            return await _context.Receita.AsNoTracking().Include(r => r.Carteira).Include(r => r.ReceitaTipo).Include(r => r.ReceitaFonte).FirstOrDefaultAsync(r => r.IDReceita == idReceita && r.IDUsuario == idUsuario);
        }

        public async Task<List<Receita>> RetornarReceitasPorIdUsuario(int idUsuario)
        {
            return await _context.Receita.AsNoTracking().Include(r => r.Carteira).Include(r => r.ReceitaTipo).Include(r => r.ReceitaFonte).Where(r => r.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Receita> CadastrarReceita(Receita receita)
        {
            _context.Receita.Add(receita);
            await _context.SaveChangesAsync();
            return receita;
        }

        public async Task<Receita> AtualizarReceita(Receita receita)
        {
            _context.Receita.Update(receita);
            await _context.SaveChangesAsync();
            return receita;
        }

        public async Task<bool> ExcluirReceita(int idReceita, int idUsuario)
        {
            try { return await _context.Receita.Where(r => r.IDReceita == idReceita && r.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}