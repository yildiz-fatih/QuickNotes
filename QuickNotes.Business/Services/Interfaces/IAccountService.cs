using QuickNotes.Business.DTOs.Account.Requests;
using QuickNotes.Business.DTOs.Account.Responses;

namespace QuickNotes.Business.Services.Interfaces;

public interface IAccountService
{
    Task<RegisterResponse> RegisterAsync(RegisterRequest request);
    Task<LoginResponse> LoginAsync(LoginRequest request);
    Task LogoutAsync();
    public Task<PromoteToAdminResponse> PromoteToAdminAsync(PromoteToAdminRequest request);
}