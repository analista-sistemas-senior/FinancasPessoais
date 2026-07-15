using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.Mappings;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace FinancasPessoais.Service.Services
{
    public class UsuarioService(IUsuarioRepository usuarioRepository) : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository = usuarioRepository;
        private readonly PasswordHasher<Usuario> _passwordHasher = new();

        public async Task<UsuarioDTO?> RetornarUsuarioPorId(int idUsuario)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorId(idUsuario);
            return usuario?.ParaDTO();
        }

        public async Task<UsuarioDTO?> RetornarUsuarioPorLogin(string nmLogin)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorLogin(nmLogin);
            return usuario?.ParaDTO();
        }

        public async Task<Resultado<UsuarioDTO>> CadastrarUsuario(UsuarioDTO usuario)
        {
            var usuarioExistente = await _usuarioRepository.RetornarUsuarioPorLogin(usuario.NMLogin);
            if (usuarioExistente != null) return Resultado<UsuarioDTO>.Falha("Nome de login já existente");

            var usuarioNovo = usuario.ParaEntidade();
            string senhaCriptografada = _passwordHasher.HashPassword(usuarioNovo, usuarioNovo.CDSenha);
            usuarioNovo.DefinirSenhaCriptografada(senhaCriptografada);

            await _usuarioRepository.CadastrarUsuario(usuarioNovo);

            return Resultado<UsuarioDTO>.Ok(usuario);
        }

        public async Task<Resultado<UsuarioDTO>> AtualizarUsuarioPerfil(UsuarioDTO usuario)
        {
            try
            {
                var usuarioAtualizado = usuario.ParaEntidade();
                usuarioAtualizado.DefinirSenhaCriptografada(_passwordHasher.HashPassword(usuarioAtualizado, usuarioAtualizado.CDSenha));

                await _usuarioRepository.AtualizarUsuario(usuarioAtualizado);
                return Resultado<UsuarioDTO>.Ok(usuarioAtualizado.ParaDTO());
            }
            catch (DbUpdateConcurrencyException) { return Resultado<UsuarioDTO>.Falha("Falhou"); }
        }

        public async Task<Resultado<UsuarioDTO>> AutenticarUsuario(string nmLogin, string cdSenha)
        {
            var usuario = await _usuarioRepository.RetornarUsuarioPorLogin(nmLogin);
            if (usuario == null) return Resultado<UsuarioDTO>.Falha("Usuário inexistente");

            var verificacaoSenha = _passwordHasher.VerifyHashedPassword(usuario, usuario.CDSenha, cdSenha);
            if (verificacaoSenha == PasswordVerificationResult.Success || verificacaoSenha == PasswordVerificationResult.SuccessRehashNeeded) return Resultado<UsuarioDTO>.Ok(usuario.ParaDTO());

            return Resultado<UsuarioDTO>.Falha("Senha incorreta");
        }
    }
}