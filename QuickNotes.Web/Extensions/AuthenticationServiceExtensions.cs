using Microsoft.AspNetCore.Authentication.Cookies;

namespace QuickNotes.Web.Extensions;

public static class AuthenticationServiceExtensions
{
    public static IServiceCollection AddAuthenticationServices(this IServiceCollection services)
    {
        services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
            .AddCookie();

        services.ConfigureApplicationCookie(options =>
        {
            options.Cookie.Name = "QuickNotesAuthCookie"; // Custom cookie name
            options.LoginPath = "/Account/LogIn";        // Redirect here if not authenticated
            options.AccessDeniedPath = "/Account/AccessDenied"; // Redirect here if unauthorized
            options.ExpireTimeSpan = TimeSpan.FromHours(2);
            options.SlidingExpiration = true;
            options.Cookie.HttpOnly = true;
            options.Cookie.SameSite = SameSiteMode.Lax;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        });

        return services;
    }
}