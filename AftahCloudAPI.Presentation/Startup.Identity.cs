using AftahCloud.Domain.Entities.ApplicationUsers;
using AftahCloudAPI.Persistence.Data;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AftahCloudAPI.Presentation
{
    public partial class Startup
    {
        public void BootstrapIdentity(IServiceCollection services)
        {
            services.AddIdentity<ApplicationUser, ApplicationRole>(config =>
            {
                config.SignIn.RequireConfirmedEmail = true;
            })
                .AddEntityFrameworkStores<ApplicationDbContext>()
                .AddDefaultTokenProviders();

            var identityConfig = new Config(GetClients(), GetIdentityResource());

            services
             .AddIdentityServer(
             options =>
             {
                 options.IssuerUri = Configuration.GetValue<string>("OpenIdConnect:IssuerUri");
             })
             .AddDeveloperSigningCredential(true)
             .AddInMemoryPersistedGrants()
             .AddInMemoryIdentityResources(identityConfig.GetIdentityResources())
             .AddInMemoryApiResources(identityConfig.GetApiResources())
             .AddInMemoryClients(identityConfig.GetClients())
             .AddTestUsers()
             .AddAspNetIdentity<ApplicationUser>();
        }

        private List<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = Configuration.GetValue<string>("OpenIdConnect:Implicit:ClientId"),
                    ClientName = Configuration.GetValue<string>("OpenIdConnect:Implicit:ClientName"),
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    ClientUri = Configuration.GetValue<string>("OpenIdConnect:Implicit:ClientUri"),
                    RedirectUris = Configuration.GetSection("OpenIdConnect:Implicit:RedirectUris").GetChildren().Select(x => x.Value).ToList(),
                    PostLogoutRedirectUris =
                    Configuration.GetSection("OpenIdConnect:Implicit:PostLogoutRedirectUris").GetChildren().Select(x => x.Value).ToList(),
                    AllowAccessTokensViaBrowser = true,

                    AllowedScopes = new List<string>
                    {
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        IdentityServerConstants.StandardScopes.Email,
                        "api1"
                    }

                }
        };
    }

        private List<IdentityResource> GetIdentityResource()
        {
            return new List<IdentityResource>
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
                new IdentityResources.Email()
            };
        }
    }
}
