using ChatApp.Models;
using MongoDB.Driver;

namespace ChatApp.Services
{
    public class ChatService
    {
        private readonly IMongoCollection<Message> _messages;
        private readonly UserService _userService;

        public ChatService(IConfiguration config, UserService userService)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var db=client.GetDatabase(config["MongoDB:DatabaseName"]);
            _messages = db.GetCollection<Message>(config["MongoDB:MessagesCollection"]);
            _userService = userService;
        }
        public async Task<Message> SaveMessageAsync(string userName, string room, string content)
        {
            var message = new Message
            {
                UserName = userName,
                Content = content,
                IsPrivate = false
            };
            await _messages.InsertOneAsync(message);
            return message;
        }

        public async Task<Message> SavePrivateMessageAsync(string senderId, string receiverId, string content)
        {
            var sender = await _userService.GetByIdAsync(senderId);

            var message = new Message
            {
                SenderId = senderId,
                ReceiverId = receiverId,
                UserName = sender?.Username ?? "Unknown",
                Content = content,
                IsPrivate = true
            };
            await _messages.InsertOneAsync(message);
            return message;
        }

        public async Task<List<Message>> GetPublicMessagesAsync()
        {
            return await _messages.Find(m => !m.IsPrivate).SortBy(m => m.Timestamp).ToListAsync();
        }

        public async Task<List<Message>> GetPrivateMessagesAsync(string userId1, string userId2)
        {
            return await _messages.Find(m =>
                m.IsPrivate &&
                ((m.SenderId == userId1 && m.ReceiverId == userId2) ||
                 (m.SenderId == userId2 && m.ReceiverId == userId1))
            ).SortBy(m => m.Timestamp).ToListAsync();
        }
    }
}
