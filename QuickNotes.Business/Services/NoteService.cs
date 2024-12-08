using QuickNotes.Business.DTOs.Note;
using QuickNotes.Data.Entities;
using QuickNotes.Data.Repositories;

namespace QuickNotes.Business.Services;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public async Task<IEnumerable<GetNoteResponse>> GetAllAsync()
    {
        var notes = await _noteRepository.GetAllAsync();
        var noteResponses = notes.Select(note => new GetNoteResponse()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated
        }).ToList();
        
        return noteResponses;
    }

    public async Task<GetNoteResponse> GetAsync(int id)
    {
        var note = await _noteRepository.GetAsync(id);
        var noteResponse = new GetNoteResponse()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated
        };

        return noteResponse;
    }

    public async Task<GetNoteResponse> CreateAsync(CreateNoteRequest request)
    {
        var note = new Note()
        {
            Title = request.Title,
            Text = request.Text,
            DateCreated = DateTime.Now
        };
        await _noteRepository.CreateAsync(note);

        var noteResponse = new GetNoteResponse()
        {
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated
        };
        
        return noteResponse;
    }

    public async Task<GetNoteResponse> UpdateAsync(UpdateNoteRequest request)
    {
        var note = await _noteRepository.GetAsync(request.Id);
        
        note.Title = request.Title;
        note.Text = request.Text;
        await _noteRepository.UpdateAsync(note);

        var noteResponse = new GetNoteResponse()
        {
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated
        };
        
        return noteResponse;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var isDeleted = await _noteRepository.DeleteAsync(id);

        return isDeleted;
    }
}