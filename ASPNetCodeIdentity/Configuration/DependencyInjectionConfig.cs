using ASPNetCodeIdentity.Extensions;
using KissLog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace ASPNetCodeIdentity.Configuration
{
    public static class DependencyInjectionConfig
    {

        public static IServiceCollection AddDependencyInjections(this IServiceCollection services)
        {

            #region === Identity DI =======================

            //Registrando Identity Handler customizado
            services.AddSingleton<IAuthorizationHandler, PermissaoNecessariaHandler>();

            #endregion

            #region === KissLog.Net =======================

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<ILogger>((context) =>
            {
                return Logger.Factory.Get();
            });

            #endregion

            services.AddScoped<AuditoriaFilter>();

            return services;
        } //AddDependencyInjections

    } //class

} //namespace
