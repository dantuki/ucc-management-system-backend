using Application.Services.Security;
using Domain.DTOs.AdminDTOs;
using Domain.Security;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers;

[ApiController]
[Route("api/rbac")]
[Authorize(Policy = PermissionCodes.RbacManage)]
public sealed class RbacController(IRbacService rbac) : ControllerBase
{
    [HttpGet("permissions")]
    public async Task<ActionResult<IReadOnlyList<PermissionDto>>> GetPermissions(CancellationToken cancellationToken) =>
        Ok(await rbac.GetPermissionsAsync(cancellationToken));

    [HttpGet("users/{userId:int}/roles")]
    public async Task<ActionResult<IReadOnlyList<int>>> GetUserRoles(int userId, CancellationToken cancellationToken) =>
        Ok(await rbac.GetUserRolesAsync(userId, cancellationToken));

    [HttpPut("users/{userId:int}/roles")]
    public async Task<IActionResult> SetUserRoles(
        int userId,
        RoleAssignmentRequestDto request,
        CancellationToken cancellationToken)
    {
        await rbac.SetUserRolesAsync(userId, request.RoleIds, cancellationToken);
        return NoContent();
    }

    [HttpGet("roles/{roleId:int}/permissions")]
    public async Task<ActionResult<IReadOnlyList<string>>> GetRolePermissions(
        int roleId,
        CancellationToken cancellationToken) =>
        Ok(await rbac.GetRolePermissionsAsync(roleId, cancellationToken));

    [HttpPut("roles/{roleId:int}/permissions")]
    public async Task<IActionResult> SetRolePermissions(
        int roleId,
        RolePermissionsRequestDto request,
        CancellationToken cancellationToken)
    {
        await rbac.SetRolePermissionsAsync(roleId, request.PermissionCodes, cancellationToken);
        return NoContent();
    }
}
