namespace QuickNotes.Business.DTOs.Note.Requests;

public class CreateNoteRequest
{
    public string Title { get; set; }
    public string Text { get; set; }
    public int AppUserId { get; set; }
}