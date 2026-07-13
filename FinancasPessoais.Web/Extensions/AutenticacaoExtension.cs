using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;

namespace FinancasPessoais.Web.Extensions
{
    public static class AutenticacaoExtension
    {
        public static async Task CriarCookieAutenticacao(this HttpContext httpContext, int idUsuario, string nmUsuario, string nmLogin)
        {
            var credenciais = new List<Claim>
            {
                new(ClaimTypes.NameIdentifier, idUsuario.ToString()),
                new(ClaimTypes.Name, nmUsuario),
                new("IDUsuario", idUsuario.ToString()),
                new("NMUsuario", nmUsuario),
                new("Login", nmLogin)
            };

            var identidade = new ClaimsIdentity(credenciais, "Auth");
            var autenticacao = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15)
            };

            await httpContext.SignInAsync("Auth", new ClaimsPrincipal(identidade), autenticacao);
        }

        public static async Task AtualizarCookieAutenticacao(this HttpContext httpContext, string nmUsuario)
        {
            var identidadeAtual = (ClaimsIdentity) httpContext.User.Identity!;

            var claimNativa = identidadeAtual.FindFirst(ClaimTypes.Name);
            if (claimNativa != null) identidadeAtual.RemoveClaim(claimNativa);

            var claimCustom = identidadeAtual.FindFirst("NMUsuario");
            if (claimCustom != null) identidadeAtual.RemoveClaim(claimCustom);

            identidadeAtual.AddClaim(new Claim(ClaimTypes.Name, nmUsuario));
            identidadeAtual.AddClaim(new Claim("NMUsuario", nmUsuario));

            var propriedades = new AuthenticationProperties
            {
                IsPersistent = false,
                ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(15)
            };

            await httpContext.SignInAsync("Auth", new ClaimsPrincipal(identidadeAtual), propriedades);
        }

        public static int RetornarIDUsuario(this ClaimsPrincipal user)
        {
            var claim = user.FindFirst(ClaimTypes.NameIdentifier);
            return claim != null ? int.Parse(claim.Value) : 0;
        }
    }
}