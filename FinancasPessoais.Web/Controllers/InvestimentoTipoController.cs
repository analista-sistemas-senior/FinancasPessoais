using FinancasPessoais.Domain.Entities;
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
    public class InvestimentoTipoController(IInvestimentoTipoService investimentoTipoService, IIndexadorService indexadorService, IDataProtectionProvider provider) : Controller
    {
        private readonly IInvestimentoTipoService _investimentoTipoService = investimentoTipoService;
        private readonly IIndexadorService _indexadorService = indexadorService;
        private readonly IDataProtector _protetor = provider.CreateProtector("InvestimentoTipo.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var investimentosTiposDTOs = await _investimentoTipoService.RetornarInvestimentosTiposPorIdUsuario(User.RetornarIDUsuario());
            return View(investimentosTiposDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            await Selecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(InvestimentoTipoViewModel investimentoTipoViewModel)
        {
            if (!ModelState.IsValid) return View(investimentoTipoViewModel);

            investimentoTipoViewModel.IDUsuario = User.RetornarIDUsuario();
            await _investimentoTipoService.CadastrarInvestimentoTipo(investimentoTipoViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idInvestimentoTipo;
            try 
            {
                idInvestimentoTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var investimentoTipoResultado = await _investimentoTipoService.RetornarInvestimentoTipoAutentico(idInvestimentoTipo, User.RetornarIDUsuario());
            if (!investimentoTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(investimentoTipoResultado.Dados!.IDIndexador);

            var investimentoTipoViewModel = investimentoTipoResultado.Dados!.ParaViewModel();
            investimentoTipoViewModel.IDInvestimentoTipoCriptografado = id;

            return View(investimentoTipoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(InvestimentoTipoViewModel investimentoTipoViewModel)
        {
            if (!ModelState.IsValid) return View(investimentoTipoViewModel);

            try 
            {
                investimentoTipoViewModel.IDInvestimentoTipo = int.Parse(_protetor.Unprotect(investimentoTipoViewModel.IDInvestimentoTipoCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            investimentoTipoViewModel.IDUsuario = User.RetornarIDUsuario();

            var investimentoTipoResultado = await _investimentoTipoService.AtualizarInvestimentoTipo(investimentoTipoViewModel.ParaDTO());
            if (!investimentoTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idInvestimentoTipo;
            try 
            {
                idInvestimentoTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var investimentoTipoExcluida = await _investimentoTipoService.ExcluirInvestimentoTipo(idInvestimentoTipo, User.RetornarIDUsuario());
            if (!investimentoTipoExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        private async Task Selecao(int? idIndexador = null)
        {
            ViewData["Indexadores"] = null;
            var indexador = await _indexadorService.RetornarIndexadoresPorIdUsuario(User.RetornarIDUsuario());
            if (indexador != null)
            {
                var indexadoresFormatados = indexador.Select(i => new { i.IDIndexador, Texto = $"{i.SGIndexador} - {i.NMIndexador}" }).OrderBy(i => i.Texto).ToList();
                ViewData["Indexadores"] = new SelectList(indexadoresFormatados, "IDIndexador", "Texto", idIndexador);
            }
        }
    }
}