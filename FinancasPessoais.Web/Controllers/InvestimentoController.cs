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
    public class InvestimentoController(IInvestimentoService investimentoService, IInvestimentoTipoService investimentoTipoService, IEmissorService emissorService, IInvestimentoHistoricoService investimentoHistoricoService, IDataProtectionProvider provider) : Controller
    {
        private readonly IInvestimentoService _investimentoService = investimentoService;
        private readonly IInvestimentoTipoService _investimentoTipoService = investimentoTipoService;
        private readonly IEmissorService _emissorService = emissorService;
        private readonly IInvestimentoHistoricoService _investimentoHistoricoService = investimentoHistoricoService;
        private readonly IDataProtector _protetor = provider.CreateProtector("Investimento.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var investimentosDTOs = await _investimentoService.RetornarInvestimentosPorIdUsuario(User.RetornarIDUsuario());
            return View(investimentosDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            await Selecao();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(InvestimentoViewModel investimentoViewModel)
        {
            if (!ModelState.IsValid)
            {
                await Selecao();
                return View(investimentoViewModel);
            }

            investimentoViewModel.IDUsuario = User.RetornarIDUsuario();
            investimentoViewModel.VLSaldo = investimentoViewModel.VLInvestimento;
            await _investimentoService.CadastrarInvestimento(investimentoViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idInvestimento;
            try
            {
                idInvestimento = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var investimentoResultado = await _investimentoService.RetornarInvestimentoAutentico(idInvestimento, User.RetornarIDUsuario());
            if (!investimentoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(investimentoResultado.Dados!.IDInvestimentoTipo, investimentoResultado.Dados.IDEmissor);

            var investimentoViewModel = investimentoResultado.Dados.ParaViewModel();
            investimentoViewModel.IDInvestimentoCriptografado = id;

            return View(investimentoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(InvestimentoViewModel investimentoViewModel)
        {
            if (!ModelState.IsValid) return View(investimentoViewModel);

            try
            {
                investimentoViewModel.IDInvestimento = int.Parse(_protetor.Unprotect(investimentoViewModel.IDInvestimentoCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            investimentoViewModel.IDUsuario = User.RetornarIDUsuario();
            investimentoViewModel.VLSaldo = investimentoViewModel.VLInvestimento;

            var investimentoResultado = await _investimentoService.AtualizarInvestimento(investimentoViewModel.ParaDTO());
            if (!investimentoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await _investimentoHistoricoService.AtualizarInvestimentoSaldo(investimentoViewModel.IDInvestimento);

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idInvestimento;
            try
            {
                idInvestimento = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var investimentoExcluido = await _investimentoService.ExcluirInvestimento(idInvestimento, User.RetornarIDUsuario());
            if (!investimentoExcluido)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        private async Task Selecao(int? idInvestimentoTipo = null, int? idEmissor = null)
        {
            ViewData["InvestimentosTipos"] = null;
            var investimentoTipo = await _investimentoTipoService.RetornarInvestimentosTiposPorIdUsuario(User.RetornarIDUsuario());
            if (investimentoTipo != null)
            {
                var investimentoTipoFormatados = investimentoTipo.Select(i => new { i.IDInvestimentoTipo, Texto = $"{i.INTipoRentabilidade.RetornarDescricao()} - {i.SGInvestimentoTipo} - {i.NMInvestimentoTipo}" }).OrderBy(i => i.Texto).ToList();
                ViewData["InvestimentosTipos"] = new SelectList(investimentoTipoFormatados, "IDInvestimentoTipo", "Texto", idInvestimentoTipo);
            }

            ViewData["Emissores"] = null;
            var emissor = await _emissorService.RetornarEmissoresPorIdUsuario(User.RetornarIDUsuario());
            if (emissor != null)
            {
                var emissoresFormatados = emissor.Select(i => new { i.IDEmissor, i.NMEmissor }).OrderBy(i => i.NMEmissor).ToList();
                ViewData["Emissores"] = new SelectList(emissoresFormatados, "IDEmissor", "NMEmissor", idEmissor);
            }
        }
    }
}