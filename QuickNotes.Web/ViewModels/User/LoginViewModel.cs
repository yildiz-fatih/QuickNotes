using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Web.ViewModels.User;

public class LoginViewModel
{
    [Required(ErrorMessage = "Please enter your email address")]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please enter your password")]
    [DataType(DataType.Password, ErrorMessage = "Please enter a valid password")]
    public string Password { get; set; }
    public bool RememberMe { get; set; }
}