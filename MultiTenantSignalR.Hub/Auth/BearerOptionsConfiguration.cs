using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MultiTenantSignalR.Hub.Options;
using System;

namespace MultiTenantSignalR.Hub.Auth
{
    public class BearerOptionsConfiguration : IConfigureNamedOptions<JwtBearerOptions>
    {
        private readonly IIssuerValidator issuerValidator;
        private readonly AuthSettings settings;

        public BearerOptionsConfiguration(IIssuerValidator issuerValidator, IOptions<AuthSettings> options)
        {
            this.issuerValidator = issuerValidator;
            this.settings = options.Value;
        }

        public void Configure(string name, JwtBearerOptions options)
        {
            // If/Else and check name in case multiple auhtentication schemes

            options.Authority = settings.Authority;
            options.Audience = settings.ClientId.ToString();
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateAudience = true,
                ValidAudiences = settings.ValidAudiences,
                ValidateIssuer = true,
                ValidateIssuerSigningKey = true,
                IssuerValidator = (issuer, token, paramterers) => issuerValidator.ValidateIssuer(issuer),
                ValidateLifetime = true
            };
        }

        public void Configure(JwtBearerOptions options)
        {
            throw new NotImplementedException();
        }
    }
}
