using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MultiTenantSignalR.Hub.Options;
using System;
using System.Linq;

namespace MultiTenantSignalR.Hub.Auth
{
    public class IssuerValidator : IIssuerValidator
    {
        private readonly AuthSettings settings;
        private readonly ILogger logger;

        public IssuerValidator(ILogger<IssuerValidator> logger, IOptions<AuthSettings> options)
        {
            this.settings = options.Value;
            this.logger = logger;
        }

        public string ValidateIssuer(string issuer)
        {
            if (settings.ValidIssuers.Count() == 0  || !settings.ValidIssuers.Contains(issuer, StringComparer.Ordinal)) 
            {
                logger.LogError($"Isssuer {issuer} does not exist in valid issuers list.");

                throw new SecurityTokenInvalidIssuerException($"Rejected ID token issuer {issuer}")
                {
                    InvalidIssuer = issuer
                };
            }

            return issuer;
        }
    }
}
