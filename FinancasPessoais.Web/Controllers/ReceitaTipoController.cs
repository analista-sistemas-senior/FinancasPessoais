using FinancasPessoais.Web.Extensions;
using FinancasPessoais.Service.Interfaces;
using FinancasPessoais.Web.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Mvc;

namespace FinancasPessoais.Web.Controllers
{
    [Authorize]
    public class ReceitaTipoController(IReceitaTipoService receitaTipoService, IDataProtectionProvider provider) : Controller
    {
        private readonly IReceitaTipoService _receitaTipoService = receitaTipoService;
        private readonly IDataProtector _protetor = provider.CreateProtector("ReceitaTipo.IdProtetor");

        public async Task<IActionResult> Index()
        {
            var receitasTiposDTOs = await _receitaTipoService.RetornarReceitasTiposPorIdUsuario(User.RetornarIDUsuario());
            return View(receitasTiposDTOs.ParaViewModels(_protetor));
        }

        public async Task<IActionResult> Criacao()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Criacao(ReceitaTipoViewModel receitaTipoViewModel)
        {
            if (!ModelState.IsValid) return View(receitaTipoViewModel);

            receitaTipoViewModel.IDUsuario = User.RetornarIDUsuario();
            await _receitaTipoService.CadastrarReceitaTipo(receitaTipoViewModel.ParaDTO());

            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Atualizacao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return RedirectToAction("Index");

            int idReceitaTipo;
            try 
            {
                idReceitaTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            var receitaTipoResultado = await _receitaTipoService.RetornarReceitaTipoAutentico(idReceitaTipo, User.RetornarIDUsuario());
            if (!receitaTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            var receitaTipoViewModel = receitaTipoResultado.Dados!.ParaViewModel();
            receitaTipoViewModel.IDReceitaTipoCriptografada = id;

            return View(receitaTipoViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Atualizacao(ReceitaTipoViewModel receitaTipoViewModel)
        {
            if (!ModelState.IsValid) return View(receitaTipoViewModel);

            try 
            {
                receitaTipoViewModel.IDReceitaTipo = int.Parse(_protetor.Unprotect(receitaTipoViewModel.IDReceitaTipoCriptografada!));
            }
            catch (System.Security.Cryptography.CryptographicException) 
            {
                TempData["MensagemErro"] = "Tentativa de manipulação de dados detectada";
                return RedirectToAction("Index");
            }

            receitaTipoViewModel.IDUsuario = User.RetornarIDUsuario();

            var receitaTipoResultado = await _receitaTipoService.AtualizarReceitaTipo(receitaTipoViewModel.ParaDTO());
            if (!receitaTipoResultado.Sucesso)
            {
                TempData["MensagemErro"] = receitaTipoResultado.MensagemErro;
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Exclusao(string? id)
        {
            if (string.IsNullOrWhiteSpace(id)) return Json(new { sucesso = false });

            int idReceitaTipo;
            try 
            {
                idReceitaTipo = int.Parse(_protetor.Unprotect(id));
            }
            catch (System.Security.Cryptography.CryptographicException)
            {
                return BadRequest(new { sucesso = false });
            }

            var receitaTipoExcluida = await _receitaTipoService.ExcluirReceitaTipo(idReceitaTipo, User.RetornarIDUsuario());
            if (!receitaTipoExcluida)
            {
                return Json(new { sucesso = false });
            }

            return Json(new { sucesso = true });
        }
    }
}