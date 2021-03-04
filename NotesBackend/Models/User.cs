using System.Collections.Generic;

namespace NotesBackend.Models
{
    public class User
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public ICollection<Note> Notes { get; set; }

        public User WithoutPassword()
        {
            Password = string.Empty;
            return this;
        }
    }
}
