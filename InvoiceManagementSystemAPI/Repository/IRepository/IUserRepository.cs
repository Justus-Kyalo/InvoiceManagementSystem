using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;

namespace InvoiceManagementSystemAPI.Repository.IRepository;

public interface IUserRepository
{
    bool IsUniqueUser(string username);
    Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto);
    Task<User> Register(RegistrationRequestDTO registrationRequestDto);
    Task<LoginResponseDTO> RefreshAsync(RefreshRequestDTO refreshRequestDto);
}