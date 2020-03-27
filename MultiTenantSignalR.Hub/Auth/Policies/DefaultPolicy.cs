using Microsoft.AspNetCore.Authorization;

namespace MultiTenantSignalR.Hub.Auth.Policies
{
    public class DefaultPolicy
    {
        public static AuthorizationPolicy Build()
        {
            var policy = new AuthorizationPolicyBuilder();

            policy.RequireAuthenticatedUser();

            return policy.Build();
        }
    }
}
