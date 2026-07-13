using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class ReceitaController(IReceitaService receitaService, ICarteiraService carteiraService, IReceitaTipoService receitaTipoService, IReceitaFonteService receitaFonteService, IDataProtectionProvider provider) : Controller
    {
        private readonly IReceitaService _receitaService = receitaService;
        private readonly ICarteiraService _carteiraService = carteiraService;
        private readonly IReceitaTipoService _receitaTipoService = receitaTipoService;
        private readonly IReceitaFonteService _receitaFonteService = receitaFonteService;
        private readonly IDataProtector _protetor = provider.CreateProtector("Receita.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var receitasDTOs = await _receitaService.RetornarReceitasPorIdUsuario(User.RetornarIDUsuario());
            return View(receitasDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            await Selecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(ReceitaViewModel receitaViewModel)
        {
            if (!ModelState.IsValid)
            {
                await Selecao();
                return View(receitaViewModel);
            }

            receitaViewModel.IDUsuario = User.RetornarIDUsuario();
            await _receitaService.CadastrarReceita(receitaViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idReceita;
            try
            {
                idReceita = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var receitaResultado = await _receitaService.RetornarReceitaAutentica(idReceita, User.RetornarIDUsuario());
            if (!receitaResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(receitaResultado.Dados!.IDCarteira, receitaResultado.Dados.IDReceitaTipo, receitaResultado.Dados.IDReceitaFonte);

            var receitaViewModel = receitaResultado.Dados!.ParaViewModel();
            receitaViewModel.IDReceitaCriptografada = id;

            return View(receitaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(ReceitaViewModel receitaViewModel)
        {
            if (!ModelState.IsValid) return View(receitaViewModel);

            try
            {
                receitaViewModel.IDReceita = int.Parse(_protetor.Unprotect(receitaViewModel.IDReceitaCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            receitaViewModel.IDUsuario = User.RetornarIDUsuario();

            var receitaResultado = await _receitaService.AtualizarReceita(receitaViewModel.ParaDTO());
            if (!receitaResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idReceita;
            try
            {
                idReceita = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var receitaExcluido = await _receitaService.ExcluirReceita(idReceita, User.RetornarIDUsuario());
            if (!receitaExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        private async Task Selecao(int? idCarteira = null, int? idReceitaTipo = null, int? idReceitaFonte = null)
        {
            ViewData["Carteiras"] = null;
            var carteiras = await _carteiraService.RetornarCarteirasPorIdUsuario(User.RetornarIDUsuario());
            if (carteiras != null)
            {
                var carteiraFormatada = carteiras.Select(i => new { i.IDCarteira, i.NMCarteira }).OrderBy(i => i.NMCarteira).ToList();
                ViewData["Carteiras"] = new SelectList(carteiraFormatada, "IDCarteira", "NMCarteira", idCarteira);
            }

            ViewData["Tipos"] = null;
            var receitaTipo = await _receitaTipoService.RetornarReceitasTiposPorIdUsuario(User.RetornarIDUsuario());
            if (receitaTipo != null)
            {
                var tiposFormatados = receitaTipo.Select(i => new { i.IDReceitaTipo, i.NMReceitaTipo }).OrderBy(i => i.NMReceitaTipo).ToList();
                ViewData["Tipos"] = new SelectList(tiposFormatados, "IDReceitaTipo", "NMReceitaTipo", idReceitaTipo);
            }

            ViewData["Fontes"] = null;
            var receitaFonte = await _receitaFonteService.RetornarReceitasFontesPorIdUsuario(User.RetornarIDUsuario());
            if (receitaFonte != null)
            {
                var fontesFormatados = receitaFonte.Select(i => new { i.IDReceitaFonte, i.NMReceitaFonte }).OrderBy(i => i.NMReceitaFonte).ToList();
                ViewData["Fontes"] = new SelectList(fontesFormatados, "IDReceitaFonte", "NMReceitaFonte", idReceitaFonte);
            }
        }
    }
}