using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class EmissorMappingExtension
    {
        public static EmissorViewModel ParaViewModel(this EmissorDTO emissorDTO, IDataProtector? protetor = null)
        {
            return new EmissorViewModel(emissorDTO.IDEmissor, protetor?.Protect(emissorDTO.IDEmissor.ToString()), emissorDTO.IDUsuario, emissorDTO.NMEmissor);
        }

        public static IEnumerable<EmissorViewModel> ParaViewModels(this IEnumerable<EmissorDTO> emissoresDTOs, IDataProtector protetor)
        {
            return [.. emissoresDTOs.Select(e => e.ParaViewModel(protetor))];
        }

        public static EmissorDTO ParaDTO(this EmissorViewModel emissorViewModel)
        {
            return new EmissorDTO(emissorViewModel.IDEmissor, emissorViewModel.IDUsuario, emissorViewModel.NMEmissor);
        }
    }
}