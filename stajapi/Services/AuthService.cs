using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using stajapi.Entities;
using stajapi.Helpers;
using stajapi.Login;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace stajapi.Services;
public class AuthService
{
    private readonly IConfiguration _configuration;
    private readonly DataContext _context;

    public AuthService(IConfiguration configuration, DataContext context)
    {
        _configuration = configuration;
        _context = context;
    }

    public string GenerateJwtToken(string kullaniciAdi)
    {
        var jwtSettings = _configuration.GetSection("Jwt");
        var claims = new[]
        {
            new Claim(ClaimTypes.Name, kullaniciAdi)
        };

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["Key"]));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwtSettings["Issuer"],
            audience: jwtSettings["Audience"],
            claims: claims,
            notBefore: DateTime.Now,
            expires: DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"])),
            signingCredentials: creds);

        CurrentToken.Token =new JwtSecurityTokenHandler().WriteToken(token);
        CurrentToken.tokenExpireDate = DateTime.Now.AddMinutes(Convert.ToDouble(jwtSettings["ExpiryMinutes"]));
        return CurrentToken.Token;
    }

    public bool ValidateUser(LoginDto request)
    {
        if (string.IsNullOrEmpty(request.Username) || string.IsNullOrEmpty(request.Password))
        {
            throw new ArgumentNullException(nameof(request));
        }
        var kullanici = _context.Kullanici
                    .FromSqlRaw("SELECT * FROM kullanici WHERE ad = {0} AND parola = crypt({1}, parola)", request.Username, request.Password)
                    .FirstOrDefault();

        return kullanici != null;
    }
}
