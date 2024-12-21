using Microsoft.AspNetCore.Identity;
using QuickNotes.Business.DTOs.Account;

namespace QuickNotes.Business.Services;

public interface IAccountService
{
    public Task<IdentityResult> RegisterAsync(RegisterRequest registerRequest);
    public Task<SignInResult> LogInAsync(LogInRequest logInRequest);
    public Task LogOutAsync();
    public Task<IdentityResult> PromoteToAdminAsync(PromoteToAdminRequest promoteToAdminRequest);
}