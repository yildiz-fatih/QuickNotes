using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using QuickNotes.Data.Repositories;

namespace QuickNotes.Data;

public static class DataServiceRegistration
{
    public static IServiceCollection AddDataServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        services.AddDbContext<QuickNotesDbContext>(options => { options.UseMySQL(connectionString); });

        services.AddScoped<INoteRepository, NoteRepository>();
        
        return services;
    }
}