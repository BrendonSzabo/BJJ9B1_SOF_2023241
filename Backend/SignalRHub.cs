using Backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace Backend
{
    public class SignalRHub : Hub
    {
        private static readonly Dictionary<string, string> UserConnections = new Dictionary<string, string>();

        public override Task OnConnectedAsync()
        {
            Clients.Caller.SendAsync("Connected", Context.ConnectionId);
            return base.OnConnectedAsync();
        }

        public override Task OnDisconnectedAsync(Exception exception)
        {
            Clients.Caller.SendAsync("Disconnected", Context.ConnectionId);
            return base.OnDisconnectedAsync(exception);
        }

        public async Task RegisterUser(string userName)
        {
            lock (UserConnections)
            {
                UserConnections[userName] = Context.ConnectionId;
            }
        }

        public async Task SendMessage(string user, string message, string destination)
        {
            if (UserConnections.TryGetValue(destination, out var connectionId) && UserConnections.TryGetValue(user, out var originId))
            {
                await Clients.Client(connectionId).SendAsync("ReceiveMessage", user, message);
                if (originId != connectionId)
                {
                    await Clients.Client(originId).SendAsync("ReceiveMessage", user, message);
                }
            }
        }
    }
}
