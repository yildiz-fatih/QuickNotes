namespace QuickNotes.Business.DTOs.Account.Responses;

public class RegisterResponse
{
    public bool Succeeded { get; set; }
    public ICollection<ErrorResponse> Errors { get; set; } = [];
}