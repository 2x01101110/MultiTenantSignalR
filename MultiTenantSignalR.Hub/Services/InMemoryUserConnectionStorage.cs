using MultiTenantSignalR.Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Services
{
    public class InMemoryUserConnectionStorage : IInMemoryUserConnectionStorage
    {
        private List<HubUser> hubUsers = new List<HubUser>();

        public void AddHubUserConnection(HubUser user, string connectionId)
        {
            var hubConnection = new HubConnection
            {
                ConnectionId = connectionId,
                ConnectionStarted = DateTime.UtcNow
            };

            var hubUser = hubUsers.FirstOrDefault(x => x.UserId == user.UserId);

            if (hubUser != null)
            {
                hubUser.HubConnections.Add(hubConnection);
            }
            else
            {
                user.HubConnections.Add(hubConnection);
                hubUsers.Add(user);
            }
        }

        public void RemoveHubUserConnection(HubUser user, string connectionId)
        {
            var hubUser = hubUsers.FirstOrDefault(x => x.UserId == user.UserId);

            if (hubUser != null)
            {
                var hubConnection = hubUser.HubConnections.FirstOrDefault(x => x.ConnectionId == connectionId);

                if (hubConnection != null)
                {
                    hubUser.HubConnections.Remove(hubConnection);
                }
            }
        }        
        
        public IEnumerable<HubUser> GetAllHubUserConnections(string tenantId)
        {
            return hubUsers.Where(x => x.TenantId == tenantId);
        }
    }
}
