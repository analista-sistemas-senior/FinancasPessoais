using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class EmissorController(IEmissorService emissorService, IDataProtectionProvider provider) : Controller
    {
        private readonly IEmissorService _emissorService = emissorService;
        private readonly IDataProtector _protetor = provider.CreateProtector("Emissor.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var emissoresDTOs = await _emissorService.RetornarEmissoresPorIdUsuario(User.RetornarIDUsuario());
            return View(emissoresDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(EmissorViewModel emissorViewModel)
        {
            if (!ModelState.IsValid) return View(emissorViewModel);

            emissorViewModel.IDUsuario = User.RetornarIDUsuario();
            await _emissorService.CadastrarEmissor(emissorViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idEmissor;
            try 
            {
                idEmissor = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var emissorResultado = await _emissorService.RetornarEmissorAutentico(idEmissor, User.RetornarIDUsuario());
            if (!emissorResultado.Sucesso)
            {
                TempData["MensagemErro"] = emissorResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var emissorViewModel = emissorResultado.Dados!.ParaViewModel();
            emissorViewModel.IDEmissorCriptografado = id;

            return View(emissorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(EmissorViewModel emissorViewModel)
        {
            if (!ModelState.IsValid) return View(emissorViewModel);

            try 
            {
                emissorViewModel.IDEmissor = int.Parse(_protetor.Unprotect(emissorViewModel.IDEmissorCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            emissorViewModel.IDUsuario = User.RetornarIDUsuario();

            var emissorResultado = await _emissorService.AtualizarEmissor(emissorViewModel.ParaDTO());
            if (!emissorResultado.Sucesso)
            {
                TempData["MensagemErro"] = emissorResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idEmissor;
            try 
            {
                idEmissor = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var emissorExcluido = await _emissorService.ExcluirEmissor(idEmissor, User.RetornarIDUsuario());
            if (!emissorExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}