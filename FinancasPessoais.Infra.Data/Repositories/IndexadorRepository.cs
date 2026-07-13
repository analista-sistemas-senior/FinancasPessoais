using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class IndexadorRepository(FinancasPessoaisDbContext context) : IIndexadorRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Indexador?> RetornarIndexadorPorId(int idIndexador)
        {
            return await _context.Indexador.AsNoTracking().Include(ix => ix.IndiceFinanceiro).FirstOrDefaultAsync(ix => ix.IDIndexador == idIndexador);
        }

        public async Task<Indexador?> RetornarIndexadorPorIdEIdUsuario(int idIndexador, int idUsuario)
        {
            return await _context.Indexador.AsNoTracking().Include(ix => ix.IndiceFinanceiro).FirstOrDefaultAsync(ix => ix.IDIndexador == idIndexador && ix.IDUsuario == idUsuario);
        }

        public async Task<List<Indexador>> RetornarIndexadoresPorIdUsuario(int idUsuario)
        {
            return await _context.Indexador.AsNoTracking().Include(ix => ix.IndiceFinanceiro).Where(ix => ix.IDUsuario == idUsuario).ToListAsync();
        }

        public async Task<Indexador> CadastrarIndexador(Indexador indexador)
        {
            _context.Indexador.Add(indexador);
            await _context.SaveChangesAsync();
            return indexador;
        }

        public async Task<Indexador> AtualizarIndexador(Indexador indexador)
        {
            _context.Indexador.Update(indexador);
            await _context.SaveChangesAsync();
            return indexador;
        }

        public async Task<bool> ExcluirIndexador(int idIndexador, int idUsuario)
        {
            try { return await _context.Indexador.Where(ix => ix.IDIndexador == idIndexador && ix.IDUsuario == idUsuario).ExecuteDeleteAsync() > 0; }
            catch (DbUpdateException) { return false; }
        }
    }
}