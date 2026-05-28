using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using InvoiceManagementSystemAPI.Data;
using InvoiceManagementSystemAPI.Models;
using InvoiceManagementSystemAPI.Models.Dto;
using InvoiceManagementSystemAPI.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace InvoiceManagementSystemAPI.Repository;

public class UserRepository:IUserRepository
{
    private readonly ApplicationDbContext _db;
    private readonly PasswordHasher<User> _passwordHasher;
    private string secretKey;
    private string issuer;
    private string audience;
    public UserRepository(ApplicationDbContext db,IConfiguration configuration)
    {
        _db = db;
        _passwordHasher = new PasswordHasher<User>();
        secretKey = configuration.GetValue<string>("ApiSettings:Secret");
        issuer = configuration.GetValue<string>("ApiSettings:Issuer");
        audience = configuration.GetValue<string>("ApiSettings:Audience");
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

    public  async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO loginRequestDto)
    {
        try
        {
            var user =  await _db.Users.FirstOrDefaultAsync(u => u.UserName == loginRequestDto.UserName);
            if (user == null) return FailedLogin();
            var result = _passwordHasher.VerifyHashedPassword(user, user.Password, loginRequestDto.Password);
            if (result == PasswordVerificationResult.Failed) return FailedLogin();

            var accessToken = GenerateAccessToken(user);
            var refreshToken = GenerateRefreshToken();

            user.RefreshToken = refreshToken;
            user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
            await _db.SaveChangesAsync();

            return new LoginResponseDTO
            {
                Token = accessToken,
                RefreshToken = refreshToken,
                User = user
            };
        }
        catch (Exception e)
        {
            Console.WriteLine($"Errror verifying password: {e.Message}");
            return FailedLogin();
        }
    }

    public async  Task<User> RegisterAsync(RegistrationRequestDTO registrationRequestDto)
    {
        User user = new User()
        {
            UserName = registrationRequestDto.UserName,
            Name = registrationRequestDto.Name,
            Role = registrationRequestDto.Role
        };
        user.Password = _passwordHasher.HashPassword(user, registrationRequestDto.Password);
        _db.Users.Add(user);
        await _db.SaveChangesAsync();
        user.Password = "";
        return user;
    }

    public async Task<LoginResponseDTO> RefreshAsync(RefreshRequestDTO refreshRequestDto)
    {
        var principal = GetPrincipalFromExpiredToken(refreshRequestDto.AccessToken);
        if (principal == null) return FailedLogin();

        var userIdClaim = principal.FindFirst(ClaimTypes.Name)?.Value;
        if (!int.TryParse(userIdClaim, out int userId)) return FailedLogin();

        var user = await _db.Users.FindAsync(userId);
        if (user == null
            || user.RefreshToken != refreshRequestDto.RefreshToken
            || user.RefreshTokenExpiry <= DateTime.UtcNow)
            return FailedLogin();

        var newAccessToken = GenerateAccessToken(user);
        var newRefreshToken = GenerateRefreshToken();

        user.RefreshToken = newRefreshToken;
        user.RefreshTokenExpiry = DateTime.UtcNow.AddDays(7);
        await _db.SaveChangesAsync();

        return new LoginResponseDTO
        {
            Token = newAccessToken,
            RefreshToken = newRefreshToken,
            User = user
        };
    }

    private string GenerateAccessToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.UTF8.GetBytes(secretKey);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.UserId.ToString()),
                new Claim(ClaimTypes.Role, user.Role)
            }),
            Expires = DateTime.UtcNow.AddMinutes(15),
            Issuer = issuer,
            Audience = audience,
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        return tokenHandler.WriteToken(tokenHandler.CreateToken(tokenDescriptor));
    }

    private static string GenerateRefreshToken()
    {
        var bytes = RandomNumberGenerator.GetBytes(64);
        return Convert.ToBase64String(bytes);
    }

    private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
    {
        var validationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ValidateIssuer = true,
            ValidIssuer = issuer,
            ValidateAudience = true,
            ValidAudience = audience,
            ValidateLifetime = false  // allow expired tokens , we only need the claims
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var principal = tokenHandler.ValidateToken(token, validationParameters, out _);
        return principal;
    }

    private LoginResponseDTO FailedLogin()
    {
        return new LoginResponseDTO
        {
            Token = "",
            User = null
        };
    }
}