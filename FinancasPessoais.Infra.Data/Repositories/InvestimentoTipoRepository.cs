using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class InvestimentoTipoRepository(FinancasPessoaisDbContext context) : IInvestimentoTipoRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<InvestimentoTipo?> RetornarInvestimentoTipoPorId(int idInvestimentoTipo)
        {
            return await _context.InvestimentoTipo.AsNoTracking().Include(it => it.Indexador).FirstOrDefaultAsync(it => it.IDInvestimentoTipo == idInvestimentoTipo);
        }

        public async Task<InvestimentoTipo?> RetornarInvestimentoTipoPorIdEIdUsuario(int idInvestimentoTipo, int idUsuario)
        {
            return await _context.InvestimentoTipo.AsNoTracking().Include(it => it.Indexador).FirstOrDefaultAsync(it => it.IDInvestimentoTipo == idInvestimentoTipo && it.IDUsuario == idUsuario);
        }

        public async Task<List<InvestimentoTipo>> RetornarInvestimentosTiposPorIdUsuario(int idUsuario)
        {
            return await _context.InvestimentoTipo.AsNoTracking().Include(it => it.Indexador).Where(it => it.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<InvestimentoTipo> CadastrarInvestimentoTipo(InvestimentoTipo investimentoTipo)
        {
            _context.InvestimentoTipo.Add(investimentoTipo);
            await _context.SaveChangesAsync();
            return investimentoTipo;
        }

        public async Task<InvestimentoTipo> AtualizarInvestimentoTipo(InvestimentoTipo investimentoTipo)
        {
            _context.InvestimentoTipo.Update(investimentoTipo);
            await _context.SaveChangesAsync();
            return investimentoTipo;
        }

        public async Task<bool> ExcluirInvestimentoTipo(int idInvestimentoTipo, int idUsuario)
        {
            try { return await _context.InvestimentoTipo.Where(it => it.IDInvestimentoTipo == idInvestimentoTipo && it.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}