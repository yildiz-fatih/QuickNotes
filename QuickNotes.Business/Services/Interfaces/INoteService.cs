using QuickNotes.Business.DTOs.Note.Requests;
using QuickNotes.Business.DTOs.Note.Responses;

namespace QuickNotes.Business.Services.Interfaces;

public interface INoteService
{
    public Task<IEnumerable<GetNoteResponse>> GetAllByUserIdAsync(int userId);
    public Task<GetNoteResponse> GetByUserIdAsync(int id, int userId);
    public Task<GetNoteResponse> CreateAsync(CreateNoteRequest request);
    public Task<GetNoteResponse> UpdateAsync(UpdateNoteRequest request);
    public Task<bool> DeleteByUserIdAsync(int id, int userId);
}