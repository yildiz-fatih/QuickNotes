using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using QuickNotes.Business.DTOs.Note;
using QuickNotes.Business.Services;
using QuickNotes.Web.Models;

namespace QuickNotes.Web.Controllers;

public class HomeController : Controller
{
    private readonly INoteService _noteService;

    public HomeController(INoteService noteService)
    {
        _noteService = noteService;
    }

    public async Task<IActionResult> Index()
    {
        var noteResponses = await _noteService.GetAllAsync();
        var noteViewModels = noteResponses.Select(note => new NoteViewModel()
        {
            Id = note.Id,
            Title = note.Title,
            Text = note.Text,
            FormattedDateCreated = note.DateCreated.ToShortDateString()
        });
        
        return View(noteViewModels);
    }

    public IActionResult CreateNote()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> CreateNote(CreateNoteViewModel model)
    {
        var noteRequest = new CreateNoteRequest()
        {
            Title = model.Title,
            Text = model.Text
        };
        await _noteService.CreateAsync(noteRequest);
        
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> EditNote([FromRoute] int id)
    {
        var noteResponse = await _noteService.GetAsync(id);

        var editNoteViewModel = new EditNoteViewModel()
        {
            Id = id,
            Title = noteResponse.Title,
            Text = noteResponse.Text
        };

        return View(editNoteViewModel);
    }

    [HttpPost]
    public async Task<IActionResult> EditNote(EditNoteViewModel model)
    {
        var noteRequest = new UpdateNoteRequest()
        {
            Id = model.Id,
            Title = model.Title,
            Text = model.Text
        };
        await _noteService.UpdateAsync(noteRequest);
        
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> DeleteNote([FromRoute] int id)
    {
        await _noteService.DeleteAsync(id);
        
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
