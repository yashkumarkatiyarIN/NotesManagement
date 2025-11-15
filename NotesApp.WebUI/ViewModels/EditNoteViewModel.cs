using System.ComponentModel.DataAnnotations;

public class EditNoteViewModel
{
    public int Id { get; set; }

    [Required, StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, StringLength(4000)]
    public string Content { get; set; } = string.Empty;

    public int Priority { get; set; }

    // comma-separated tags
    public string? Tags { get; set; }
}