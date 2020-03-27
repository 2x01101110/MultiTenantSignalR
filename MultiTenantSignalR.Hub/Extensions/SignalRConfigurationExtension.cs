using Microsoft.Azure.SignalR;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MultiTenantSignalR.Hub.Extensions
{
    public static class SignalRConfigurationExtension
    {
        public static void ConfigureSignalR(this IServiceCollection services, IConfiguration configuration)
        {
            services
                .AddSignalR()
                .AddAzureSignalR(configure =>
                {
                    configure.ConnectionString = configuration["SignalRConnectionString"];
                    configure.ServerStickyMode = ServerStickyMode.Preferred;
                });
        }
    }
}
