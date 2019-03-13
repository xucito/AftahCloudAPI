using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AftahCloudAPI.Infrastructure.SecretsManager;
using AftahCloudAPI.Persistence.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Migrations.Internal;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System.Runtime.InteropServices;

namespace AftahCloudAPI.Presentation
{
    public partial class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            if(Configuration.GetValue<string>("Secret:FilePath") == null || Configuration.GetValue<string>("Secret:FilePath") == "")
            {
                bool isWindows = System.Runtime.InteropServices.RuntimeInformation
                       .IsOSPlatform(OSPlatform.Windows);
                SecretManager = new SecretManager(isWindows ? "C://ProgramData//Docker//secrets//secrets.json": "/run/secrets/secrets.json");
            }
            else
            {
                SecretManager = new SecretManager(Configuration.GetValue<string>("Secret:FilePath"));
            }
        }

        public IConfiguration Configuration { get; }
        public SecretManager SecretManager { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseNpgsql("Server=localhost;port=5432;Database=AftahDB;user id=defaultUser;password=P@ssw0rd123!", b =>
                        b.MigrationsAssembly(typeof(ApplicationDbContext).GetTypeInfo().Assembly.GetName().Name));
                });

            BootstrapIdentity(services);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }
        
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();
            app.UseIdentityServer();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
