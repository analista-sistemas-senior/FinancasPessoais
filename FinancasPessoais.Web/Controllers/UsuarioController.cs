using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    public class UsuarioController(IUsuarioService usuarioService) : Controller
    {
        private readonly IUsuarioService _usuarioService = usuarioService;

        [Authorize]
        public async Task<IActionResult> Perfil()
        {
            var usuario = await _usuarioService.RetornarUsuarioPorId(User.RetornarIDUsuario());
            return View(usuario!.ParaViewModel());
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Perfil(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return View(usuarioViewModel);

            usuarioViewModel.IDUsuario = User.RetornarIDUsuario();

            var usuarioResultado = await _usuarioService.AtualizarUsuarioPerfil(usuarioViewModel.ParaDTO());
            if (!usuarioResultado.Sucesso)
            {
                ModelState.AddModelError("", usuarioResultado.MensagemErro);
                return View(usuarioViewModel);
            }

            await AutenticacaoExtension.AtualizarCookieAutenticacao(HttpContext, usuarioViewModel.NMUsuario);

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registro(UsuarioViewModel usuarioViewModel)
        {
            if (!ModelState.IsValid) return View(usuarioViewModel);

            var usuarioResultado = await _usuarioService.CadastrarUsuario(usuarioViewModel.ParaDTO());
            if (!usuarioResultado.Sucesso)
            {
                ModelState.AddModelError("", usuarioResultado.MensagemErro);
                return View(usuarioResultado);
            }

            return RedirectToAction("Login", "Autenticacao");
        }
    }
}
