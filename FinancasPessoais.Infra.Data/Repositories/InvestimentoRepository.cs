using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class InvestimentoRepository(FinancasPessoaisDbContext context) : IInvestimentoRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Investimento?> RetornarInvestimentoPorId(int idInvestimento)
        {
            return await _context.Investimento.AsNoTracking().Include(i => i.InvestimentoTipo).Include(i => i.Emissor).FirstOrDefaultAsync(i => i.IDInvestimento == idInvestimento);
        }

        public async Task<Investimento?> RetornarInvestimentoPorIdEIdUsuario(int idInvestimento, int idUsuario)
        {
            return await _context.Investimento.AsNoTracking().Include(i => i.InvestimentoTipo).Include(i => i.Emissor).FirstOrDefaultAsync(i => i.IDInvestimento == idInvestimento && i.IDUsuario == idUsuario);
        }

        public async Task<List<Investimento>> RetornarInvestimentosPorIdUsuario(int idUsuario)
        {
            return await _context.Investimento.AsNoTracking().Include(i => i.InvestimentoTipo).Include(i => i.Emissor).Where(i => i.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Investimento> CadastrarInvestimento(Investimento investimento)
        {
            _context.Investimento.Add(investimento);
            await _context.SaveChangesAsync();
            return investimento;
        }

        public async Task<Investimento> AtualizarInvestimento(Investimento investimento)
        {
            _context.Investimento.Update(investimento);
            await _context.SaveChangesAsync();
            return investimento;
        }

        public async Task<bool> ExcluirInvestimento(int idInvestimento, int idUsuario)
        {
            try { return await _context.Investimento.Where(i => i.IDInvestimento == idInvestimento && i.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}