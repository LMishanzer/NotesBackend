using System.Collections.Generic;
using NotesBackend.Models.Dto;

namespace NotesBackend.Models
{
    public class User : UserCredentials
    {
        public string Id { get; set; }
        public ICollection<Note> Notes { get; set; }

        public User WithoutPassword()
        {
            Password = string.Empty;
            return this;
        }
    }
}
