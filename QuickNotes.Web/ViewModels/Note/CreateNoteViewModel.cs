using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Web.ViewModels.Note;

public class CreateNoteViewModel
{
    [Required(ErrorMessage = "Please enter a title for the note")]
    public string Title { get; set; }
    [Required(ErrorMessage = "Please enter a content for the note")]
    public string Text { get; set; }
}