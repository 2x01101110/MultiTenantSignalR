using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Models
{
    public class HubUser
    {
        public string TenantId { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public List<HubConnection> HubConnections { get; set; } = new List<HubConnection>();
    }
}
