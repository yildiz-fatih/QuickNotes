namespace QuickNotes.Business.DTOs.Note;

public class GetNoteResponse
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime DateCreated { get; set; } 
}