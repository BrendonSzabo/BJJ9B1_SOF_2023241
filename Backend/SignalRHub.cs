using Backend.Models;
using Microsoft.AspNetCore.SignalR;

namespace Backend
{
    public class SignalRHub : Hub
    {
        public async Task LoadPlayers(Player[] players)
        {
            await Clients.All.SendAsync("LoadPlayers", players);
        }
        public async Task AddPlayer(Player player)
        {
            await Clients.All.SendAsync("AddPlayer", player);
        }
    }
}
