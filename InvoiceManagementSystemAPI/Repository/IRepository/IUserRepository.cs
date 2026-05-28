using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto);
    Task<User> RegisterAsync(RegistrationRequestDTO registrationRequestDto);
    Task<LoginResponseDTO> RefreshAsync(RefreshRequestDTO refreshRequestDto);
}