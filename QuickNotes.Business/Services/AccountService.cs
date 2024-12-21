using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.Account;
using QuickNotes.Data.Entities;

namespace QuickNotes.Business.Services;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest)
        {
            var appUser = new AppUser()
            {
                FullName = registerRequest.FullName,
                UserName = registerRequest.UserName,
                Email = registerRequest.Email
            };
                
            var createResult = await _userManager.CreateAsync(appUser, registerRequest.Password);
            if (!createResult.Succeeded)
            {
                return createResult;
            }

            // Assign the default role of "User"
            await _userManager.AddToRoleAsync(appUser, "User");

            return IdentityResult.Success;
        }

    public async Task<SignInResult> LogInAsync(LogInRequest logInRequest)
    {
        var user = await _userManager.FindByEmailAsync(logInRequest.Email);
        if (user != null)
        {
            await _signInManager.SignOutAsync();

            return await _signInManager.PasswordSignInAsync(user, logInRequest.Password,
                logInRequest.RememberMe, false);
        }

        return SignInResult.Failed;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }

    public async Task<IdentityResult> PromoteToAdminAsync(PromoteToAdminRequest promoteToAdminRequest)
    {
        if (promoteToAdminRequest.AdminSecretKey != "hello")
        {
            return IdentityResult.Failed(new IdentityError { Description = "Invalid secret key" });
        }

        var user = await _userManager.FindByIdAsync(promoteToAdminRequest.AppUserId.ToString());

        // Remove the user from the "User" role
        if (await _userManager.IsInRoleAsync(user, "User"))
        {
            await _userManager.RemoveFromRoleAsync(user, "User");
        }

        // Add the user to the "Admin" role
        await _userManager.AddToRoleAsync(user, "Admin");
        
        // Refresh the user’s sign-in so the role claims update
        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(user, false);
        
        return IdentityResult.Success;
    }
}