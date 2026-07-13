using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class IndiceFinanceiroController(IIndiceFinanceiroService indiceFinanceiro, IDataProtectionProvider provider) : Controller
    {
        private readonly IIndiceFinanceiroService _indiceFinanceiroService = indiceFinanceiro;
        private readonly IDataProtector _protetor = provider.CreateProtector("IndiceFinanceiro.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var indiceFinanceirosDTOS = await _indiceFinanceiroService.RetornarIndicesFinanceirosPorIdUsuario(User.RetornarIDUsuario());
            return View(indiceFinanceirosDTOS.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(IndiceFinanceiroViewModel indiceFinanceiroViewModel)
        {
            if (!ModelState.IsValid) return View(indiceFinanceiroViewModel);

            indiceFinanceiroViewModel.IDUsuario = User.RetornarIDUsuario();
            await _indiceFinanceiroService.CadastrarIndiceFinanceiro(indiceFinanceiroViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idIndiceFinanceiro;
            try 
            {
                idIndiceFinanceiro = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var indiceFinanceiroResultado = await _indiceFinanceiroService.RetornarIndiceFinanceiroAutentico(idIndiceFinanceiro, User.RetornarIDUsuario());
            if (!indiceFinanceiroResultado.Sucesso)
            {
                TempData["MensagemErro"] = indiceFinanceiroResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var indiceFinanceiroViewModel = indiceFinanceiroResultado.Dados!.ParaViewModel();
            indiceFinanceiroViewModel.IDIndiceFinanceiroCriptografado = id;

            return View(indiceFinanceiroViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(IndiceFinanceiroViewModel indiceFinanceiroViewModel)
        {
            if (!ModelState.IsValid) return View(indiceFinanceiroViewModel);

            try
            {
                indiceFinanceiroViewModel.IDIndiceFinanceiro = int.Parse(_protetor.Unprotect(indiceFinanceiroViewModel.IDIndiceFinanceiroCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            indiceFinanceiroViewModel.IDUsuario = User.RetornarIDUsuario();

            var indiceFinanceiroResultado = await _indiceFinanceiroService.AtualizarIndiceFinanceiro(indiceFinanceiroViewModel.ParaDTO());
            if (!indiceFinanceiroResultado.Sucesso)
            {
                TempData["MensagemErro"] = indiceFinanceiroResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idIndiceFinanceiro;
            try
            {
                idIndiceFinanceiro = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var indiceFinanceiroExcluido = await _indiceFinanceiroService.ExcluirIndiceFinanceiro(idIndiceFinanceiro, User.RetornarIDUsuario());
            if (!indiceFinanceiroExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}