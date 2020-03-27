using MultiTenantSignalR.Hub.Models;
using System.Collections.Generic;

namespace MultiTenantSignalR.Hub.Services
{
    public interface IInMemoryUserConnectionStorage
    {
        void AddHubUserConnection(HubUser user, string connectionId);
        void RemoveHubUserConnection(HubUser user, string connectionId);
        IEnumerable<HubUser> GetAllHubUserConnections(string tenantId);
    }
}
