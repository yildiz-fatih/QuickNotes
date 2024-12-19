using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.User;
using QuickNotes.Data.Entities;

namespace QuickNotes.Business.Services;

public class UserService : IUserService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly SignInManager<AppUser> _signInManager;

    public UserService(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager, SignInManager<AppUser> signInManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
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
                
            var createResult = await _userManager.CreateAsync(appUser, registerUserRequest.Password);
            if (!createResult.Succeeded)
            {
                return createResult;
            }

            // Assign the role to the user
            await _userManager.AddToRoleAsync(appUser, registerUserRequest.RoleSelected);

            return IdentityResult.Success;
        }

    public async Task<SignInResult> LoginAsync(LoginUserRequest loginUserRequest)
    {
        var user = await _userManager.FindByEmailAsync(loginUserRequest.Email);
        if (user != null)
        {
            await _signInManager.SignOutAsync();

            return await _signInManager.PasswordSignInAsync(user, loginUserRequest.Password,
                loginUserRequest.RememberMe, false);
        }

        return SignInResult.Failed;
    }

    public async Task LogOutAsync()
    {
        await _signInManager.SignOutAsync();
    }
}