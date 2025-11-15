using System.Threading;
using System.Threading.Tasks;
using NotesApp.Application.Interfaces;
using NotesApp.Infrastructure.Persistence;

namespace NotesApp.Infrastructure
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _db;

        public UnitOfWork(AppDbContext db)
        {
            _db = db;
        }

        public async Task<int> SaveChangesAsync(CancellationToken ct = default)
        {
            return await _db.SaveChangesAsync(ct);
        }
    }
}
