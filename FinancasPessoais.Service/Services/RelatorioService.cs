using FinancasPessoais.Domain.Interfaces;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Service.DTOs.Reports;

namespace FinancasPessoais.Service.Services
{
    public class RelatorioService(IInvestimentoHistoricoRepository investimentoHistoricoRepository, IInvestimentoRepository investimentoRepository, IReceitaRepository receitaRepository, IDespesaRepository despesaRepository) : IRelatorioService
    {
        private readonly IInvestimentoHistoricoRepository _investimentoHistoricoRepository = investimentoHistoricoRepository;
        private readonly IInvestimentoRepository _investimentoRepository = investimentoRepository;
        private readonly IReceitaRepository _receitaRepository = receitaRepository;
        private readonly IDespesaRepository _despesaRepository = despesaRepository;

        public async Task<(decimal totalInvestimento, decimal totalJuro, decimal totalAporte, decimal totalSaque)> RetornarInvestimentosTotais(int idUsuario)
        {
            var investimentos = await _investimentoRepository.RetornarInvestimentosPorIdUsuario(idUsuario);
            decimal totalInvestimentos = investimentos == null ? 0 : investimentos.Sum(i => i.VLSaldo);

            decimal totalJuros = 0, totalAporte = 0, totalSaque = 0;
            var investimentosHistoricos = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdUsuario(idUsuario);
            if (investimentosHistoricos != null)
            {
                var investimentosHistoricosAgrupados = investimentosHistoricos.GroupBy(ih => ih.IDInvestimento);
                decimal totalJurosGrupo = 0;

                foreach (var grupoInvestimento in investimentosHistoricosAgrupados)
                {
                    totalJurosGrupo = 0;

                    var investimento = grupoInvestimento.First();
                    totalJurosGrupo = investimento.Investimento.VLSaldo - investimento.Investimento.VLInvestimento;

                    var grupoInvestimentoOrdenado = grupoInvestimento.OrderBy(h => h.DTInvestimentoHistorico);

                    foreach (var historico in grupoInvestimentoOrdenado)
                    {
                        totalJurosGrupo -= historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Aporte ? historico.VLInvestimentoHistorico : (historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque ? -1 * historico.VLInvestimentoHistorico : 0);
                        totalAporte += historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Aporte ? historico.VLInvestimentoHistorico : 0;
                        totalSaque += historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque ? historico.VLInvestimentoHistorico : 0;
                    }

                    totalJuros += totalJurosGrupo;
                }
            }

            return (totalInvestimentos, totalJuros, totalAporte, totalSaque);
        }

