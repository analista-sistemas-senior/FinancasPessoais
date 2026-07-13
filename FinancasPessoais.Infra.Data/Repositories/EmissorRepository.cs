using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class EmissorRepository(FinancasPessoaisDbContext context) : IEmissorRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Emissor?> RetornarEmissorPorId(int idEmissor)
        {
            return await _context.Emissor.AsNoTracking().FirstOrDefaultAsync(e => e.IDEmissor == idEmissor);
        }

        public async Task<Emissor?> RetornarEmissorPorIdEIdUsuario(int idEmissor, int idUsuario)
        {
            return await _context.Emissor.AsNoTracking().FirstOrDefaultAsync(e => e.IDEmissor == idEmissor && e.IDUsuario == idUsuario);
        }

        public async Task<List<Emissor>> RetornarEmissoresPorIdUsuario(int idUsuario)
        {
            return await _context.Emissor.AsNoTracking().Where(e => e.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Emissor> CadastrarEmissor(Emissor emissor)
        {
            _context.Emissor.Add(emissor);
            await _context.SaveChangesAsync();
            return emissor;
        }

        public async Task<Emissor> AtualizarEmissor(Emissor emissor)
        {
            _context.Emissor.Update(emissor);
            await _context.SaveChangesAsync();
            return emissor;
        }

        public async Task<bool> ExcluirEmissor(int idEmissor, int idUsuario)
        {
            try { return await _context.Emissor.Where(e => e.IDEmissor == idEmissor && e.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}