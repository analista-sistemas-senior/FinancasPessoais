using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class DespesaTipoController(IDespesaTipoService despesaTipoService, IDataProtectionProvider provider) : Controller
    {
        private readonly IDespesaTipoService _despesaTipoService = despesaTipoService;
        private readonly IDataProtector _protetor = provider.CreateProtector("DespesaTipo.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var despesasTiposDTOs = await _despesaTipoService.RetornarDespesasTiposPorIdUsuario(User.RetornarIDUsuario());
            return View(despesasTiposDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(DespesaTipoViewModel despesaTipoViewModel)
        {
            if (!ModelState.IsValid) return View(despesaTipoViewModel);

            despesaTipoViewModel.IDUsuario = User.RetornarIDUsuario();
            await _despesaTipoService.CadastrarDespesaTipo(despesaTipoViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idDespesaTipo;
            try 
            {
                idDespesaTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var despesaTipoResultado = await _despesaTipoService.RetornarDespesaTipoAutentica(idDespesaTipo, User.RetornarIDUsuario());
            if (!despesaTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var despesaTipoViewModel = despesaTipoResultado.Dados!.ParaViewModel();
            despesaTipoViewModel.IDDespesaTipoCriptografado = id;

            return View(despesaTipoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(DespesaTipoViewModel despesaTipoViewModel)
        {
            if (!ModelState.IsValid) return View(despesaTipoViewModel);

            try 
            {
                despesaTipoViewModel.IDDespesaTipo = int.Parse(_protetor.Unprotect(despesaTipoViewModel.IDDespesaTipoCriptografado!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            despesaTipoViewModel.IDUsuario = User.RetornarIDUsuario();

            var despesaTipoResultado = await _despesaTipoService.AtualizarDespesaTipo(despesaTipoViewModel.ParaDTO());
            if (!despesaTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = despesaTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idDespesaTipo;
            try 
            {
                idDespesaTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var despesaTipoExcluida = await _despesaTipoService.ExcluirDespesaTipo(idDespesaTipo, User.RetornarIDUsuario());
            if (!despesaTipoExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}