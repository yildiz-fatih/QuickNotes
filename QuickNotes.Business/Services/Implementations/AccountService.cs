using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.Account.Requests;
using QuickNotes.Business.DTOs.Account.Responses;
using QuickNotes.Business.Services.Interfaces;
using QuickNotes.Data.Entities;

namespace QuickNotes.Business.Services.Implementations;

public class AccountService : IAccountService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public AccountService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<RegisterResponse> RegisterAsync(RegisterRequest registerRequest)
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
                return new RegisterResponse
                {
                    Succeeded = false,
                    Errors = createResult.Errors
                        .Select(e => new ErrorResponse
                        {
                            Code = e.Code,
                            Description = e.Description
                        })
                        .ToList()
                };
            }

            // Assign the default role of "User"
            await _userManager.AddToRoleAsync(appUser, "User");

            return new RegisterResponse
            {
                Succeeded = true
            };
        }

    public async Task<LoginResponse> LoginAsync(LoginRequest loginRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginRequest.Email);

        if (user == null)
        {
            return new LoginResponse() { Succeeded = false };
        }
        
        // Sign out any existing user
        await _signInManager.SignOutAsync();

        var signInResult = await _signInManager.PasswordSignInAsync(user, loginRequest.Password,
            loginRequest.RememberMe, lockoutOnFailure: false);
        
        return new LoginResponse() { Succeeded = signInResult.Succeeded };
    }

    public async Task LogoutAsync()
    {
        await _signInManager.SignOutAsync();
    }
    
    public async Task<PromoteToAdminResponse> PromoteToAdminAsync(PromoteToAdminRequest promoteToAdminRequest)
    {
        if (promoteToAdminRequest.AdminSecretKey != "hello")
        {
            return new PromoteToAdminResponse
            {
                Succeeded = false,
                Errors =
                [
                    new ErrorResponse { Description = "Invalid secret key" }
                ]
            };
        }

        var user = await _userManager.FindByIdAsync(promoteToAdminRequest.AppUserId.ToString());
        if (user == null)
        {
            return new PromoteToAdminResponse
            {
                Succeeded = false,
                Errors =
                [
                    new ErrorResponse { Description = "User not found" }
                ]
            };
        }

        // Remove "User" role
        if (await _userManager.IsInRoleAsync(user, "User"))
        {
            await _userManager.RemoveFromRoleAsync(user, "User");
        }

        // Add the user to the "Admin" role
        await _userManager.AddToRoleAsync(user, "Admin");

        // Refresh the user’s sign-in so the new role claims apply
        await _signInManager.SignOutAsync();
        await _signInManager.SignInAsync(user, isPersistent: false);

        return new PromoteToAdminResponse { Succeeded = true };
    }
}