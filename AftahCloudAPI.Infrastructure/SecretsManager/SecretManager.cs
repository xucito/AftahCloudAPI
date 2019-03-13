using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;

namespace AftahCloudAPI.Infrastructure.SecretsManager
{
    public class SecretManager
    {
        IConfigurationRoot _configuration { get; }

        public SecretManager(string fullFilePath)
        {
            var secretName = fullFilePath.Split('/').Last();

            int idx = fullFilePath.LastIndexOf('/');
            var path = fullFilePath.Substring(0, idx);

            var builder = new ConfigurationBuilder()
                .SetBasePath(path)
                .AddJsonFile(secretName, optional: true, reloadOnChange: false);

            _configuration = builder.Build();
        }

        public string GetSecret(string key)
        {
            return _configuration.GetSection(key).Value;
        }
    }
}
