namespace NotesApp.Domain.Entities
{
    public class Comment
    {
        public int Id { get; set; }
        public string Text { get; set; } = "";

        public DateTime CreatedOn { get; set; } = DateTime.UtcNow;

        public int NoteId { get; set; }
        public Note Note { get; set; }
    }
}
