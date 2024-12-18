namespace QuickNotes.Data.Entities;

public class Note
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Text { get; set; }
    public DateTime DateCreated { get; set; }
    
    public int AppUserId { get; set; }
    public AppUser AppUser { get; set; }
}