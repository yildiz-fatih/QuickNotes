namespace QuickNotes.Business.DTOs.Account.Responses;

public class ErrorResponse
{
    public string Code { get; set; }
    public string Description { get; set; } = string.Empty;
}