using QuickNotes.Data.Entities;

namespace QuickNotes.Data.Repositories;

public interface INoteRepository
{
    public Task<IEnumerable<Note>> GetAllByUserIdAsync(int userId);
    public Task<Note> GetByUserIdAsync(int id, int userId);
    public Task<Note> CreateAsync(Note note);
    public Task<Note> UpdateAsync(Note note);
    public Task<bool> DeleteAsync(int id);
}