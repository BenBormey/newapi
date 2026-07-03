using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using JuJuBis.Application.Abstractions.Auth;
using JuJuBis.Domain.Entities;

namespace JuJuBis.Infrastructure.Auth;

public sealed class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IConfiguration _config;

    public JwtTokenGenerator(IConfiguration config)
    {
        _config = config;
    }

    public string Generate(User user, IEnumerable<string> permissions)
    {
        var jwt = _config.GetSection("Jwt");

        var claims = new List<Claim>
    {
        new(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
        new(ClaimTypes.Name, user.Username),
        new(ClaimTypes.Role, user.RoleCode),
        new(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
    };

        if (user.RoleCode.Equals("admin", StringComparison.OrdinalIgnoreCase))
        {
            claims.Add(new Claim("permission", "*"));   // wildcard = គ្រប់យ៉ាង
        }
        else
        {
            foreach (var permission in permissions)
                claims.Add(new Claim("permission", permission));
        }

        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwt["Key"]!));
        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: jwt["Issuer"], audience: jwt["Audience"],
            claims: claims, expires: DateTime.UtcNow.AddHours(8),
            signingCredentials: creds);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}
