using Fitness.Auth.Domain.Entities;
using Fitness.Auth.Shared.Models;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Fitness.Auth.Shared.Services;

public class TokenService(IOptions<JwtOptions> jwtOptions) 
{
    private readonly JwtOptions _options = jwtOptions.Value;

    public string GenerateAccessToken(User user,string serviceName)
    {
        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email ?? string.Empty),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var signingKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_options.SigningKey));
        var credentials = new SigningCredentials(signingKey, SecurityAlgorithms.HmacSha256);

        var token = new JwtSecurityToken(
            issuer: _options.Issuer,
            audience: _options.Audience,
            claims: claims,
            expires: DateTime.UtcNow.AddMinutes(_options.AccessTokenLifetimeMinutes),
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    public (string PlaintextToken, string TokenHash) GenerateRefreshToken()
    {
        var randomBytes = RandomNumberGenerator.GetBytes(64);
        var plaintext = Convert.ToBase64String(randomBytes);
        return (plaintext, HashToken(plaintext));
    }

    public string HashToken(string plaintextToken)
    {
        var bytes = Encoding.UTF8.GetBytes(plaintextToken);
        var hash = SHA256.HashData(bytes);
        return Convert.ToBase64String(hash);
    }
}
