using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class DespesaFonteController(IDespesaFonteService despesaFonteService, IDataProtectionProvider provider) : Controller
    {
        private readonly IDespesaFonteService _despesaFonteService = despesaFonteService;
        private readonly IDataProtector _protetor = provider.CreateProtector("DespesaTipo.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var DespesasFontesDTOs = await _despesaFonteService.RetornarDespesasFontesPorIdUsuario(User.RetornarIDUsuario());
            return View(DespesasFontesDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(DespesaFonteViewModel despesaFonteViewModel)
        {
            if (!ModelState.IsValid) return View(despesaFonteViewModel);

            despesaFonteViewModel.IDUsuario = User.RetornarIDUsuario();
            await _despesaFonteService.CadastrarDespesaFonte(despesaFonteViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idDespesaFonte;
            try 
            {
                idDespesaFonte = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var despesaFonteResultado = await _despesaFonteService.RetornarDespesaFonteAutentica(idDespesaFonte, User.RetornarIDUsuario());
            if (!despesaFonteResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaFonteResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var despesaFonteViewModel = despesaFonteResultado.Dados!.ParaViewModel();
            despesaFonteViewModel.IDDespesaFonteCriptografada = id;

            return View(despesaFonteViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(DespesaFonteViewModel despesaFonteViewModel)
        {
            if (!ModelState.IsValid) return View(despesaFonteViewModel);

            try 
            {
                despesaFonteViewModel.IDDespesaFonte = int.Parse(_protetor.Unprotect(despesaFonteViewModel.IDDespesaFonteCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            despesaFonteViewModel.IDUsuario = User.RetornarIDUsuario();

            var despesaFonteResultado = await _despesaFonteService.AtualizarDespesaFonte(despesaFonteViewModel.ParaDTO());
            if (!despesaFonteResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaFonteResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idDespesaFonte;
            try 
            {
                idDespesaFonte = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var despesaFonteExcluida = await _despesaFonteService.ExcluirDespesaFonte(idDespesaFonte, User.RetornarIDUsuario());
            if (!despesaFonteExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}