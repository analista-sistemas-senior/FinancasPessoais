using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IIndexadorRepository
    {
        Task<Indexador?> RetornarIndexadorPorId(int idIndexador);
        Task<Indexador?> RetornarIndexadorPorIdEIdUsuario(int idIndexador, int idUsuario);
        Task<List<Indexador>> RetornarIndexadoresPorIdUsuario(int idUsuario);
        Task<Indexador> CadastrarIndexador(Indexador indexador);
        Task<Indexador> AtualizarIndexador(Indexador indexador);
        Task<bool> ExcluirIndexador(int idIndexador, int idUsuario);
    }
}