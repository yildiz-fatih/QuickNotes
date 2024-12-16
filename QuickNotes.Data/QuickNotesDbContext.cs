using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using QuickNotes.Data.Entities;

namespace QuickNotes.Data;

public class QuickNotesDbContext : IdentityDbContext<AppUser, AppRole, int>
{
    public QuickNotesDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Note> Notes { get; set; }
}