using ChatApp.Models;
using MongoDB.Driver;

namespace ChatApp.Services
{
    public class UserService
    {
        private readonly IMongoCollection<User> _users;
        public UserService(IConfiguration config)
        {
            var client = new MongoClient(config["MongoDB:ConnectionString"]);
            var db = client.GetDatabase(config["MongoDB:DatabaseName"]);
            _users = db.GetCollection<User>(config["MongoDB:UsersCollection"]);
        }

        public async Task<User?> GetByIdAsync(string id)
        {
            return await _users.Find(u => u.Id == id).FirstOrDefaultAsync();
        }
        public async Task<User?> GetByUsernameAsync(string username)
        {
            return await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
        }

        public async Task<User?> GetByEmailAsync(string email)
        {
            return await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
        }

        public async Task<List<User>> GetAllAsync()
        {
            return await _users.Find(_ => true).ToListAsync();
        }

        public async Task<User?> RegisterAsync(string username, string email, string password)
        {
            var existing = await _users.Find(u => u.Email == email).FirstOrDefaultAsync();
            if (existing != null) return null;

            var user = new User
            {
                Username = username,
                Email = email,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password)
            };
            await _users.InsertOneAsync(user);
            return user;
        }

        public bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }

        public async Task<List<object>> GetAllOnlineUsersAsync()
        {
            var filter = Builders<User>.Filter.Ne(u => u.ConnectionId, null);
            var projection = Builders<User>.Projection.Include(u => u.Id).Include(u => u.Username);

            var results = await _users.Find(filter)
                              .Project(u => new
                              {
                                  id = u.Id,
                                  username = u.Username
                              })
                              .ToListAsync();

            return results.Cast<object>().ToList();
        }

        public async Task UpdateConnectionIdAsync(string userId, string connectionId)
        {
            var update = Builders<User>.Update.Set(u => u.ConnectionId, connectionId);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task ClearConnectionIdAsync(string userId)
        {
            var update = Builders<User>.Update.Unset(u => u.ConnectionId);
            await _users.UpdateOneAsync(u => u.Id == userId, update);
        }

        public async Task<User?> GetByConnectionIdAsync(string connectionId)
        {
            return await _users.Find(u => u.ConnectionId == connectionId).FirstOrDefaultAsync();
        }

        public async Task<User?> AuthenticateAsync(string username, string password)
        {
            var user = await _users.Find(u => u.Username == username).FirstOrDefaultAsync();
            if (user != null && BCrypt.Net.BCrypt.Verify(password, user.PasswordHash))
                return user;

            return null;
        }
    }
}
