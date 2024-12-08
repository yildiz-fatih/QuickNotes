using Microsoft.EntityFrameworkCore;
using QuickNotes.Data.Entities;

namespace QuickNotes.Data;

public class QuickNotesDbContext : DbContext
{
    public QuickNotesDbContext(DbContextOptions options) : base(options)
    {
    }
    
    public DbSet<Note> Notes { get; set; }
}