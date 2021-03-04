using NotesBackend.Models.Dto;

namespace NotesBackend.Models
{
    public class Note : NoteDto
    {
        public string Id { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }

        public Note() { }

        public Note(NoteDto noteDto)
        {
            Title = noteDto.Title;
            Content = noteDto.Content;
        }
    }
}
