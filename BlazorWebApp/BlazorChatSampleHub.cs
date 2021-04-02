using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace SocialMediaApplication
{
    public class BlazorChatSampleHub : Hub
    {
        public const string HubUrl = "/chat";

        public async Task BroadcastMessage(string username, string message, string postId)
        {
            await Clients.All.SendAsync("BroadcastMessage", username, message, postId);
        }

        public async Task BroadcastLike(string username, string message, string postId)
        {
            await Clients.All.SendAsync("BroadcastLike", username, message, postId);
        }

        public override Task OnConnectedAsync()
        {
            Console.WriteLine($"{Context.ConnectionId} connected");
            return base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception e)
        {
            Console.WriteLine($"Disconnected {e?.Message} {Context.ConnectionId}");
            await base.OnDisconnectedAsync(e);
        }
    }
}