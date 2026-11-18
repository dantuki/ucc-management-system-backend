using Domain.Models.AdminModels;

namespace Application.Services.Security;

public interface IPasswordHashingService
{
    string Hash(Usuario user, string password);
    bool Verify(Usuario user, string password);
}
