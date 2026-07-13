using FinancasPessoais.Service.DTOs;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.DataProtection;

namespace FinancasPessoais.Web.Extensions
{
    public static class IndexadorMappingExtension
    {
        public static IndexadorViewModel ParaViewModel(this IndexadorDTO indexadorDTO, IDataProtector? protetor = null)
        {
            var indiceFinanceiroViewModel = indexadorDTO.IndiceFinanceiroDTO?.ParaViewModel();

            return new IndexadorViewModel(indexadorDTO.IDIndexador, protetor?.Protect(indexadorDTO.IDIndexador.ToString()), indexadorDTO.IDIndiceFinanceiro, indexadorDTO.IDUsuario, indexadorDTO.NMIndexador, indexadorDTO.SGIndexador, indexadorDTO.PCIndiceFinanceiro, indiceFinanceiroViewModel);
        }

        public static IEnumerable<IndexadorViewModel> ParaViewModels(this IEnumerable<IndexadorDTO> indexadoresDTOs, IDataProtector protetor)
        {
            return [.. indexadoresDTOs.Select(ix => ix.ParaViewModel(protetor))];
        }

        public static IndexadorDTO ParaDTO(this IndexadorViewModel indexadorViewModel)
        {
            return new IndexadorDTO(indexadorViewModel.IDIndexador, indexadorViewModel.IDIndiceFinanceiro, indexadorViewModel.IDUsuario, indexadorViewModel.NMIndexador, indexadorViewModel.SGIndexador, indexadorViewModel.PCIndiceFinanceiro, null);
        }
    }
}