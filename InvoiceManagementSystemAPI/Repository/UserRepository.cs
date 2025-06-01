using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceManagementSystemAPI.Repository;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _db;
    private string secretKey;
    public UserRepository(ApplicationDbContext db,IConfiguration configuration)
    {
        _db = db;
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");

    }
    public bool IsUniqueUser(string username)
    {
        var user = _db.Users.FirstOrDefault(x => x.UserName == username);
        if (user == null)
        {
            return true;
        }

        return false;
    }

    public  async Task<LoginResponseDTO> Login(LoginRequestDTO loginRequestDto)
    {
        var user = _db.Users.FirstOrDefault(u =>
            u.UserName == loginRequestDto.UserName && u.Password == loginRequestDto.Password);
        if (user == null)
        {
            return new LoginResponseDTO()
            {
                Token = "",
                User = null
            };
        }

        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor()
        {
            Subject = new ClaimsIdentity(new Claim[]
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddHours(12),
            SigningCredentials = new(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        LoginResponseDTO loginResponseDto = new LoginResponseDTO()
        {
            Token = tokenHandler.WriteToken(token),
            User = user
        };
        return loginResponseDto;
    }

    public async  Task<User> Register(RegistrationRequestDTO registrationRequestDto)
    {
        User user = new User()
        {
            UserName = registrationRequestDto.UserName,
            Password = registrationRequestDto.Password,
            Name = registrationRequestDto.Name,
            Role = registrationRequestDto.Role
        };
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }
}