using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using EducationBySubscription.Application.Providers.Authentication;
using EducationBySubscription.Infrastructure.Providers.Authentication.Options;
using EducationSubscription.Core.Domain.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace EducationBySubscription.Infrastructure.Providers.Authentication;

public class JwtProvider : IJwtProvider
{
    private readonly JwtOptions _jwtOptions;

    public JwtProvider(IOptions<JwtOptions> jwtOptions)
    {
        _jwtOptions = jwtOptions.Value;
    }

    /// <inheritdoc />
    public string GenerateAuthenticationToken(Guid userId, EUserRole userRole)
    {
        const int minutesToExpire = 480;
        var handler = new JwtSecurityTokenHandler();
        return handler.WriteToken(new JwtSecurityToken(GenerateJwtHeader(), GenerateJwtPayload(new List<Claim>()
        {
            new(JwtRegisteredClaimNames.Sub, userId.ToString()),
            new("role", userRole.ToString())
        }, minutesToExpire)));
    }

    /// <summary>
    /// Generates a JWT token payload.
    /// </summary>
    /// <param name="claims"></param>
    /// <param name="minutesToExpire"></param>
    /// <returns></returns>
    private JwtPayload GenerateJwtPayload(List<Claim> claims, int minutesToExpire)
    {
        var startExpires = DateTime.Now;
        var expiresDate = startExpires.AddMinutes(minutesToExpire);
        return new JwtPayload(_jwtOptions.Issuer, _jwtOptions.Audience, claims, startExpires, expiresDate);
    }
    
    /// <summary>
    /// Generates a JWT token header.
    /// </summary>
    /// <returns></returns>
    private JwtHeader GenerateJwtHeader()
    {
        var wrappedPrivateKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.PrivateKey));
        return new JwtHeader(new SigningCredentials(wrappedPrivateKey, SecurityAlgorithms.HmacSha512));
    }

    /// <inheritdoc/>
    public string Encrypt(string str)
    {
        var crypt = SHA512.Create();
        var encryptedBytes = crypt.ComputeHash(Encoding.UTF8.GetBytes(str));
        var builder = new StringBuilder();
        foreach(var byteChunk in encryptedBytes)
        {
            builder.Append($"{byteChunk:X2}");
        }
        return builder.ToString();
    }
}