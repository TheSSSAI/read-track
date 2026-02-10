using Microsoft.EntityFrameworkCore;
using ReadTrack.Reading.Application.Interfaces;
using ReadTrack.Reading.Domain.Aggregates.Library;
using ReadTrack.Reading.Infrastructure.Persistence;
using ReadTrack.Shared; // For IUnitOfWork

namespace ReadTrack.Reading.Infrastructure.Persistence.Repositories;

/// <summary>
/// EF Core implementation of the Library Repository.
/// </summary>
public class LibraryRepository : ILibraryRepository
{
    private readonly ReadingDbContext _context;

    public LibraryRepository(ReadingDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
    }

    public IUnitOfWork UnitOfWork => _context;

    public async Task AddAsync(LibraryItem item, CancellationToken cancellationToken = default)
    {
        await _context.LibraryItems.AddAsync(item, cancellationToken);
    }

    public void Update(LibraryItem item)
    {
        _context.Entry(item).State = EntityState.Modified;
    }

    public async Task<LibraryItem?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.LibraryItems
            .Include(l => l.Sessions) // Eager load sessions as they are part of the aggregate
            .FirstOrDefaultAsync(l => l.Id == id, cancellationToken);
    }

    public async Task<List<LibraryItem>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.LibraryItems
            .AsNoTracking() // Read-only optimization
            .Include(l => l.Sessions)
            .Where(l => l.UserId == userId)
            .OrderByDescending(l => l.LastModified)
            .ToListAsync(cancellationToken);
    }

    public async Task<LibraryItem?> GetByGoogleBookIdAsync(Guid userId, string googleBookId, CancellationToken cancellationToken = default)
    {
        return await _context.LibraryItems
            .FirstOrDefaultAsync(l => l.UserId == userId && l.Metadata.GoogleBookId == googleBookId, cancellationToken);
    }

    public async Task<int> CountByUserIdAsync(Guid userId, CancellationToken cancellationToken = default)
    {
        return await _context.LibraryItems
            .Where(l => l.UserId == userId)
            .CountAsync(cancellationToken);
    }
}