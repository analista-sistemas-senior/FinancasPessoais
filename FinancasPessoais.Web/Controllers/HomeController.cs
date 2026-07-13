using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Web.ViewModels.Reports;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class HomeController(IRelatorioService relatorioService) : Controller
    {
        private readonly IRelatorioService _relatorioService = relatorioService;

        public async Task<IActionResult> Index()
        {
            var PtBr = new System.Globalization.CultureInfo("pt-BR");
            int idUsuario = User.RetornarIDUsuario();

            var (totalInvestimento, totalJuro, totalAporte, totalSaque) = await _relatorioService.RetornarInvestimentosTotais(idUsuario);
            var totalDespesas = await _relatorioService.RetornarDespesasTotais(idUsuario);
            var totalReceitas = await _relatorioService.RetornarReceitasTotais(idUsuario);
            var totais12meses = await _relatorioService.RetornarInvestimentosTotais12Meses(idUsuario);
            var receitasDespesas12Meses = await _relatorioService.RetornarFluxoCaixa12Meses(idUsuario);
            var investimentosPorTipos = await _relatorioService.RetornarInvestimentosPorTipo(idUsuario);
            var despesasPorFontes = await _relatorioService.RetornarDespesasPorFontes(idUsuario);

            var viewModel = new RelatorioHomeViewModel
            {
                TotalInvestimento = totalInvestimento.ToString("C", PtBr),
                TotalJuro = totalJuro.ToString("C", PtBr),
                TotalAporte = totalAporte.ToString("C", PtBr),
                TotalSaque = totalSaque.ToString("C", PtBr),
                TotalDespesa = totalDespesas.ToString("C", PtBr),
                TotalReceita = totalReceitas.ToString("C", PtBr),

                Investimentos12MesesRotulosJson = JsonSerializer.Serialize(totais12meses.Keys.ToArray()),
                Investimentos12MesesValoresJson = JsonSerializer.Serialize(totais12meses.Values.ToArray()),

                FluxoCaixa12MesesRotulosJson = JsonSerializer.Serialize(receitasDespesas12Meses.Keys.ToArray()),
                FluxoCaixa12MesesReceitasJson = JsonSerializer.Serialize(receitasDespesas12Meses.Values.Select(v => v.Receita).ToArray()),
                FluxoCaixa12MesesDespesasJson = JsonSerializer.Serialize(receitasDespesas12Meses.Values.Select(v => v.Despesa).ToArray()),

                InvestimentosPorTiposJson = JsonSerializer.Serialize(investimentosPorTipos),
                DespesasPorFontesJson = JsonSerializer.Serialize(despesasPorFontes)
            };

            return View(viewModel);
        }

        public IActionResult TermosPrivacidade()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
