using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Models
{
    public class HubConnection
    {
        public string ConnectionId { get; set; }
        public DateTime ConnectionStarted { get; set; }
        public DateTime ConnectionEnded { get; set; }
    }
}
