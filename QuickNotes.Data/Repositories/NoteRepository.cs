using Microsoft.EntityFrameworkCore;
using QuickNotes.Data.Entities;

namespace QuickNotes.Data.Repositories;

public class NoteRepository : INoteRepository
{
    private readonly QuickNotesDbContext _dbContext;

    public NoteRepository(QuickNotesDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Note>> GetAllByUserIdAsync(int userId)
    {
        return await _dbContext.Notes
            .Where(note => note.AppUserId == userId)
            .ToListAsync();
    }

    public async Task<Note> GetByUserIdAsync(int id, int userId)
    {
        return await _dbContext.Notes
            .SingleOrDefaultAsync(note => note.Id == id && note.AppUserId == userId);
    }

    public async Task<Note> CreateAsync(Note note)
    {
        await _dbContext.Notes.AddAsync(note);
        await _dbContext.SaveChangesAsync();
        
        return note;
    }

    public async Task<Note> UpdateAsync(Note note)
    {
        _dbContext.Update(note);
        await _dbContext.SaveChangesAsync();
        
        return note;
    }

    public async Task<bool> DeleteByUserIdAsync(int id, int userId)
    {
        var note = await _dbContext.Notes.SingleOrDefaultAsync(note => note.Id == id && note.AppUserId == userId);
        if (note == null) return false;
        
        _dbContext.Remove(note);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}