using Domain.DTOs.AdminDTOs;

namespace Application.Services.Security;

public interface IRbacService
{
    Task<IReadOnlyList<PermissionDto>> GetPermissionsAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<int>> GetUserRolesAsync(int userId, CancellationToken cancellationToken);
    Task<IReadOnlyList<string>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken);
    Task SetUserRolesAsync(int userId, IReadOnlyCollection<int> roleIds, CancellationToken cancellationToken);
    Task SetRolePermissionsAsync(int roleId, IReadOnlyCollection<string> permissionCodes, CancellationToken cancellationToken);
    Task<int> BootstrapAdminAsync(BootstrapAdminRequestDto request, CancellationToken cancellationToken);
}
