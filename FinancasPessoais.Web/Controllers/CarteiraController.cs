using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class CarteiraController(ICarteiraService carteiraService, IDataProtectionProvider provider) : Controller
    {
        private readonly ICarteiraService _carteiraService = carteiraService;
        private readonly IDataProtector _protetor = provider.CreateProtector("Carteira.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var carteirasDTOs = await _carteiraService.RetornarCarteirasPorIdUsuario(User.RetornarIDUsuario());
            return View(carteirasDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(CarteiraViewModel carteiraViewModel)
        {
            if (!ModelState.IsValid) return View(carteiraViewModel);

            carteiraViewModel.IDUsuario = User.RetornarIDUsuario();
            await _carteiraService.CadastrarCarteira(carteiraViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idCarteira;
            try 
            {
                idCarteira = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var carteiraResultado = await _carteiraService.RetornarCarteiraAutentica(idCarteira, User.RetornarIDUsuario());
            if (!carteiraResultado.Sucesso)
            {
                TempData["MensagemErro"] = carteiraResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var carteiraViewModel = carteiraResultado.Dados!.ParaViewModel();
            carteiraViewModel.IDCarteiraCriptografada = id;

            return View(carteiraViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(CarteiraViewModel carteiraViewModel)
        {
            if (!ModelState.IsValid) return View(carteiraViewModel);

            try 
            {
                carteiraViewModel.IDCarteira = int.Parse(_protetor.Unprotect(carteiraViewModel.IDCarteiraCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            carteiraViewModel.IDUsuario = User.RetornarIDUsuario();

            var carteiraResultado = await _carteiraService.AtualizarCarteira(carteiraViewModel.ParaDTO());
            if (!carteiraResultado.Sucesso)
            {
                TempData["MensagemErro"] = carteiraResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idCarteira;
            try 
            {
                idCarteira = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var carteiraExcluida = await _carteiraService.ExcluirCarteira(idCarteira, User.RetornarIDUsuario());
            if (!carteiraExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}