using AftahCloudAPI.Infrastructure.SecretsManager;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace AftahCloudAPI.Presentation
{
    public class Config
    {
        SecretManager secretManager;
        private List<Client> _clients;
        private List<IdentityResource> _identityResource;
        public Config(List<Client> clients, List<IdentityResource> identityResource)
        {
            _clients = clients;
            _identityResource = identityResource;
        }
        
        public IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
            };
        }

        public IEnumerable<IdentityResource> GetIdentityResources()
        {
            return _identityResource;
        }

        public IEnumerable<Client> GetClients()
        {
            return _clients;
        }
    }
}
