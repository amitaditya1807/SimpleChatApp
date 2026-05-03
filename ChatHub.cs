using Microsoft.AspNetCore.SignalR;

namespace ChatServer
{
    public class ChatHub : Hub
    {
        public async Task JoinRoom(string room, string user)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, room);

            await Clients.Group(room).SendAsync(
                "ReceiveMessage",
                "System",
                $"{user} joined the room",
                "",
                DateTime.Now.ToString("hh:mm tt")
            );
        }

        public async Task SendMessage(string room, string user, string message)
        {
            await Clients.Group(room).SendAsync(
                "ReceiveMessage",
                user,
                message,
                "",
                DateTime.Now.ToString("hh:mm tt")
            );
        }

        public async Task LeaveRoom(string room, string user)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, room);

            await Clients.Group(room).SendAsync(
                "ReceiveMessage",
                "System",
                $"{user} left the room",
                "",
                DateTime.Now.ToString("hh:mm tt")
            );
        }
    }
}