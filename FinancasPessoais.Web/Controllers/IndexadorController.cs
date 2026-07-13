using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Domain.Enums.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class IndexadorController(IIndexadorService indexadorService, IIndiceFinanceiroService indiceFinanceiro, IDataProtectionProvider provider) : Controller
    {
        private readonly IIndexadorService _indexadorService = indexadorService;
        private readonly IIndiceFinanceiroService _indiceFinanceiroService = indiceFinanceiro;
        private readonly IDataProtector _protetor = provider.CreateProtector("Indexador.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var indexadoresDTOs = await _indexadorService.RetornarIndexadoresPorIdUsuario(User.RetornarIDUsuario());
            return View(indexadoresDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            await Selecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(IndexadorViewModel indexadorViewModel)
        {
            if (!ModelState.IsValid)
            {
                await Selecao();
                return View(indexadorViewModel);
            }

            indexadorViewModel.IDUsuario = User.RetornarIDUsuario();
            await _indexadorService.CadastrarIndexador(indexadorViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idIndexador;
            try
            {
                idIndexador = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var indexadorResultado = await _indexadorService.RetornarIndexadorAutentico(idIndexador, User.RetornarIDUsuario());
            if (!indexadorResultado.Sucesso)
            {
                TempData["MensagemErro"] = indexadorResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(indexadorResultado.Dados!.IDIndiceFinanceiro);

            var indexadorViewModel = indexadorResultado.Dados!.ParaViewModel();
            indexadorViewModel.IDIndexadorCriptografado = id;

            return View(indexadorViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(IndexadorViewModel indexadorViewModel)
        {
            if (!ModelState.IsValid) return View(indexadorViewModel);

            try
            {
                indexadorViewModel.IDIndexador = int.Parse(_protetor.Unprotect(indexadorViewModel.IDIndexadorCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            indexadorViewModel.IDUsuario = User.RetornarIDUsuario();

            var indexadorResultado = await _indexadorService.AtualizarIndexador(indexadorViewModel.ParaDTO());
            if (!indexadorResultado.Sucesso)
            {
                TempData["MensagemErro"] = indexadorResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idIndexador;
            try
            {
                idIndexador = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var indexadorExcluido = await _indexadorService.ExcluirIndexador(idIndexador, User.RetornarIDUsuario());
            if (!indexadorExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        private async Task Selecao(int? idIndiceFinanceiro = null)
        {
            ViewData["Indices"] = null;

            var indices = await _indiceFinanceiroService.RetornarIndicesFinanceirosPorIdUsuario(User.RetornarIDUsuario());
            if (indices != null)
            {
                var indicesFormatados = indices.Select(i => new { i.IDIndiceFinanceiro, Texto = $"{i.NMIndiceFinanceiro} - {i.VLIndiceFinanceiro}% {i.INTaxaPeriodicidade.RetornarDescricao()}" }).OrderBy(i => i.Texto).ToList();
                ViewData["Indices"] = new SelectList(indicesFormatados, "IDIndiceFinanceiro", "Texto", idIndiceFinanceiro);
            }
        }
    }
}