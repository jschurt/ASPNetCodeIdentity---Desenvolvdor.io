using Microsoft.AspNetCore.Authorization;
using System;
using System.Threading.Tasks;

/// <summary>
/// Helper que implementa autorizacao por claims recomendada pela Microsoft utilizando policies no startup.cs (nao e' a implementacao mais interessante)
/// </summary>

namespace ASPNetCodeIdentity.Extensions
{
    public class PermissaoNecessaria : IAuthorizationRequirement
    {
        public string Permissao { get; }
        public PermissaoNecessaria(string permissao)
        {
            Permissao = permissao ?? throw new ArgumentNullException(nameof(permissao));
        }
    } //class PermissaoNecessaria


    public class PermissaoNecessariaHandler : AuthorizationHandler<PermissaoNecessaria>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, PermissaoNecessaria requisito)
        {
            if (context.User.HasClaim(c => c.Type == "Permissao" && c.Value.Contains(requisito.Permissao)))
            {
                context.Succeed(requisito);    
            }

            return Task.CompletedTask;

        } //HandleRequirementAsync

    } //class PermissaoNecessariaHandler


}
