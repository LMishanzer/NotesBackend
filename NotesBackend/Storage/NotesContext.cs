using Microsoft.EntityFrameworkCore;
using NotesBackend.Models;

namespace NotesBackend.Storage
{
    public sealed class NotesContext : DbContext
    {
        public DbSet<Note> Notes { get; set; }
        public DbSet<User> Users { get; set; }

        public NotesContext(DbContextOptions<NotesContext> options)
            : base(options) { }
    }
}
