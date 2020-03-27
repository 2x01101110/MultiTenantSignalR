using MultiTenantSignalR.Hub.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Hubs
{
    public abstract class BaseHub : Microsoft.AspNetCore.SignalR.Hub
    {
        public virtual HubUser User()
        {
            var user = this.Context.User;
            return new HubUser
            {
                TenantId = user.FindFirst("http://schemas.microsoft.com/identity/claims/tenantid").Value,
                UserId = user.FindFirst("http://schemas.microsoft.com/identity/claims/objectidentifier").Value,
                UserName = user.FindFirst("http://schemas.xmlsoap.org/ws/2005/05/identity/claims/name").Value
            };
        }

        public async Task AddConnectionToTenantGroup()
        {
            await this.Groups.AddToGroupAsync(this.Context.ConnectionId, this.User().TenantId);
        }
        public async Task RemoveConnectionFromTenantGroup()
        {
            await this.Groups.RemoveFromGroupAsync(this.Context.ConnectionId, this.User().TenantId);
        }
    }
}
