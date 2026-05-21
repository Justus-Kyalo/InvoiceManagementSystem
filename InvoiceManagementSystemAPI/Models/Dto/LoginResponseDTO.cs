namespace InvoiceManagementSystemAPI.Models.Dto;

public class LoginResponseDTO
{
    public User User { get; set; }
    public string Token { get; set; }
    public string RefreshToken { get; set; }

    public LoginResponseDTO()
    {
        User = new User();
    }
}