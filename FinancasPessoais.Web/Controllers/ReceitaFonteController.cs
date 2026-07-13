using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class ReceitaFonteController(IReceitaFonteService receitaFonteService, IDataProtectionProvider provider) : Controller
    {
        private readonly IReceitaFonteService _receitaFonteService = receitaFonteService;
        private readonly IDataProtector _protetor = provider.CreateProtector("ReceitaTipo.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var receitasFontesDTOs = await _receitaFonteService.RetornarReceitasFontesPorIdUsuario(User.RetornarIDUsuario());
            return View(receitasFontesDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(ReceitaFonteViewModel receitaFonteViewModel)
        {
            if (!ModelState.IsValid) return View(receitaFonteViewModel);

            receitaFonteViewModel.IDUsuario = User.RetornarIDUsuario();
            await _receitaFonteService.CadastrarReceitaFonte(receitaFonteViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idReceitaFonte;
            try 
            {
                idReceitaFonte = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var receitaFonteResultado = await _receitaFonteService.RetornarReceitaFonteAutentica(idReceitaFonte, User.RetornarIDUsuario());
            if (!receitaFonteResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaFonteResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var receitaViewModel = receitaFonteResultado.Dados!.ParaViewModel();
            receitaViewModel.IDReceitaFonteCriptografada = id;

            return View(receitaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(ReceitaFonteViewModel receitaFonteViewModel)
        {
            if (!ModelState.IsValid) return View(receitaFonteViewModel);

            try 
            {
                receitaFonteViewModel.IDReceitaFonte = int.Parse(_protetor.Unprotect(receitaFonteViewModel.IDReceitaFonteCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            receitaFonteViewModel.IDUsuario = User.RetornarIDUsuario();

            var receitaFonteResultado = await _receitaFonteService.AtualizarReceitaFonte(receitaFonteViewModel.ParaDTO());
            if (!receitaFonteResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaFonteResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idReceitaFonte;
            try 
            {
                idReceitaFonte = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var receitaFonteExcluida = await _receitaFonteService.ExcluirReceitaFonte(idReceitaFonte, User.RetornarIDUsuario());
            if (!receitaFonteExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}