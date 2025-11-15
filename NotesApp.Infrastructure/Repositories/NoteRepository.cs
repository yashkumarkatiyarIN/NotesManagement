using Microsoft.EntityFrameworkCore;
using NotesApp.Application.Interfaces;
using NotesApp.Domain.Entities;
using NotesApp.Infrastructure.Persistence;

namespace NotesApp.Infrastructure.Repositories
{
    public class NoteRepository : INoteRepository
    {
        private readonly AppDbContext _context;

        public NoteRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Note>> GetAllAsync()
        {
            return await _context.Notes
                .Include(n => n.NoteTags)
    .ThenInclude(nt => nt.Tag)

                .Include(n => n.Comments)
                .OrderByDescending(n => n.UpdatedOn ?? n.CreatedOn)
                .ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(int id)
        {
            return await _context.Notes
                .Include(n => n.NoteTags)
    .ThenInclude(nt => nt.Tag)

                .Include(n => n.Comments)
                .FirstOrDefaultAsync(n => n.Id == id);
        }

        public async Task AddAsync(Note note)
        {
            await _context.Notes.AddAsync(note);
        }

        public async Task UpdateAsync(Note note)
        {
            _context.Notes.Update(note);
            await Task.CompletedTask;  // EF will track changes
        }

        public async Task DeleteAsync(Note note)
        {
            _context.Notes.Remove(note);
            await Task.CompletedTask;
        }
    }
}
