using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class DespesaTipoRepository(FinancasPessoaisDbContext context) : IDespesaTipoRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<DespesaTipo?> RetornarDespesaTipoPorId(int idDespesaTipo)
        {
            return await _context.DespesaTipo.AsNoTracking().FirstOrDefaultAsync(dt => dt.IDDespesaTipo == idDespesaTipo);
        }

        public async Task<DespesaTipo?> RetornarDespesaTipoPorIdEIdUsuario(int idDespesaTipo, int idUsuario)
        {
            return await _context.DespesaTipo.AsNoTracking().FirstOrDefaultAsync(dt => dt.IDDespesaTipo == idDespesaTipo && dt.IDUsuario == idUsuario);
        }

        public async Task<List<DespesaTipo>> RetornarDespesasTiposPorIdUsuario(int idUsuario)
        {
            return await _context.DespesaTipo.AsNoTracking().Where(dt => dt.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<DespesaTipo> CadastrarDespesaTipo(DespesaTipo despesaTipo)
        {
            _context.DespesaTipo.Add(despesaTipo);
            await _context.SaveChangesAsync();
            return despesaTipo;
        }

        public async Task<DespesaTipo> AtualizarDespesaTipo(DespesaTipo despesaTipo)
        {
            _context.DespesaTipo.Update(despesaTipo);
            await _context.SaveChangesAsync();
            return despesaTipo;
        }

        public async Task<bool> ExcluirDespesaTipo(int idDespesaTipo, int idUsuario)
        {
            try { return await _context.DespesaTipo.Where(dt => dt.IDDespesaTipo == idDespesaTipo && dt.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}