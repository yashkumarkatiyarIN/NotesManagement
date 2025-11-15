namespace NotesApp.Domain.Entities;

public class User
{
    public int Id { get; set; }
    public string Username { get; set; } = string.Empty;
    public string? DisplayName { get; set; }

    public ICollection<Note> Notes { get; set; } = new List<Note>();
}
