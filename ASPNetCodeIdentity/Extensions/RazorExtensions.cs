using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Razor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCodeIdentity.Extensions
{
    public static class RazorExtensions
    {

        /// <summary>
        /// Extension method para validar dentro da minha razor page se o usuario possui ou nao uma claim
        /// </summary>
        /// <param name="page"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static bool IfClaim(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorizationHelper.ValidarClaimsUsuario(page.Context, claimName, claimValue);
        }

        /// <summary>
        /// Extension method para habilitar/desabilitar um elemento (exemplo: botao) dentro da minha razor page se o usuario possuir ou nao uma claim
        /// </summary>
        /// <param name="page"></param>
        /// <param name="claimName"></param>
        /// <param name="claimValue"></param>
        /// <returns></returns>
        public static string IfClaimShow(this RazorPage page, string claimName, string claimValue)
        {
            return CustomAuthorizationHelper.ValidarClaimsUsuario(page.Context, claimName, claimValue) ? "" : "disabled";
        }


        public static IHtmlContent IfClaimShow(this IHtmlContent page, HttpContext context, string claimName, string claimValue)
        {
            return CustomAuthorizationHelper.ValidarClaimsUsuario(context, claimName, claimValue) ? page: null;
        }

    } //class
} //namespace
