namespace QuickNotes.Data.Entities;

public class Note
{
    public int Id { get; set; }
    public string Text { get; set; }
    public DateTime DateCreated { get; set; }
}