using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class CarteiraRepository(FinancasPessoaisDbContext context) : ICarteiraRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Carteira?> RetornarCarteiraPorId(int idCarteira)
        {
            return await _context.Carteira.AsNoTracking().FirstOrDefaultAsync(c => c.IDCarteira == idCarteira);
        }

        public async Task<Carteira?> RetornarCarteiraPorIdEIdUsuario(int idCarteira, int idUsuario)
        {
            return await _context.Carteira.AsNoTracking().FirstOrDefaultAsync(c => c.IDCarteira == idCarteira && c.IDUsuario == idUsuario);
        }

        public async Task<List<Carteira>> RetornarCarteirasPorIdUsuario(int idUsuario)
        {
            return await _context.Carteira.AsNoTracking().Where(c => c.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Carteira> CadastrarCarteira(Carteira carteira)
        {
            _context.Carteira.Add(carteira);
            await _context.SaveChangesAsync();
            return carteira;
        }

        public async Task<Carteira> AtualizarCarteira(Carteira carteira)
        {
            _context.Carteira.Update(carteira);
            await _context.SaveChangesAsync();
            return carteira;
        }

        public async Task<bool> ExcluirCarteira(int idCarteira, int idUsuario)
        {
            try { return await _context.Carteira.Where(c => c.IDCarteira == idCarteira && c.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}