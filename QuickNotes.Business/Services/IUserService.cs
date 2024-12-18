using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.User;

namespace QuickNotes.Business.Services;

public interface IUserService
{
    public Task<IdentityResult> RegisterAsync(RegisterUserRequest registerUserRequest);
    public Task<SignInResult> LoginAsync(LoginUserRequest loginUserRequest);
    public Task LogOutAsync();
}