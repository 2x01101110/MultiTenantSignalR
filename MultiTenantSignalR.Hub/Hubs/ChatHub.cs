using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using MultiTenantSignalR.Hub.Models;
using MultiTenantSignalR.Hub.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MultiTenantSignalR.Hub.Hubs
{
    public class ChatHub : BaseHub
    {
        private readonly IInMemoryUserConnectionStorage userConnections;
        private readonly ILogger logger;
        public ChatHub(ILogger<ChatHub> logger, IInMemoryUserConnectionStorage userConnections)
        {
            this.userConnections = userConnections;
            this.logger = logger;
        }

        public override async Task OnConnectedAsync()
        {
            var user = this.User();
            logger.LogInformation($"Connection ${this.Context.ConnectionId} by user {user.UserId}:{user.UserName} was created.");

            userConnections.AddHubUserConnection(user, this.Context.ConnectionId);

            await this.AddConnectionToTenantGroup();
        }

        public Task EchoConnectedHubUsers()
        {
            var connections = userConnections.GetAllHubUserConnections(this.User().TenantId);
            var users = connections.Select(x => new { x.UserId, x.UserName });

            return this.Clients.Caller.SendAsync("message", users);
        }

        public Task Broadcast(string message)
        {
            return this.Clients.Group(this.User().TenantId).SendAsync("message", message);
        }

        public async Task SendToUser(string userId, string message)
        {
            var hubUser = userConnections
                .GetAllHubUserConnections(this.User().TenantId)
                .FirstOrDefault(x => x.UserId == userId);

            if (hubUser != null)
            {
                var connections = hubUser.HubConnections.Select(x => x.ConnectionId).ToList();
                foreach (var connection in connections)
                {
                    await this.Clients.Client(connection).SendAsync("message", message);
                }
            }
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var user = this.User();
            logger.LogInformation($"Connection ${this.Context.ConnectionId} by user {user.UserId}:{user.UserName} was closed.");

            userConnections.RemoveHubUserConnection(this.User(), this.Context.ConnectionId);

            await this.RemoveConnectionFromTenantGroup();
        }
    }
}
