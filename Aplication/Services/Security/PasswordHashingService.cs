using Domain.Models.AdminModels;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Security;

public sealed class PasswordHashingService : IPasswordHashingService
{
    private readonly PasswordHasher<Usuario> _hasher = new();

    public string Hash(Usuario user, string password) => _hasher.HashPassword(user, password);

    public bool Verify(Usuario user, string password)
    {
        try
        {
            return _hasher.VerifyHashedPassword(user, user.Contrasena, password) is
                PasswordVerificationResult.Success or PasswordVerificationResult.SuccessRehashNeeded;
        }
        catch (FormatException)
        {
            return false;
        }
    }
}
