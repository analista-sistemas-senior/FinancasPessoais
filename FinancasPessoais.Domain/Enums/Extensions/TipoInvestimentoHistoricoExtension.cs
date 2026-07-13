using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FinancasPessoais.Domain.Enums.Extensions
{
    public static class TipoInvestimentoHistoricoExtensions
    {
        public static string RetornarDescricao(this TipoInvestimentoHistorico valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());
            var atributo = campo?.GetCustomAttribute<DisplayAttribute>();

            return atributo?.Name ?? valor.ToString();
        }
    }
}