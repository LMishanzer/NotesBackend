using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NotesBackend.Models;

namespace NotesBackend.Storage
{
    public class DbStorage : IStorage
    {
        private readonly NotesContext _notesContext;

        public DbStorage(NotesContext notesContext)
        {
            _notesContext = notesContext;
        }

        public async Task<ICollection<Note>> GetAllNotes()
        {
            return await _notesContext.Notes.ToListAsync();
        }

        public async Task<Note> GetNote(string id)
        {
            return await _notesContext.Notes.SingleAsync(n => n.Id == id);
        }

        public async Task<ICollection<Note>> GetUserNotes(string userId)
        {
            return await _notesContext.Notes.Where(n => n.UserId == userId).ToListAsync();
        }

        public async Task<Note> CreateNote(Note note)
        {
            note.Id = Guid.NewGuid().ToString();
            var result = await _notesContext.Notes.AddAsync(note);
            await _notesContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task UpdateNote(Note note)
        {
            var noteToUpdate = await _notesContext.Notes.SingleAsync(n => n.Id == note.Id);
            noteToUpdate.Title = note.Title;
            noteToUpdate.Content = note.Content;
            noteToUpdate.UserId = note.UserId;
            noteToUpdate.User = note.User;
            await _notesContext.SaveChangesAsync();
        }

        public async Task DeleteNote(string noteId)
        {
            var noteToRemove = await _notesContext.Notes.SingleAsync(n => n.Id == noteId);
            _notesContext.Remove(noteToRemove);
            await _notesContext.SaveChangesAsync();
        }

        public async Task<User> CreateUser(User user)
        {
            user.Id = Guid.NewGuid().ToString();
            var result = await _notesContext.Users.AddAsync(user);
            await _notesContext.SaveChangesAsync();
            return result.Entity;
        }

        public async Task<User> GetUserByCredentials(string username, string password)
        {
            return await _notesContext.Users.SingleOrDefaultAsync(u => u.Username == username && u.Password == password);
        }

        public async Task<ICollection<User>> GetAllUsers()
        {
            return await _notesContext.Users.ToListAsync();
        }
    }
}
