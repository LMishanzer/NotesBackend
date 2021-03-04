using System.Collections.Generic;
using System.Threading.Tasks;
using NotesBackend.Models;

namespace NotesBackend.Services.Interfaces
{
    public interface IUserService
    {
        Task<User> Authenticate(string username, string password);
        Task<IEnumerable<User>> GetAll();
        Task<User> Register(string username, string password);
    }
}
