using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Routing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


/// <summary>
/// Helper que implementa uma autorizacao por claims customizada (mais interessante do que a recomendada pela Microsoft)
/// </summary>


namespace ASPNetCodeIdentity.Extensions
{
    public static class CustomAuthorizationHelper
    {

        public static bool ValidarClaimsUsuario(HttpContext context, string claimName, string claimValue)
        {

            if (!context.User.Identity.IsAuthenticated)
                return false;

            if (context.User.Claims.Any(claim => claim.Type == claimName && claim.Value.Contains(claimValue)))
                return true;

            return false;

        }//ValidarClaimsUsuario

    } //CustomAuthorizationHelper


    /// <summary>
    /// Filtro para autorizar usuario verificando se ele possui uma claim. 
    /// Filtros nao podem ser usados como atributo, logo, para criar um atributo utilizaremos a classe ClaimsAuthorizeAttribute
    /// </summary>
    public class RequisitoClaimFilter : IAuthorizationFilter
    {

        private readonly Claim _claim;

        public RequisitoClaimFilter(Claim claim)
        {
            _claim = claim ?? throw new ArgumentNullException(nameof(claim));
        }

        public void OnAuthorization(AuthorizationFilterContext context)
        {

            if (!context.HttpContext.User.Identity.IsAuthenticated)
            {
                context.Result = new RedirectToRouteResult(new RouteValueDictionary(new { area = "Identity", page = "/Account/Login", ReturnUrl = context.HttpContext.Request.Path.ToString() }));
            }
            

            if (!CustomAuthorizationHelper.ValidarClaimsUsuario(context.HttpContext, _claim.Type, _claim.Value))
            {
                //Voltando acesso negado
                context.Result = new StatusCodeResult(403);
            }

        }

    } //RequisitoClaimFilter

    /// <summary>
    /// Classe para criar o atributo de autorizacao personalizada baseada nas claims
    /// </summary>
    public class ClaimsAuthorizeAttribute : TypeFilterAttribute
    {
        public ClaimsAuthorizeAttribute(string claimName, string claimValue) : base(typeof(RequisitoClaimFilter))
        {
            Arguments = new object[] { new Claim(claimName, claimValue) };
        }

    } //ClaimsAuthorizeAttribute

}
