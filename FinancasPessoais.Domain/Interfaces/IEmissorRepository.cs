using FinancasPessoais.Domain.Entities;

namespace FinancasPessoais.Domain.Interfaces
{
    public interface IEmissorRepository
    {
        Task<Emissor?> RetornarEmissorPorId(int idEmissor);
        Task<Emissor?> RetornarEmissorPorIdEIdUsuario(int idEmissor, int idUsuario);
        Task<List<Emissor>> RetornarEmissoresPorIdUsuario(int idUsuario);
        Task<Emissor> CadastrarEmissor(Emissor emissor);
        Task<Emissor> AtualizarEmissor(Emissor emissor);
        Task<bool> ExcluirEmissor(int idEmissor, int idUsuario);
    }
}