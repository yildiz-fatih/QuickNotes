using QuickNotes.Business.DTOs.Note;

namespace QuickNotes.Business.Services;

public interface INoteService
{
    public Task<IEnumerable<GetNoteResponse>> GetAllAsync();
    public Task<GetNoteResponse> GetAsync(int id);
    public Task<GetNoteResponse> CreateAsync(CreateNoteRequest request);
    public Task<GetNoteResponse> UpdateAsync(UpdateNoteRequest request);
    public Task<bool> DeleteAsync(int id);
}