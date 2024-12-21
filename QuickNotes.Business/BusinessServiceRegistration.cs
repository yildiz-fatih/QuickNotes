using Microsoft.Extensions.DependencyInjection;
using QuickNotes.Business.Services;

namespace QuickNotes.Business;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}