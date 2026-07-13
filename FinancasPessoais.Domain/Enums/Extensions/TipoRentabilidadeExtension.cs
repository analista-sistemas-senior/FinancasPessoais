using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace FinancasPessoais.Domain.Enums.Extensions
{
    public static class TipoRentabilidadeExtensions
    {
        public static string RetornarDescricao(this TipoRentabilidade valor)
        {
            var campo = valor.GetType().GetField(valor.ToString());
            var atributo = campo?.GetCustomAttribute<DisplayAttribute>();

            return atributo?.Name ?? valor.ToString();
        }
    }
}