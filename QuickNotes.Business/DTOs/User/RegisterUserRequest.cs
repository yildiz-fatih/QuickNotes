namespace QuickNotes.Business.DTOs.User;

public class RegisterUserRequest
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public string RoleSelected { get; set; }
}