using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickNotes.Data.Entities;
using QuickNotes.Data.Repositories;

namespace QuickNotes.Data;

public static class DataLayerServiceExtensions
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<QuickNotesDbContext>(options => { options.UseMySQL(connectionString); });
        services.AddIdentity<AppUser, AppRole>()
            .AddEntityFrameworkStores<QuickNotesDbContext>();
        
        services.AddScoped<INoteRepository, NoteRepository>();
        
        return services;
    }
}