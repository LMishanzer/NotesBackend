using System.Collections.Generic;
using System.Threading.Tasks;
using NotesBackend.Models;
using NotesBackend.Services.Interfaces;
using NotesBackend.Storage;

namespace NotesBackend.Services
{
    public class UserService : IUserService
    {
        private readonly IStorage _storage;

        public UserService(IStorage storage)
        {
            _storage = storage;
        }

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _storage.GetUserByCredentials(username, password);

            // return null if user not found
            // authentication successful so return user details without password
            return user?.WithoutPassword();
        }

        public async Task<User> Register(string username, string password)
        {
            return await _storage.CreateUser(new User
            {
                Username = username,
                Password = password
            });
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            return await _storage.GetAllUsers();
        }
    }
}
