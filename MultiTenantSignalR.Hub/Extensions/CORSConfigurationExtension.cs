using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Extensions
{
    public static class CORSConfigurationExtension
    {
        public static void ConfigureCORS(this IServiceCollection services)
        {
            services.AddCors(setupAction =>
            {
                setupAction.AddPolicy("default", policy =>
                {
                    policy
                        .WithOrigins("http://localhost:8080")
                        .AllowAnyHeader()
                        .WithMethods("GET", "POST")
                        .AllowCredentials();
                });
            });
        }
    }
}
