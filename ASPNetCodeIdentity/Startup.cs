using ASPNetCodeIdentity.Configuration;
using ASPNetCodeIdentity.Extensions;
using KissLog.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace ASPNetCodeIdentity
{
    public class Startup
    {

        public IConfiguration _configuration { get; }

        /// <summary>
        /// Startup Customizada. Com esta startup (diferente da padrao), podemos trabalhar com os diferentes tipos de environment
        /// corretamente. (Production, Staging, Development)
        /// </summary>
        /// <param name="hostEnvironment"></param>
        public Startup(IWebHostEnvironment hostEnvironment)
        {

            var builder = new ConfigurationBuilder()
                .SetBasePath(hostEnvironment.ContentRootPath)
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{hostEnvironment.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables();

            //Armazenando dados com UserSecrets
            //Botao Direito do Mouse No Projeto -> Manage User Secrets -> Copiar dados sensiveis no arquivo secrets.json
            //Note: Apenas na maquina de desenvolvimento. (Os dados nao serao copiados para o repositorio Git)
            if (hostEnvironment.IsProduction())
            {
                builder.AddUserSecrets<Startup>();
            }

            _configuration = builder.Build();
        }


        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            //Adicionando Identity Config (custom extension method)
            services.AddIdentityConfig(_configuration);

            //Adicionando injecoes de dependencia (custom extension method)
            services.AddDependencyInjections();

            services.AddControllersWithViews(options => {
                //Adicionando um filtro global (que sera acessado em todos os recursos). 
                //Com filtro global, nao eh necessario adicionar atributo na pagina 
                options.Filters.Add(typeof(AuditoriaFilter));
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                
                //Toda exception eh erro 500. Logo, ja estou passando o statusCode 500
                app.UseExceptionHandler("/erro/500");

                app.UseStatusCodePagesWithRedirects("/erro/{0}");

                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            //UseAuthentication obrigatorio para o Identity funcionar..
            app.UseAuthentication();
            app.UseAuthorization();

            // app.UseKissLogMiddleware() must to be referenced after app.UseAuthentication(), app.UseSession()

            app.UseKissLogMiddleware(options => {
                new KissLogConfig(_configuration).ConfigureKissLog(options);
            });

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                //IMPORTANTE! Obrigatorio para que as Razor Pages do Identity sejam chamados
                endpoints.MapRazorPages();

            });
        }
    }
}
