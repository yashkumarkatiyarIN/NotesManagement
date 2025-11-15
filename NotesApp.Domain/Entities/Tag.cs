namespace NotesApp.Domain.Entities
{
    public class Tag
    {
        public int Id { get; set; }
        public string Name { get; set; } = "";

        public ICollection<NoteTag> NoteTags { get; set; } = new List<NoteTag>();
    }
}
