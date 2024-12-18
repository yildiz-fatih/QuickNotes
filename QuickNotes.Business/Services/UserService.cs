using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.User;
using QuickNotes.Data.Entities;

namespace QuickNotes.Business.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserService(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _signInManager = signInManager;
    }

    public async Task<IdentityResult> RegisterAsync(RegisterUserRequest registerUserRequest)
    {
        var appUser = new AppUser()
        {
            FullName = registerUserRequest.FullName,
            UserName = registerUserRequest.UserName,
            Email = registerUserRequest.Email
        };
            
        return await _userManager.CreateAsync(appUser, registerUserRequest.Password);
    }

    public async Task<SignInResult> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginUserRequest.Email);
        if (user != null)
        {
            await _signInManager.SignOutAsync();

            return await _signInManager.PasswordSignInAsync(user, loginUserRequest.Password,
                loginUserRequest.Persistent, loginUserRequest.Lock);
        }

        return SignInResult.Failed;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}