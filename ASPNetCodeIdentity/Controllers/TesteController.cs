using KissLog;
using Microsoft.AspNetCore.Mvc;
using System;

namespace ASPNetCodeIdentity.Controllers
{
    public class TesteController : Controller
    {

        private readonly ILogger _logger;

        public TesteController(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public IActionResult Index()
        {

            _logger.Error("Log Error!");
            _logger.Trace("Usuario Acessou!");


            return View();
        }
    }
}
