namespace QuickNotes.Business.DTOs.Note;

public class CreateNoteRequest
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int AppUserId { get; set; }
}