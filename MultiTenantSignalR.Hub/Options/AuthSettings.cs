using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Options
{
    public class AuthSettings
    {
        public string Authority { get; set; }
        public Guid ClientId { get; set; }
        public string[] ValidAudiences { get; set; }
        public string[] ValidIssuers { get; set; }
    }
}
