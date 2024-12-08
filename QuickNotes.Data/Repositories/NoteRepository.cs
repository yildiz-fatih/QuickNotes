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

    public async Task<IEnumerable<Note>> GetAllAsync()
    {
        return await _dbContext.Notes.ToListAsync();
    }

    public async Task<Note> GetAsync(int id)
    {
        return await _dbContext.Notes.FindAsync(id);
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

    public async Task<bool> DeleteAsync(int id)
    {
        var note = await _dbContext.Notes.FindAsync(id);
        if (note == null) return false;
        
        _dbContext.Remove(note);
        await _dbContext.SaveChangesAsync();

        return true;
    }
}