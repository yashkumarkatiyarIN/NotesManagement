using NotesApp.Application.DTOs;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace NotesApp.Application.Services;

public interface INoteService
{
    Task<List<NoteDto>> GetAllAsync();
    Task<NoteDto?> GetByIdAsync(int id);
    Task<int> CreateAsync(string title, string content, Priority priority, IEnumerable<string> tagNames, int? userId = null);
    Task UpdateAsync(int id, string title, string content, Priority priority, IEnumerable<string> tagNames);
    Task DeleteAsync(int id);
}

public class NoteService : INoteService
{
    private readonly INoteRepository _notes;
    private readonly ITagRepository _tags;
    private readonly IUnitOfWork _uow;

    public NoteService(INoteRepository notes, ITagRepository tags, IUnitOfWork uow)
    {
        _notes = notes;
        _tags = tags;
        _uow = uow;
    }

    public async Task<List<NoteDto>> GetAllAsync()
    {
        var notes = await _notes.GetAllAsync();

        // Map to DTO, include tags & comments
        return notes
            .OrderByDescending(n => n.UpdatedOn ?? n.CreatedOn)
            .Select(n => new NoteDto(
                n.Id,
                n.Title,
                n.Content,
                (int)n.Priority,
                n.CreatedOn,
                n.UpdatedOn,
                n.User?.Username,
                n.NoteTags.Select(nt => nt.Tag!.Name),
                n.Comments.Select(c => (c.Id, c.Text, c.CreatedOn))
            ))
            .ToList();
    }

    public async Task<NoteDto?> GetByIdAsync(int id)
    {
        var note = await _notes.GetByIdAsync(id);
        if (note == null) return null;
        return new NoteDto(
            note.Id,
            note.Title,
            note.Content,
            (int)note.Priority,
            note.CreatedOn,
            note.UpdatedOn,
            note.User?.Username,
            note.NoteTags.Select(nt => nt.Tag!.Name),
            note.Comments.Select(c => (c.Id, c.Text, c.CreatedOn))
        );
    }

    public async Task<int> CreateAsync(string title, string content, Priority priority, IEnumerable<string> tagNames, int? userId = null)
    {
        var note = new Note
        {
            Title = title,
            Content = content,
            Priority = priority,
            CreatedOn = DateTime.UtcNow
        };

        // Attach tags
        foreach (var tn in tagNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var tag = await _tags.FindByNameAsync(tn) ?? await _tags.AddAsync(new Tag { Name = tn });
            note.NoteTags.Add(new NoteTag { Tag = tag });
        }

        if (userId.HasValue) note.UserId = userId.Value;

        await _notes.AddAsync(note);
        await _uow.SaveChangesAsync();
        return note.Id;
    }

    public async Task UpdateAsync(int id, string title, string content, Priority priority, IEnumerable<string> tagNames)
    {
        var note = await _notes.GetByIdAsync(id);
        if (note == null) throw new KeyNotFoundException("Note not found");
        note.Title = title;
        note.Content = content;
        note.Priority = priority;
        note.UpdatedOn = DateTime.UtcNow;

        // Sync tags
        note.NoteTags.Clear();
        foreach (var tn in tagNames.Distinct(StringComparer.OrdinalIgnoreCase))
        {
            var tag = await _tags.FindByNameAsync(tn) ?? await _tags.AddAsync(new Tag { Name = tn });
            note.NoteTags.Add(new NoteTag { Tag = tag });
        }

        await _notes.UpdateAsync(note);
        await _uow.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var note = await _notes.GetByIdAsync(id); // fetch Note entity
        if (note != null)
        {
            await _notes.DeleteAsync(note); // now passing Note object
            await _uow.SaveChangesAsync();
        }
    }

}