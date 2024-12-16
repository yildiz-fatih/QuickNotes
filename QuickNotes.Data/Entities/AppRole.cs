using Microsoft.AspNetCore.Identity;

namespace QuickNotes.Data.Entities;

public class AppRole : IdentityRole<int>
{
    public DateTime CreatedDate { get; set; }
}