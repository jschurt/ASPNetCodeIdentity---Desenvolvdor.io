using ASPNetCodeIdentity.Areas.Identity.Data;
using ASPNetCodeIdentity.Extensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ASPNetCodeIdentity.Configuration
{
    public static class IdentityConfig
    {

        public static IServiceCollection AddIdentityConfig(this IServiceCollection services, IConfiguration config)
        {

            //Pacote necessario: Microsoft.AspNetCore.Identity.UI

            services.AddDbContext<ASPNetCodeIdentityContext>(options =>
                options.UseSqlServer(
                    config.GetConnectionString("ASPNetCodeIdentityContextConnection")));

            services
                .AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddDefaultUI()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<ASPNetCodeIdentityContext>();

            //Adicionando regras (claims)
            services.AddAuthorization(options =>
            {
                options.AddPolicy("PodeExcluir", policy => policy.RequireClaim("PodeExcluir"));

                //Fazendo tratamento personalizado (AuthorizationHelper) onde posso criar uma claim unica com multiplos valores
                options.AddPolicy("PodeLer", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeLer")));
                options.AddPolicy("PodeEscrever", policy => policy.Requirements.Add(new PermissaoNecessaria("PodeEscrever")));

            });

            return services;

        } //AddIdentityConfig


    }
}
