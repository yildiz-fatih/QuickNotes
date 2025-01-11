using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.Business.DTOs.Note.Requests;
using QuickNotes.Business.Services.Interfaces;
using QuickNotes.Web.ViewModels.Note;

namespace QuickNotes.Web.Controllers;

[Authorize]
public class NoteController : Controller
{
    private readonly INoteService _noteService;

    public NoteController(INoteService noteService)
    {
        _noteService = noteService;
    }
    
    public async Task<IActionResult> Index()
    {
        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var noteResponses = await _noteService.GetAllByUserIdAsync(userId);
        var noteViewModels = noteResponses.Select(note => new NoteViewModel()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            FormattedDateCreated = note.DateCreated.ToShortDateString()
        });
        
        return View(noteViewModels);
    }
    
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateNoteViewModel model)
    {
        if (!ModelState.IsValid)
        {
            // If validation fails, show the same form with error messages.
            return View(model);
        }
        
        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var noteRequest = new CreateNoteRequest()
        {
            Title = model.Title,
            Text = model.Text,
            AppUserId = userId
        };
        await _noteService.CreateAsync(noteRequest);
        
        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Edit([FromRoute] int id)
    {
        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var noteResponse = await _noteService.GetByUserIdAsync(id, userId);

        var editNoteViewModel = new EditNoteViewModel()
        {
            Id = id,
            Title = noteResponse.Title,
            Text = noteResponse.Text
        };

        return View(editNoteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(EditNoteViewModel model)
    {
        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        var noteRequest = new UpdateNoteRequest()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text,
            AppUserId = userId
        };
        await _noteService.UpdateAsync(noteRequest);
        
        return RedirectToAction(nameof(Index));
    }

    [HttpPost]
    public async Task<IActionResult> Delete([FromForm] int id)
    {
        var userId = int.Parse(HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier));
        
        await _noteService.DeleteByUserIdAsync(id, userId);
        
        return RedirectToAction(nameof(Index));
    }
}