using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class IndiceFinanceiroRepository(FinancasPessoaisDbContext context) : IIndiceFinanceiroRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<IndiceFinanceiro?> RetornarIndiceFinanceiroPorId(int idIndiceFinanceiro)
        {
            return await _context.IndiceFinanceiro.AsNoTracking().FirstOrDefaultAsync(inf => inf.IDIndiceFinanceiro == idIndiceFinanceiro);
        }

        public async Task<IndiceFinanceiro?> RetornarIndiceFinanceiroPorIdEIdUsuario(int idIndiceFinanceiro, int idUsuario)
        {
            return await _context.IndiceFinanceiro.AsNoTracking().FirstOrDefaultAsync(inf => inf.IDIndiceFinanceiro == idIndiceFinanceiro && inf.IDUsuario == idUsuario);
        }

        public async Task<List<IndiceFinanceiro>> RetornarIndicesFinanceirosPorIdUsuario(int idUsuario)
        {
            return await _context.IndiceFinanceiro.AsNoTracking().Where(inf => inf.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<IndiceFinanceiro> CadastrarIndiceFinanceiro(IndiceFinanceiro indiceFinanceiro)
        {
            _context.IndiceFinanceiro.Add(indiceFinanceiro);
            await _context.SaveChangesAsync();
            return indiceFinanceiro;
        }

        public async Task<IndiceFinanceiro> AtualizarIndiceFinanceiro(IndiceFinanceiro indiceFinanceiro)
        {
            _context.IndiceFinanceiro.Update(indiceFinanceiro);
            await _context.SaveChangesAsync();
            return indiceFinanceiro;
        }

        public async Task<bool> ExcluirIndiceFinanceiro(int idIndiceFinanceiro, int idUsuario)
        {
            try { return await _context.IndiceFinanceiro.Where(inf => inf.IDIndiceFinanceiro == idIndiceFinanceiro && inf.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}