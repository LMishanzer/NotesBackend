using System.Collections.Generic;
using System.Threading.Tasks;
using NotesBackend.Models;
using NotesBackend.Models.Dto;

namespace NotesBackend.Storage
{
    public interface IStorage
    {
        Task<ICollection<Note>> GetAllNotes();
        Task<Note> GetNote(string id);
        Task<ICollection<Note>> GetUserNotes(string userId);
        Task<Note> CreateNote(NoteDto noteDto, string userId);
        Task UpdateNote(Note note);
        Task DeleteNote(string noteId);

        Task<User> CreateUser(User user);
        Task<User> GetUserByCredentials(string username, string password);
        Task<bool> IsUserExists(string username);
        Task<ICollection<User>> GetAllUsers();
    }
}
