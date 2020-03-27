using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using MultiTenantSignalR.Hub.Auth;
using MultiTenantSignalR.Hub.Auth.Policies;
using MultiTenantSignalR.Hub.Options;

namespace MultiTenantSignalR.Hub.Extensions
{
    public static class AuthConfigurationExtension
    {
        public static void ConfigureAuth(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IAuthorizationPolicyProvider, AuthorizationPolicyProvider>();

            services
                .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, null);

            services.AddSingleton<IConfigureOptions<JwtBearerOptions>, BearerOptionsConfiguration>();
            services.AddSingleton<IIssuerValidator, IssuerValidator>();
            services.Configure<AuthSettings>(configuration.GetSection("Auth"));

            services.AddMvc(setup =>
            {
                setup.Filters.Add(new AuthorizeFilter(nameof(DefaultPolicy)));
            });
        }
    }
}
