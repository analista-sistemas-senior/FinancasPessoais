using FinancasPessoais.Domain.Entities;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Web.ViewModels;
using FinancasPessoais.Web.ViewModels.Reports;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class InvestimentoHistoricoController(IInvestimentoHistoricoService investimentoHistoricoService, IInvestimentoService investimentoService, IRelatorioService relatorioService, IDataProtectionProvider provider) : Controller
    {
        private readonly IInvestimentoHistoricoService _investimentoHistoricoService = investimentoHistoricoService;
        private readonly IInvestimentoService _investimentoService = investimentoService;
        private readonly IRelatorioService _relatorioService = relatorioService;
        private readonly IDataProtector _protetor = provider.CreateProtector("InvestimentoHistorico.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var investimentosHistoricosDTOs = await _investimentoService.RetornarInvestimentosPorIdUsuario(User.RetornarIDUsuario());
            return View(investimentosHistoricosDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                await Selecao();
            }
            else
            {
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
                await Selecao(idInvestimento);
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(InvestimentoHistoricoViewModel investimentoHistoricoViewModel)
        {
            if (!ModelState.IsValid) return View(investimentoHistoricoViewModel);

            await _investimentoHistoricoService.CadastrarInvestimentoHistorico(investimentoHistoricoViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idInvestimentoHistorico;
            try 
            {
                idInvestimentoHistorico = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var investimentoHistoricoResultado = await _investimentoHistoricoService.RetornarInvestimentoHistoricoAutentico(idInvestimentoHistorico, User.RetornarIDUsuario());
            if (!investimentoHistoricoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoHistoricoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            await Selecao(investimentoHistoricoResultado.Dados!.IDInvestimento);

            var investimentoHistoricoViewModel = investimentoHistoricoResultado.Dados.ParaViewModel();
            investimentoHistoricoViewModel.IDInvestimentoHistoricoCriptografado = id;

            return View(investimentoHistoricoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(InvestimentoHistoricoViewModel investimentoHistoricoViewModel)
        {
            if (!ModelState.IsValid) return View(investimentoHistoricoViewModel);

            try 
            {
                investimentoHistoricoViewModel.IDInvestimentoHistorico = int.Parse(_protetor.Unprotect(investimentoHistoricoViewModel.IDInvestimentoHistoricoCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var investimentoHistoricoResultado = await _investimentoHistoricoService.AtualizarInvestimentoHistorico(investimentoHistoricoViewModel.ParaDTO());
            if (!investimentoHistoricoResultado.Sucesso)
            {
                TempData["MensagemErro"] = investimentoHistoricoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idInvestimentoHistorico;
            try 
            {
                idInvestimentoHistorico = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var investimentoHistoricoDTO = await _investimentoHistoricoService.RetornarInvestimentoHistoricoPorId(idInvestimentoHistorico);
            if (investimentoHistoricoDTO == null) return Json(new { sucesso = false });

            var investimentoHistoricoExcluida = await _investimentoHistoricoService.ExcluirInvestimentoHistorico(idInvestimentoHistorico, investimentoHistoricoDTO.IDInvestimento, User.RetornarIDUsuario());
            if (!investimentoHistoricoExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RetornarInvestimentosHistoricos(string? id)
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

            var investimentoHistorico = await _investimentoHistoricoService.RetornarInvestimentosHistoricosPorIdInvestimento(idInvestimento);
            if (investimentoHistorico == null) return Json(new { sucesso = false });

            var primeiroHistorico = investimentoHistorico.FirstOrDefault();
            if (primeiroHistorico == null) return Json(new { sucesso = true, dados = new List<object>() });
            decimal saldo = primeiroHistorico.InvestimentoDTO!.VLInvestimento;

            var investimentoHistoricoVisualizacao = investimentoHistorico.ParaViewModels(_protetor);
            investimentoHistoricoVisualizacao = investimentoHistoricoVisualizacao.OrderBy(ih => ih.DTInvestimentoHistorico);

            var dados = new List<object>();
            foreach (var itemInvestimentoHistorico in investimentoHistoricoVisualizacao)
            {
                if (itemInvestimentoHistorico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saldo)
                {
                    saldo = itemInvestimentoHistorico.VLInvestimentoHistorico;
                }
                else
                {
                    saldo += itemInvestimentoHistorico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque ? -1 * itemInvestimentoHistorico.VLInvestimentoHistorico : itemInvestimentoHistorico.VLInvestimentoHistorico;
                }

                var historico = new
                {
                    data = itemInvestimentoHistorico.DTInvestimentoHistorico.ToString("dd/MM/yyyy HH:mm"),
                    valor = itemInvestimentoHistorico.VLInvestimentoHistorico.ToString("C", new System.Globalization.CultureInfo("pt-BR")),
                    tipo = itemInvestimentoHistorico.INInvestimentoHistorico.ToString(),
                    Cor = itemInvestimentoHistorico.VLInvestimentoHistorico > 0 ? (itemInvestimentoHistorico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque ? "vermelho" : "verde") : (itemInvestimentoHistorico.VLInvestimentoHistorico < 0 ? "vermelho" : "cinza"),
                    CorSaldo = saldo > 0 ? "verde" : (saldo < 0 ? "vermelho" : "cinza"),
                    id = itemInvestimentoHistorico.IDInvestimentoHistoricoCriptografado,
                    saldo = saldo.ToString("C", new System.Globalization.CultureInfo("pt-BR"))
                };

                dados.Add(historico);
            }

            return Json(new { sucesso = true, dados = dados.AsEnumerable().Reverse()});
        }

        public async Task<IActionResult> RelatorioVariacao()
        {
            var investimentosDTOs = await _relatorioService.RetornarInvestimentosVariacoes(User.RetornarIDUsuario());

            var viewModel = investimentosDTOs.Select(d => new RelatorioInvestimentoVariacaoViewModel
            {
                IDInvestimento = d.IDInvestimento,
                NMInvestimento = d.NMInvestimento,
                DTInvestimento = d.DTInvestimento,
                VLSaldo = d.VLSaldo,
                DTVencimento = d.DTVencimento,
                PCTaxaRentabilidade = d.PCTaxaRentabilidade.HasValue ? d.PCTaxaRentabilidade : null, 
                FLLiquidado = d.FLLiquidado,
                VLInvestimentoVariacao = d.VLInvestimentoVariacao,
                PCInvestimentoVariacao = d.PCInvestimentoVariacao
            }).ToList();

            return View(viewModel);
        }

        private async Task Selecao(int? idInvestimento = null)
        {
            ViewData["Investimentos"] = null;
            var investimento = await _investimentoService.RetornarInvestimentosPorIdUsuario(User.RetornarIDUsuario());
            if (investimento != null)
            {
                var investimentoFormatados = investimento.Select(i => new { i.IDInvestimento, i.NMInvestimento }).OrderBy(i => i.NMInvestimento).ToList();
                ViewData["Investimentos"] = new SelectList(investimentoFormatados, "IDInvestimento", "NMInvestimento", idInvestimento);
            }
        }
    }
}