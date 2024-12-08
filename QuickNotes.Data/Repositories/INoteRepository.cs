using QuickNotes.Data.Entities;

namespace QuickNotes.Data.Repositories;

public interface INoteRepository
{
    public Task<IEnumerable<Note>> GetAllAsync();
    public Task<Note> GetAsync(int id);
    public Task<Note> CreateAsync(Note note);
    public Task<Note> UpdateAsync(Note note);
    public Task<bool> DeleteAsync(int id);
}