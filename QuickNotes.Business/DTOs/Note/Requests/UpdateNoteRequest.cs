namespace QuickNotes.Business.DTOs.Note.Requests;

public class UpdateNoteRequest
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public int AppUserId { get; set; }
}