using System;
using System.Collections.Generic;

namespace NotesApp.Domain.Entities
{
    public class Note
    {
        public int Id { get; set; }

        public string Title { get; set; } = "";
        public string Content { get; set; } = "";
        public Priority Priority { get; set; }

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedOn { get; set; }

       
        public ICollection<NoteTag> NoteTags { get; set; } = new List<NoteTag>();

      
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

      
        public int? UserId { get; set; }
        public User? User { get; set; }
    }
}
