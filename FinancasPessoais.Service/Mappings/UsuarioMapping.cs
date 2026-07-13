using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class UsuarioMapping
{
    public static UsuarioDTO ParaDTO(this Usuario usuario)
    {
        return new UsuarioDTO(usuario.IDUsuario, usuario.NMUsuario, usuario.NMLogin, usuario.CDSenha);
    }

    public static List<UsuarioDTO> ParaDTOs(this List<Usuario> usuarios)
    {
        return [.. usuarios.Select(u => u.ParaDTO()).ToList()];
    }

    public static Usuario PraEntidade(this UsuarioDTO usuarioDTO)
    {
        return new Usuario(usuarioDTO.IDUsuario, usuarioDTO.NMUsuario, usuarioDTO.NMLogin, usuarioDTO.CDSenha);
    }
}