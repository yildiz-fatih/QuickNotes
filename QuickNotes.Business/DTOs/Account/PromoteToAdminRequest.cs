namespace QuickNotes.Business.DTOs.Account;

public class PromoteToAdminRequest
{
    public string AdminSecretKey { get; set; }
    public int AppUserId { get; set; }
}