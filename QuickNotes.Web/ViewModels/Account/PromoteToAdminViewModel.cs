using System.ComponentModel.DataAnnotations;

namespace QuickNotes.Web.ViewModels.Account;

public class PromoteToAdminViewModel
{
    [Required(ErrorMessage = "Please enter the secret key")]
    public string AdminSecretKey { get; set; }
}