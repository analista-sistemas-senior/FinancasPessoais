using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;

namespace FinancasPessoais.Web.Extensions
{
    public static class UsuarioMappingExtension
    {
        public static UsuarioViewModel ParaViewModel(this UsuarioDTO usuarioDTO)
        {
            return new UsuarioViewModel(usuarioDTO.IDUsuario, usuarioDTO.NMUsuario, usuarioDTO.NMLogin, usuarioDTO.CDSenha);
        }

        public static UsuarioDTO ParaDTO(this UsuarioViewModel usuarioViewModel)
        {
            return new UsuarioDTO(usuarioViewModel.IDUsuario, usuarioViewModel.NMUsuario, usuarioViewModel.NMLogin, usuarioViewModel.CDSenha);
        }
    }
}