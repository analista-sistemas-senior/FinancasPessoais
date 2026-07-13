using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.DTOs;

namespace FinancasPessoais.Service.Mappings;

public static class EmissorMapping
{
    public static EmissorDTO ParaDTO(this Emissor emissor)
    {
        return new EmissorDTO(emissor.IDEmissor, emissor.IDUsuario, emissor.NMEmissor);
    }

    public static List<EmissorDTO> ParaDTOs(this List<Emissor> emissores)
    {
        return [.. emissores.Select(e => e.ParaDTO()).ToList()];
    }

    public static Emissor PraEntidade(this EmissorDTO emissorDTO)
    {
        return new Emissor(emissorDTO.IDEmissor, emissorDTO.IDUsuario, emissorDTO.NMEmissor);
    }
}