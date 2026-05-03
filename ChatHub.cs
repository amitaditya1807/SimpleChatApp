using Microsoft.AspNetCore.SignalR;

namespace ChatServer
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string room, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room)
                .SendAsync("ReceiveMessage", "System", $"{user} joined", "");
        }

        public async Task SendMessage(string room, string user, string message, string imageBase64)
        {
            await Clients.Group(room)
                .SendAsync("ReceiveMessage", user, message, imageBase64);
        }

        public async Task LeaveRoom(string room, string user)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);
            await Clients.Group(room)
                .SendAsync("ReceiveMessage", "System", $"{user} left", "");
        }
    }
}