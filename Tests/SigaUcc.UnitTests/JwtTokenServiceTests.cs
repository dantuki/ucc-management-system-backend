using System.IdentityModel.Tokens.Jwt;
using Domain.DTOs.AdminDTOs;
using WebApi.Security;
using Xunit;

namespace SigaUcc.UnitTests;

public sealed class JwtTokenServiceTests
{
    [Fact]
    public void Create_emits_subject_and_expiration_without_password_data()
    {
        var settings = new JwtSettings
        {
            Issuer = "siga-ucc-test",
            Audience = "siga-ucc-test-api",
            SigningKey = new string('x', 64),
            ExpirationMinutes = 15
        };
        var user = new UsuarioDto { IdUsuario = 42, NombreUsuario = "demo.admin", Habilitado = true };

        var response = new JwtTokenService(settings).Create(user);
        var token = new JwtSecurityTokenHandler().ReadJwtToken(response.AccessToken);

        Assert.Contains(token.Claims, claim => claim.Value == "42");
        Assert.Equal("demo.admin", response.User.NombreUsuario);
        Assert.DoesNotContain("password", response.AccessToken, StringComparison.OrdinalIgnoreCase);
        Assert.InRange(response.ExpiresAtUtc, DateTime.UtcNow.AddMinutes(14), DateTime.UtcNow.AddMinutes(16));
    }
}
