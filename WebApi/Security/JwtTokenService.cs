using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Domain.DTOs.AdminDTOs;
using Microsoft.IdentityModel.Tokens;

namespace WebApi.Security;

public sealed class JwtTokenService(JwtSettings settings)
{
    public AuthTokenResponse Create(UsuarioDto user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(settings.ExpirationMinutes);
        var credentials = new SigningCredentials(new SymmetricSecurityKey(Encoding.UTF8.GetBytes(settings.SigningKey)), SecurityAlgorithms.HmacSha256);
        var token = new JwtSecurityToken(settings.Issuer, settings.Audience,
            [new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString()), new Claim(ClaimTypes.Name, user.NombreUsuario)],
            expires: expiresAt, signingCredentials: credentials);
        return new AuthTokenResponse(new JwtSecurityTokenHandler().WriteToken(token), expiresAt, user);
    }
}

public sealed record AuthTokenResponse(string AccessToken, DateTime ExpiresAtUtc, UsuarioDto User);
