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
    public class DespesaController(IDespesaService despesaService, ICarteiraService carteiraService, IDespesaTipoService despesaTipo, IDespesaFonteService despesaFonte, IDataProtectionProvider provider) : Controller
    {
        private readonly IDespesaService _despesaService = despesaService;
        private readonly ICarteiraService _carteiraService = carteiraService;
        private readonly IDespesaTipoService _despesaTipoService = despesaTipo;
        private readonly IDespesaFonteService _despesaFonteService = despesaFonte;
        private readonly IDataProtector _protetor = provider.CreateProtector("Despesa.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var despesasDTOs = await _despesaService.RetornarDespesasPorIdUsuario(User.RetornarIDUsuario());
            return View(despesasDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            await Selecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(DespesaViewModel despesaViewModel)
        {
            if (!ModelState.IsValid)
            {
                await Selecao();
                return View(despesaViewModel);
            }

            despesaViewModel.IDUsuario = User.RetornarIDUsuario();
            await _despesaService.CadastrarDespesa(despesaViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idDespesa;
            try
            {
                idDespesa = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var despesaResultado = await _despesaService.RetornarDespesaAutentica(idDespesa, User.RetornarIDUsuario());
            if (!despesaResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(despesaResultado.Dados!.IDCarteira, despesaResultado.Dados.IDDespesaTipo, despesaResultado.Dados.IDDespesaFonte);

            var despesaViewModel = despesaResultado.Dados.ParaViewModel();
            despesaViewModel.IDDespesaCriptografada = id;

            return View(despesaViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(DespesaViewModel despesaViewModel)
        {
            if (!ModelState.IsValid) return View(despesaViewModel);

            try
            {
                despesaViewModel.IDDespesa = int.Parse(_protetor.Unprotect(despesaViewModel.IDDespesaCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            despesaViewModel.IDUsuario = User.RetornarIDUsuario();

            var despesaResultado = await _despesaService.AtualizarDespesa(despesaViewModel.ParaDTO());
            if (!despesaResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idDespesa;
            try
            {
                idDespesa = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var despesaExcluido = await _despesaService.ExcluirDespesa(idDespesa, User.RetornarIDUsuario());
            if (!despesaExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        private async Task Selecao(int? idCarteira = null, int? idDespesaTipo = null, int? idDespesaFonte = null)
        {
            ViewData["Carteiras"] = null;
            var carteiras = await _carteiraService.RetornarCarteirasPorIdUsuario(User.RetornarIDUsuario());
            if (carteiras != null)
            {
                var carteiraFormatada = carteiras.Select(i => new { i.IDCarteira, i.NMCarteira }).OrderBy(i => i.NMCarteira).ToList();
                ViewData["Carteiras"] = new SelectList(carteiraFormatada, "IDCarteira", "NMCarteira", idCarteira);
            }

            ViewData["Tipos"] = null;
            var despesaTipo = await _despesaTipoService.RetornarDespesasTiposPorIdUsuario(User.RetornarIDUsuario());
            if (despesaTipo != null)
            {
                var tiposFormatados = despesaTipo.Select(i => new { i.IDDespesaTipo, i.NMDespesaTipo }).OrderBy(i => i.NMDespesaTipo).ToList();
                ViewData["Tipos"] = new SelectList(tiposFormatados, "IDDespesaTipo", "NMDespesaTipo", idDespesaTipo);
            }

            ViewData["Fontes"] = null;
            var despesaFonte = await _despesaFonteService.RetornarDespesasFontesPorIdUsuario(User.RetornarIDUsuario());
            if (despesaFonte != null)
            {
                var fontesFormatados = despesaFonte.Select(i => new { i.IDDespesaFonte, i.NMDespesaFonte }).OrderBy(i => i.NMDespesaFonte).ToList();
                ViewData["Fontes"] = new SelectList(fontesFormatados, "IDDespesaFonte", "NMDespesaFonte", idDespesaFonte);
            }
        }
    }
}