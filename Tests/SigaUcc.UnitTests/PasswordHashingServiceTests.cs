using Application.Services.Security;
using Domain.DTOs.AdminDTOs;
using Domain.Models.AdminModels;
using Xunit;

namespace SigaUcc.UnitTests;

public sealed class PasswordHashingServiceTests
{
    [Fact]
    public void Hash_creates_a_non_plaintext_value_that_verifies()
    {
        var service = new PasswordHashingService();
        var user = new Usuario { NombreUsuario = "test" };
        const string password = "CorrectHorseBatteryStaple1";

        user.Contrasena = service.Hash(user, password);

        Assert.NotEqual(password, user.Contrasena);
        Assert.True(service.Verify(user, password));
        Assert.False(service.Verify(user, "incorrect-password"));
    }

    [Fact]
    public void Public_user_dto_does_not_expose_a_password_or_hash()
    {
        Assert.Null(typeof(UsuarioDto).GetProperty("Contrasena"));
    }
}
