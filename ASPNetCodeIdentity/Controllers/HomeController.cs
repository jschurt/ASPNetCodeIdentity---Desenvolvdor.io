using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ASPNetCodeIdentity.Models;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using ASPNetCodeIdentity.Extensions;

namespace ASPNetCodeIdentity.Controllers
{

    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
            return View();
        }

        [Authorize(Roles="Admin, Gestor")]
        public IActionResult Secret()
        {

            return View();
        }


        [Authorize(Policy = "PodeExcluir")]
        public IActionResult SecretClaim()
        {

            return View(nameof(Secret));
        }

        //Usando policies - ver startup.cs e AuthorizationHelper.cs (Recomendacao da Microsoft)
        [Authorize(Policy = "PodeEscrever")]

        public IActionResult SecretClaimCustomMicrosoft()
        {

            return View(nameof(Secret));
        }

        //Sem usar policies - usando apenas o atributo customizado CustomAuthorizationHelper (bem mais simples que o metodo microsoft) 
        [ClaimsAuthorize("Produtos", "Ler")]
        public IActionResult ClaimCustom()
        {

            return View();
        }


        public IActionResult Privacy()
        {
            //Plantando erro para testar tratamento de erros
            throw new Exception("Erro");

            return View();
        }

        /// <summary>
        /// Pagina de Tratamento de erro personalizado
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Route("erro/{statusCode:length(3,3)}")]
        public IActionResult Error(int statusCode)
        {

            var modelError = new ErrorViewModel();

            switch (statusCode)
            {
                case 500:
                    modelError.Mensagem = "Ocorreu um erro! Tente novamente mais tarde ou contate o nosso suporte.";
                    modelError.Titulo = "Ocorreu um erro!";
                    modelError.ErrCode = statusCode;
                    break;

                case 404:
                    modelError.Mensagem = "A pagina que voce esta procurando nao existe! <br />Em caso de duvida entre em contato com o nosso suporte.";
                    modelError.Titulo = "Ops! Pagina nao encontrada.";
                    modelError.ErrCode = statusCode;
                    break;

                case 403:
                    modelError.Mensagem = "Voce nao tem permissao para fazer isto.";
                    modelError.Titulo = "Acesso negado.";
                    modelError.ErrCode = statusCode;
                    break;

                default:
                    return StatusCode(statusCode);

            }

            return View("Error", modelError);
        }
    }
}
