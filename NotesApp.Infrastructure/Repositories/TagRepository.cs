using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Persistence;

namespace NotesApp.Infrastructure.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly AppDbContext _db;

        public TagRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<Tag?> FindByNameAsync(string name)
        {
            if (string.IsNullOrWhiteSpace(name)) return null;
            return await _db.Tags.FirstOrDefaultAsync(t => t.Name == name);
        }

        public async Task<Tag> AddAsync(Tag tag)
        {
            await _db.Tags.AddAsync(tag);
            return tag;
        }

        public async Task<List<Tag>> GetAllAsync()
        {
            return await _db.Tags.ToListAsync();
        }
    }
}
