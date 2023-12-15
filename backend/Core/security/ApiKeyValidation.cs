using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Core.Interface.security;
using Microsoft.Extensions.Configuration;

namespace Core.security
{
    public class ApiKeyValidation : IApiKeyValidation
    {
        private IConfiguration _configuration;

        public ApiKeyValidation(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Validate(string apiKey)
        {
            var apiKeyFromConfig = _configuration.GetValue<string>("ApiKeys:KeyOne");

            return apiKey == apiKeyFromConfig;
        }
    }
}