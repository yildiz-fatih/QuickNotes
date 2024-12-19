namespace QuickNotes.Business.DTOs.User;

public class PromoteToAdminRequest
{
    public string AdminSecretKey { get; set; }
    public int AppUserId { get; set; }
}