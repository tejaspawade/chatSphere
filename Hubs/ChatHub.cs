using ChatApp.Services;
using Microsoft.AspNetCore.SignalR;

namespace ChatApp.Hubs
{
    public class ChatHub: Hub
    {
        private readonly ChatService _chatService;
        private readonly UserService _userService;

        public ChatHub(ChatService chatService, UserService userService)
        {
            _chatService = chatService;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            string? userId = httpContext?.Session.GetString("UserId");
            if (!string.IsNullOrEmpty(userId))
            {
                await _userService.UpdateConnectionIdAsync(userId, Context.ConnectionId);
            }

            var allUsers = await _userService.GetAllOnlineUsersAsync();
            await Clients.All.SendAsync("UserConnected", allUsers);
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = await _userService.GetByConnectionIdAsync(Context.ConnectionId);
            if (user != null)
            {
                await _userService.ClearConnectionIdAsync(user.Id);
            }

            var allUsers = await _userService.GetAllOnlineUsersAsync();
            await Clients.All.SendAsync("UserDisconnected", allUsers);
        }


        public async Task SendMessage(string userName, string message)
        {
            var msg = await _chatService.SaveMessageAsync(userName,"PublicRoom", message);
            await Clients.All.SendAsync("ReceiveMessage", msg.UserName, msg.Content, msg.Timestamp);
        }

        public async Task SendPrivateMessage(string senderId, string receiverId, string message)
        {
            var msg = await _chatService.SavePrivateMessageAsync(senderId, receiverId, message);
            var receiver = await _userService.GetByIdAsync(receiverId);
            var sender = await _userService.GetByIdAsync(senderId);
            if (receiver != null && !string.IsNullOrEmpty(receiver.ConnectionId) && sender != null)
            {
                await Clients.Client(receiver.ConnectionId).SendAsync("ReceivePrivateMessage", sender.Username, msg.Content, msg.Timestamp);
                await Clients.Caller.SendAsync("ReceivePrivateMessage", sender.Username, msg.Content, msg.Timestamp);
            }
        }

        public async Task LoadMessageHistory()
        {
            var messages = await _chatService.GetPublicMessagesAsync();
            await Clients.Caller.SendAsync("LoadMessageHistory", messages);
        }

        public async Task LoadPrivateChat(string userId1, string userId2)
        {
            var messages = await _chatService.GetPrivateMessagesAsync(userId1, userId2);
            await Clients.Caller.SendAsync("LoadPrivateChat", messages);
        }
    }
}
