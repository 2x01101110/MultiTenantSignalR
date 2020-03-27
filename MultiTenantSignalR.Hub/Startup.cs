using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MultiTenantSignalR.Hub.Extensions;
using MultiTenantSignalR.Hub.Hubs;
using MultiTenantSignalR.Hub.Services;

namespace MultiTenantSignalR.Hub
{
    public class Startup
    {
        public IConfiguration configuration { get; }

        public Startup(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            services.ConfigureCORS();
            services.ConfigureAuth(configuration);
            services.ConfigureSignalR(configuration);

            services.AddSingleton<IInMemoryUserConnectionStorage, InMemoryUserConnectionStorage>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseRouting();
            app.UseCors("default");

            app.UseHttpsRedirection();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapHub<ChatHub>("/chat");
            });
        }
    }
}
