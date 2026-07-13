using FinancasPessoais.Service.Common;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Interfaces
{
    public interface IIndexadorService
    {
        Task<IndexadorDTO?> RetornarIndexadorPorId(int idIndexador);
        Task<IndexadorDTO?> RetornarIndexadorPorIdEIdUsuario(int idIndexador, int idUsuario);
        Task<List<IndexadorDTO>> RetornarIndexadoresPorIdUsuario(int idUsuario);
        Task<Resultado<IndexadorDTO>> CadastrarIndexador(IndexadorDTO indexador);
        Task<Resultado<IndexadorDTO>> AtualizarIndexador(IndexadorDTO indexador);
        Task<bool> ExcluirIndexador(int idIndexador, int idUsuario);
        Task<Resultado<IndexadorDTO>> RetornarIndexadorAutentico(int idIndexadoro, int idUsuario);
    }
}