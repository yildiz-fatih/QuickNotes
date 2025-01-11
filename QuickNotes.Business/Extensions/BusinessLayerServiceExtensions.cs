using Microsoft.Extensions.DependencyInjection;
using QuickNotes.Business.Services;
using QuickNotes.Business.Services.Implementations;
using QuickNotes.Business.Services.Interfaces;

namespace QuickNotes.Business.Extensions;

public static class BusinessLayerServiceExtensions
{
    public static IServiceCollection AddBusinessServices(this IServiceCollection services)
    {
        services.AddScoped<INoteService, NoteService>();
        services.AddScoped<IAccountService, AccountService>();

        return services;
    }
}