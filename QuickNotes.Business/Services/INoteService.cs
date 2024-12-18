using QuickNotes.Business.DTOs.Note;

namespace QuickNotes.Business.Services;

public interface INoteService
{
    public Task<IEnumerable<GetNoteResponse>> GetAllByUserIdAsync(int userId);
    public Task<GetNoteResponse> GetByUserIdAsync(int id, int userId);
    public Task<GetNoteResponse> CreateAsync(CreateNoteRequest request);
    public Task<GetNoteResponse> UpdateAsync(UpdateNoteRequest request);
    public Task<bool> DeleteAsync(int id);
}