using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IEmissorService
    {
        Task<EmissorDTO?> RetornarEmissorPorId(int idEmissor);
        Task<EmissorDTO?> RetornarEmissorPorIdEIdUsuario(int idEmissor, int idUsuario);
        Task<List<EmissorDTO>> RetornarEmissoresPorIdUsuario(int idUsuario);
        Task<Resultado<EmissorDTO>> CadastrarEmissor(EmissorDTO emissor);
        Task<Resultado<EmissorDTO>> AtualizarEmissor(EmissorDTO emissor);
        Task<bool> ExcluirEmissor(int idEmissor, int idUsuario);
        Task<Resultado<EmissorDTO>> RetornarEmissorAutentico(int idEmissor, int idUsuario);
    }
}