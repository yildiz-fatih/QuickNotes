using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Web.ViewModels.User;

public class AppUserViewModel
{
    [Required(ErrorMessage = "Please enter your full name")]
    public string FullName { get; set; }
    [Required(ErrorMessage = "Please enter your username")]
    public string UserName { get; set; }
    [Required(ErrorMessage = "Please enter your email")]
    [EmailAddress(ErrorMessage = "Please enter a valid email")]
    public string Email { get; set; }
    [Required(ErrorMessage = "Please enter your password")]
    [DataType(DataType.Password, ErrorMessage = "Please enter a password")]
    public string Password { get; set; }
}