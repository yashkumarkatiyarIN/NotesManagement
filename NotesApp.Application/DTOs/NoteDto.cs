namespace NotesApp.Application.DTOs;

public record NoteDto(
    int Id,
    string Title,
    string Content,
    int Priority,
    DateTime CreatedOn,
    DateTime? UpdatedOn,
    string? Username,
    IEnumerable<string> Tags,
    IEnumerable<(int Id, string Text, DateTime CreatedOn)> Comments
);
