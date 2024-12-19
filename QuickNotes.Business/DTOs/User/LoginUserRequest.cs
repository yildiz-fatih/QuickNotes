namespace QuickNotes.Business.DTOs.User;

public class LoginUserRequest
{
    public string Email { get; set; }
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}