        public async Task<Dictionary<string, decimal>> RetornarInvestimentosTotais12Meses(int idUsuario)
        {
            var resultado = new Dictionary<string, decimal>();
            var hoje = DateTime.Today;
            var meses = new List<DateTime>();

            for (int i = 12; i >= 0; i--) meses.Add(new DateTime(hoje.Year, hoje.Month, 1).AddMonths(-i));
            foreach (var mes in meses) resultado[mes.ToString("MM/yyyy")] = 0;
            
            var investimentosHistoricos = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdUsuario(idUsuario);
            var idsExcluidos = new Dictionary<string, List<int>>();

            if (investimentosHistoricos != null)
            {
                var investimentosHistoricosAgrupados = investimentosHistoricos.GroupBy(ih => ih.IDInvestimento);

                foreach (var grupoInvestimento in investimentosHistoricosAgrupados)
                {
                    var investimento = grupoInvestimento.First();
                    if (investimento.Investimento.DTVencimento < hoje || investimento.Investimento.FLLiquidado == true) continue;

                    var historicoOrdenado = grupoInvestimento.OrderBy(h => h.DTInvestimentoHistorico).ToList();
                    decimal saldoAtual = investimento.Investimento.VLInvestimento;

                    foreach (var mesReferenca in meses)
                    {
                        var primeiroDiaDoMes = new DateTime(mesReferenca.Year, mesReferenca.Month, 1, 0, 0, 0);
                        var ultimoDiaDoMes = new DateTime(mesReferenca.Year, mesReferenca.Month, DateTime.DaysInMonth(mesReferenca.Year, mesReferenca.Month), 23, 59, 59);

                        if (primeiroDiaDoMes < investimento.Investimento.DTInvestimento) continue;

                        var historicosDoMes = historicoOrdenado.Where(hu => hu.DTInvestimentoHistorico >= primeiroDiaDoMes && hu.DTInvestimentoHistorico <= ultimoDiaDoMes).ToList();
                        foreach (var historico in historicosDoMes)
                        {
                            if (historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saque)
                            {
                                saldoAtual -= historico.VLInvestimentoHistorico;
                            }
                            if (historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Aporte)
                            {
                                saldoAtual += historico.VLInvestimentoHistorico;
                            }
                            if (historico.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saldo)
                            {
                                saldoAtual = historico.VLInvestimentoHistorico;
                            }
                        }

                        string chaveMes = mesReferenca.ToString("MM/yyyy");
                        if (resultado.ContainsKey(chaveMes)) resultado[chaveMes] += saldoAtual;
                        else resultado[chaveMes] = saldoAtual;

                        if (!idsExcluidos.ContainsKey(chaveMes)) idsExcluidos[chaveMes] = [];
                        idsExcluidos[chaveMes].Add(investimento.Investimento.IDInvestimento);
                    }
                }
            }

            var investimentos = await _investimentoRepository.RetornarInvestimentosPorIdUsuario(idUsuario);
            if (investimentos != null)
            {
                foreach (var itemInvestimento in investimentos)
                {
                    if (itemInvestimento.DTVencimento < hoje || itemInvestimento.FLLiquidado == true) continue;

                    foreach (var mesReferenca in meses)
                    {
                        var limiteMesReferencia = new DateTime(mesReferenca.Year, mesReferenca.Month, 1);
                        var inicioInvestimento = new DateTime(itemInvestimento.DTInvestimento.Year, itemInvestimento.DTInvestimento.Month, 1);
                        string chaveMes = mesReferenca.ToString("MM/yyyy");

                        if (inicioInvestimento <= limiteMesReferencia)
                        {
                            if (idsExcluidos.ContainsKey(chaveMes))
                            {
                                if (!idsExcluidos[chaveMes].Contains(itemInvestimento.IDInvestimento))
                                {
                                    resultado[chaveMes] += itemInvestimento.VLInvestimento;
                                }
                            } else resultado[chaveMes] += itemInvestimento.VLInvestimento;
                        }
                    }
                }
            }

            return resultado;
        }

        public async Task<Dictionary<string, (decimal Receita, decimal Despesa)>> RetornarFluxoCaixa12Meses(int idUsuario)
        {
            var fluxoCaixa = new Dictionary<string, (decimal Receita, decimal Despesa)>();
            var hoje = DateTime.Today;
            var meses = new List<DateTime>();

            for (int i = 12; i >= 0; i--) meses.Add(new DateTime(hoje.Year, hoje.Month, 1).AddMonths(-i));
            foreach (var mes in meses) fluxoCaixa[mes.ToString("MM/yyyy")] = (Receita: 0, Despesa: 0);

            var receitas = await _receitaRepository.RetornarReceitasPorIdUsuario(idUsuario);
            var despesas = await _despesaRepository.RetornarDespesasPorIdUsuario(idUsuario);

            foreach (var mesReferenca in meses)
            {
                string chaveMes = mesReferenca.ToString("MM/yyyy");
                decimal totalReceitasMes = 0;
                decimal totalDespesasMes = 0;

                if (receitas != null) totalReceitasMes = receitas.Where(r => r.DTReceita.Month == mesReferenca.Month && r.DTReceita.Year == mesReferenca.Year).Sum(r => r.VLReceita);
                if (despesas != null) totalDespesasMes = despesas.Where(d => d.DTDespesa.Month == mesReferenca.Month && d.DTDespesa.Year == mesReferenca.Year).Sum(d => d.VLDespesa);

                fluxoCaixa[chaveMes] = (Receita: totalReceitasMes, Despesa: totalDespesasMes);
            }

            return fluxoCaixa;
        }

        public async Task<decimal> RetornarDespesasTotais(int idUsuario)
        {
            var despesas = await _despesaRepository.RetornarDespesasPorIdUsuario(idUsuario);
            return despesas == null ? 0 : despesas.Sum(d => d.VLDespesa);
        }

