/*
using System;
using ASPNetCodeIdentity.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(ASPNetCodeIdentity.Areas.Identity.IdentityHostingStartup))]
namespace ASPNetCodeIdentity.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<ASPNetCodeIdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("ASPNetCodeIdentityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<ASPNetCodeIdentityContext>();
            });
        }
    }
}
*/