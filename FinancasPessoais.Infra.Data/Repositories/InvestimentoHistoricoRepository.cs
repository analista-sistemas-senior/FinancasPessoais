using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class InvestimentoHistoricoRepository(FinancasPessoaisDbContext context) : IInvestimentoHistoricoRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<InvestimentoHistorico?> RetornarInvestimentoHistoricoPorId(int idInvestimentoHistorico)
        {
            return await _context.InvestimentoHistorico.AsNoTracking().Include(ih => ih.Investimento).FirstOrDefaultAsync(ih => ih.IDInvestimentoHistorico == idInvestimentoHistorico);
        }

        public async Task<InvestimentoHistorico?> RetornarInvestimentoHistoricoPorIdEIdUsuario(int idInvestimentoHistorico, int idUsuario)
        {
            return await _context.InvestimentoHistorico.AsNoTracking().Include(ih => ih.Investimento).FirstOrDefaultAsync(ih => ih.IDInvestimentoHistorico == idInvestimentoHistorico && ih.Investimento.IDUsuario == idUsuario);
        }

        public async Task<List<InvestimentoHistorico>> RetornarInvestimentosHistoricosPorIdUsuario(int idUsuario)
        {
            return await _context.InvestimentoHistorico.AsNoTracking().Include(ih => ih.Investimento).Where(ih => ih.Investimento.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<InvestimentoHistorico> CadastrarInvestimentoHistorico(InvestimentoHistorico investimentoHistorico)
        {
            _context.InvestimentoHistorico.Add(investimentoHistorico);
            await _context.SaveChangesAsync();
            return investimentoHistorico;
        }

        public async Task<InvestimentoHistorico> AtualizarInvestimentoHistorico(InvestimentoHistorico investimentoHistorico)
        {
            _context.InvestimentoHistorico.Update(investimentoHistorico);
            await _context.SaveChangesAsync();
            return investimentoHistorico;
        }

        public async Task<bool> ExcluirInvestimentoHistorico(int idInvestimentoHistorico, int idUsuario)
        {
            try { return await _context.InvestimentoHistorico.Where(ih => ih.IDInvestimentoHistorico == idInvestimentoHistorico && ih.Investimento.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }

        public async Task<List<InvestimentoHistorico>> RetornarInvestimentosHistoricosPorIdInvestimento(int idInvestimento)
        {
            return await _context.InvestimentoHistorico.AsNoTracking().Include(ih => ih.Investimento).Where(ih => ih.IDInvestimento == idInvestimento).ToListAsync();
        }
    }
}