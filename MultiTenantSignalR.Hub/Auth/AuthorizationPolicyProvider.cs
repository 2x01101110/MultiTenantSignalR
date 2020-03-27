using Microsoft.AspNetCore.Authorization;
using MultiTenantSignalR.Hub.Auth.Policies;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Auth
{
    public class AuthorizationPolicyProvider : IAuthorizationPolicyProvider
    {
        public Task<AuthorizationPolicy> GetDefaultPolicyAsync() => Task.FromResult(DefaultPolicy.Build());
        public Task<AuthorizationPolicy> GetFallbackPolicyAsync() => Task.FromResult(DefaultPolicy.Build());

        public Task<AuthorizationPolicy> GetPolicyAsync(string policyName)
        {
            if (string.IsNullOrWhiteSpace(policyName))
                return GetDefaultPolicyAsync();
            else
                return GetDefaultPolicyAsync();
        }
    }
}
