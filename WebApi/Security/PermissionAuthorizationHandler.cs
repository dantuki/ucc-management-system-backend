using System.Security.Claims;
using Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Persistence.Context;

namespace WebApi.Security;

public sealed class PermissionRequirement(string permissionCode) : IAuthorizationRequirement
{
    public string PermissionCode { get; } = permissionCode;
}

public sealed class PermissionAuthorizationHandler(ApplicationDbContext db)
    : AuthorizationHandler<PermissionRequirement>
{
    protected override async Task HandleRequirementAsync(
        AuthorizationHandlerContext context,
        PermissionRequirement requirement)
    {
        var userIdValue = context.User.FindFirstValue(ClaimTypes.NameIdentifier);
        if (!int.TryParse(userIdValue, out var userId))
        {
            return;
        }

        var hasPermission = await db.Usuario.AnyAsync(user =>
            user.IdUsuario == userId &&
            user.Habilitado &&
            user.UserRoles.Any(userRole =>
                userRole.Role.Habilitado &&
                userRole.Role.RolePermissions.Any(rolePermission =>
                    rolePermission.PermissionCode == requirement.PermissionCode)));

        if (hasPermission)
        {
            context.Succeed(requirement);
        }
    }
}
