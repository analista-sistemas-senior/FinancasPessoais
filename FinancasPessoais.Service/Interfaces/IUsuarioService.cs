using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IUsuarioService
    {
        Task<UsuarioDTO?> RetornarUsuarioPorId(int idUsuario);
        Task<UsuarioDTO?> RetornarUsuarioPorLogin(string nmLogin);
        Task<Resultado<UsuarioDTO>> CadastrarUsuario(UsuarioDTO usuario);
        Task<Resultado<UsuarioDTO>> AtualizarUsuarioPerfil(UsuarioDTO usuario);
        Task<Resultado<UsuarioDTO>> AutenticarUsuario(string nmLogin, string cdSenha);
    }
}