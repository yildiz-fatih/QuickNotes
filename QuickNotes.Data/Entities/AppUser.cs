using Microsoft.AspNetCore.Identity;

namespace QuickNotes.Data.Entities;

public class AppUser : IdentityUser<int>
{
    public string FullName { get; set; }
    
    public ICollection<Note> Notes { get; set; } = new List<Note>();
}