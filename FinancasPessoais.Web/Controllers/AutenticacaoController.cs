using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    public class AutenticacaoController(IUsuarioService usuarioService) : Controller
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        public IActionResult Login(string? returnUrl = null)
        {
            if (User.Identity?.IsAuthenticated == true) return RedirectToAction("Index", "Home");

            if (!string.IsNullOrEmpty(returnUrl) && returnUrl != "/") TempData["SessaoExpirada"] = "Sua sessão expirou por inatividade";

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(string login, string cdSenha)
        {
            if (string.IsNullOrEmpty(login) || string.IsNullOrEmpty(cdSenha))
            {
                ModelState.AddModelError(string.Empty, "Preencha todos os campos");
                return View();
            }

            var usuario = await _usuarioService.AutenticarUsuario(login, cdSenha);
            if (!usuario.Sucesso)
            {
                ModelState.AddModelError(string.Empty, usuario.MensagemErro);
                return View();
            }

            await AutenticacaoExtension.CriarCookieAutenticacao(HttpContext, usuario.Dados!.IDUsuario, usuario.Dados.NMUsuario, usuario.Dados.NMLogin);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult AcessoNegado()
        {
            return View();
        }

        public async Task<IActionResult> Sair()
        {
            await HttpContext.SignOutAsync("Auth");
            return RedirectToAction("Login");
        }
    }
}
