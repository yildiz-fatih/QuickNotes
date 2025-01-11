using QuickNotes.Business.DTOs.Note.Requests;
using QuickNotes.Business.DTOs.Note.Responses;
using QuickNotes.Business.Services.Interfaces;
using QuickNotes.Data.Entities;
using QuickNotes.Data.Repositories;

namespace QuickNotes.Business.Services.Implementations;

public class NoteService : INoteService
{
    private readonly INoteRepository _noteRepository;

    public NoteService(INoteRepository noteRepository)
    {
        _noteRepository = noteRepository;
    }
    
    public async Task<IEnumerable<GetNoteResponse>> GetAllByUserIdAsync(int userId)
    {
        var notes = await _noteRepository.GetAllByUserIdAsync(userId);
        var noteResponses = notes.Select(note => new GetNoteResponse()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated,
            AppUserId = note.AppUserId
        }).ToList();
        
        return noteResponses;
    }

    public async Task<GetNoteResponse> GetByUserIdAsync(int id, int userId)
    {
        var note = await _noteRepository.GetByUserIdAsync(id, userId);
        var noteResponse = new GetNoteResponse()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated,
            AppUserId = note.AppUserId
        };

        return noteResponse;
    }

    public async Task<GetNoteResponse> CreateAsync(CreateNoteRequest request)
    {
        var note = new Note()
        {
            Title = request.Title,
            Text = request.Text,
            DateCreated = DateTime.Now,
            AppUserId = request.AppUserId
        };
        await _noteRepository.CreateAsync(note);

        var noteResponse = new GetNoteResponse()
        {
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated,
            AppUserId = note.AppUserId
        };
        
        return noteResponse;
    }

    public async Task<GetNoteResponse> UpdateAsync(UpdateNoteRequest request)
    {
        var note = await _noteRepository.GetByUserIdAsync(request.Id, request.AppUserId);
        
        note.Title = request.Title;
        note.Text = request.Text;
        await _noteRepository.UpdateAsync(note);

        var noteResponse = new GetNoteResponse()
        {
            Title = note.Title,
            Text = note.Text,
            DateCreated = note.DateCreated,
            AppUserId = note.AppUserId
        };
        
        return noteResponse;
    }

    public async Task<bool> DeleteByUserIdAsync(int id, int userId)
    {
        var isDeleted = await _noteRepository.DeleteByUserIdAsync(id, userId);

        return isDeleted;
    }
}