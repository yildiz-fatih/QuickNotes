namespace QuickNotes.Business.DTOs.Account.Requests;

public class PromoteToAdminRequest
{
    public string AdminSecretKey { get; set; }
    public int AppUserId { get; set; }
}