        public async Task<decimal> RetornarReceitasTotais(int idUsuario)
        {
            var despesas = await _receitaRepository.RetornarReceitasPorIdUsuario(idUsuario);
            return despesas == null ? 0 : despesas.Sum(d => d.VLReceita);
        }

        public async Task<List<RelatorioHomeDTO>> RetornarInvestimentosPorTipo(int idUsuario)
        {
            var investimentos = await _investimentoRepository.RetornarInvestimentosPorIdUsuario(idUsuario);
            if (investimentos == null) return [];

            var retorno = investimentos.GroupBy(i => i.InvestimentoTipo.SGInvestimentoTipo).Select(g => new RelatorioHomeDTO(g.Sum(i => i.VLSaldo), g.Key)).ToList();

            return retorno;
        }

        public async Task<List<RelatorioHomeDTO>> RetornarDespesasPorFontes(int idUsuario)
        {
            var despesas = await _despesaRepository.RetornarDespesasPorIdUsuario(idUsuario);
            if (despesas == null) return [];

            var retorno = despesas.GroupBy(d => d.DespesaTipo.NMDespesaTipo).Select(g => new RelatorioHomeDTO(g.Sum(i => i.VLDespesa), g.Key)).ToList();

            return retorno;
        }
        
        public async Task<List<RelatorioInvestimentoVariacaoDTO>> RetornarInvestimentosVariacoes(int idUsuario)
        {
            var retorno = new List<RelatorioInvestimentoVariacaoDTO>();

            var investimentosHistoricos = await _investimentoHistoricoRepository.RetornarInvestimentosHistoricosPorIdUsuario(idUsuario);
            if (investimentosHistoricos == null) return [];

            var investimentosHistoricosAgrupados = investimentosHistoricos.GroupBy(ih => ih.IDInvestimento);
            foreach (var grupoInvestimento in investimentosHistoricosAgrupados)
            {
                var investimento = grupoInvestimento.First();

                var historicoOrdenado = grupoInvestimento.OrderBy(h => h.DTInvestimentoHistorico).ToList();
                var ultimo = historicoOrdenado.Last(h => h.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saldo);

                var dataUltimo = ultimo.DTInvestimentoHistorico;
                var anoAnterior = dataUltimo.Month == 1 ? dataUltimo.Year - 1 : dataUltimo.Year;
                var mesAnterior = dataUltimo.Month == 1 ? 12 : dataUltimo.Month - 1;

                var penultimo = historicoOrdenado.LastOrDefault(h => h.INInvestimentoHistorico == Domain.Enums.TipoInvestimentoHistorico.Saldo && h.DTInvestimentoHistorico.Month <= mesAnterior && h.DTInvestimentoHistorico.Year <= anoAnterior);

                decimal diferencaAbsoluta = 0;
                decimal diferencaPercentual = 0;

                if (penultimo != null)
                {
                    diferencaAbsoluta = ultimo.VLInvestimentoHistorico - penultimo.VLInvestimentoHistorico;
                    if (penultimo.VLInvestimentoHistorico > 0) diferencaPercentual = ((ultimo.VLInvestimentoHistorico - penultimo.VLInvestimentoHistorico) / penultimo.VLInvestimentoHistorico) * 100;
                    else if (penultimo.VLInvestimentoHistorico == 0 && ultimo.VLInvestimentoHistorico > 0) diferencaPercentual = 100;
                }
                else
                {
                    diferencaAbsoluta = ultimo.VLInvestimentoHistorico;
                    diferencaPercentual = 100;
                }

                var objeto = new RelatorioInvestimentoVariacaoDTO(investimento.Investimento.IDInvestimento, investimento.Investimento.NMInvestimento, investimento.Investimento.DTInvestimento, investimento.Investimento.VLSaldo, investimento.Investimento.DTVencimento, investimento.Investimento.PCTaxaRentabilidade, investimento.Investimento.FLLiquidado, diferencaAbsoluta, diferencaPercentual, investimento.Investimento.INTaxaPeriodicidade);
                retorno.Add(objeto);
            }

            return retorno;
        }
    }
}