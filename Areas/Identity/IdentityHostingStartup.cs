using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Project_Kerekes_Denisa_SADE.Data;

[assembly: HostingStartup(typeof(Project_Kerekes_Denisa_SADE.Areas.Identity.IdentityHostingStartup))]
namespace Project_Kerekes_Denisa_SADE.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<IdentityContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("IdentityContextConnection")));

                services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                   .AddRoles<IdentityRole>()
                   .AddEntityFrameworkStores<IdentityContext>();
                
            });
        }
    }
}