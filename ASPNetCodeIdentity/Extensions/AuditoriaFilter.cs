using KissLog;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCodeIdentity.Extensions
{
    /// <summary>
    /// Filtro para logar qe um usuario acessou um recurso. Para que este filtro seja um filreo global, 
    /// e' necessario registra-lo dentro de startup.cs
    /// </summary>
    public class AuditoriaFilter : IActionFilter
    {

        private readonly ILogger _logger;

        public AuditoriaFilter(ILogger logger)
        {
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        /// <summary>
        /// Este metodo eh disparado DEPOIS que o recurso tiver sido executado
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuted(ActionExecutedContext context)
        {
            //Fazendo log apenas de usuarios autenticados
            if (context.HttpContext.User.Identity.IsAuthenticated)
            { 
                var message = context.HttpContext.User.Identity.Name + " Acessou: " + context.HttpContext.Request.GetDisplayUrl();

                _logger.Info(message);

            }
        }

        /// <summary>
        /// Este metodo eh disparado ANTES do recurso ser executado
        /// </summary>
        /// <param name="context"></param>
        public void OnActionExecuting(ActionExecutingContext context)
        { }
    }
}
