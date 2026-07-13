using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Infra.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Infra.Data.Repositories
{
    public class UsuarioRepository(FinancasPessoaisDbContext context) : IUsuarioRepository
    {
        private readonly FinancasPessoaisDbContext _context = context;

        public async Task<Usuario?> RetornarUsuarioPorId(int idUsuario)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.IDUsuario == idUsuario);
        }

        public async Task<Usuario?> RetornarUsuarioPorLogin(string nmLogin)
        {
            return await _context.Usuario.AsNoTracking().FirstOrDefaultAsync(u => u.NMLogin == nmLogin);
        }

        public async Task<Usuario> CadastrarUsuario(Usuario usuario)
        {
            _context.Usuario.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<Usuario> AtualizarUsuario(Usuario usuario)
        {
            _context.Usuario.Update(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }
    }
}