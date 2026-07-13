using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class DespesaRepository(FinancasPessoaisDbContext context) : IDespesaRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Despesa?> RetornarDespesaPorId(int idDespesa)
        {
            return await _context.Despesa.AsNoTracking().Include(d => d.DespesaTipo).Include(d => d.DespesaFonte).Include(d => d.Carteira).FirstOrDefaultAsync(d => d.IDDespesa == idDespesa);
        }

        public async Task<Despesa?> RetornarDespesaPorIdEIdUsuario(int idDespesa, int idUsuario)
        {
            return await _context.Despesa.AsNoTracking().Include(d => d.DespesaTipo).Include(d => d.DespesaFonte).Include(d => d.Carteira).FirstOrDefaultAsync(d => d.IDDespesa == idDespesa && d.IDUsuario == idUsuario);
        }

        public async Task<List<Despesa>> RetornarDespesasPorIdUsuario(int idUsuario)
        {
            return await _context.Despesa.AsNoTracking().Include(d => d.DespesaTipo).Include(d => d.DespesaFonte).Include(d => d.Carteira).Where(d => d.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Despesa> CadastrarDespesa(Despesa despesa)
        {
            _context.Despesa.Add(despesa);
            await _context.SaveChangesAsync();
            return despesa;
        }

        public async Task<Despesa> AtualizarDespesa(Despesa despesa)
        {
            _context.Despesa.Update(despesa);
            await _context.SaveChangesAsync();
            return despesa;
        }

        public async Task<bool> ExcluirDespesa(int idDespesa, int idUsuario)
        {
            try { return await _context.Despesa.Where(d => d.IDDespesa == idDespesa && d.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}