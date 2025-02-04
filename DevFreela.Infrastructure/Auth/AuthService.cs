using System.Security.Cryptography;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace DevFreela.Infrastructure.Auth;

public class AuthService : IAuthService
{
    public AuthService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    private readonly IConfiguration _configuration;
        
    //armazenamento da senha de forma segura
    public string ComputeHash(string password)
    {
        using (var hash = SHA256.Create())
        {
            var passwordBytes = Encoding.UTF8.GetBytes(password);
                
            var hashBytes = hash.ComputeHash(passwordBytes);
                
            var builder =  new StringBuilder();

            foreach (var bit in hashBytes)
            {
                builder.Append(bit.ToString("X2"));
            }
                
            return builder.ToString();
        }
    }

    public string GenerateToken(string email, string role)
    {
        var issuer = _configuration["Jwt:Issuer"];
        var audience = _configuration["Jwt:audience"];
        var key = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? string.Empty)
        );
            
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new List<Claim>
        {
            new Claim("username", email),
            new Claim(ClaimTypes.Role, role),
        };
            
        var token = new JwtSecurityToken(issuer, audience, claims, null, DateTime.Now.AddHours(2), credentials);
            
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}