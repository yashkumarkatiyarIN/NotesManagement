using NotesApp.Domain.Entities;

namespace NotesApp.Application.Interfaces;

public interface ITagRepository
{
    Task<Tag?> FindByNameAsync(string name);
    Task<Tag> AddAsync(Tag tag);
    Task<List<Tag>> GetAllAsync();
}
