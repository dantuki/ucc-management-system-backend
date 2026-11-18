using Domain.DTOs.AdminDTOs;
using Domain.Models.Admin;
using Domain.Models.AdminModels;
using Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Application.Services.Security;

public sealed class RbacService(
    ApplicationDbContext db,
    IPasswordHashingService passwordHashingService) : IRbacService
{
    private const string AdministratorRoleName = "Administrator";

    public async Task<IReadOnlyList<PermissionDto>> GetPermissionsAsync(CancellationToken cancellationToken) =>
        await db.Permission
            .AsNoTracking()
            .OrderBy(permission => permission.Code)
            .Select(permission => new PermissionDto(permission.Code, permission.Description))
            .ToListAsync(cancellationToken);

    public async Task<IReadOnlyList<int>> GetUserRolesAsync(int userId, CancellationToken cancellationToken)
    {
        if (!await db.Usuario.AnyAsync(user => user.IdUsuario == userId, cancellationToken))
        {
            throw new KeyNotFoundException("User not found.");
        }

        return await db.UserRole
            .Where(userRole => userRole.IdUsuario == userId)
            .OrderBy(userRole => userRole.IdRol)
            .Select(userRole => userRole.IdRol)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<string>> GetRolePermissionsAsync(int roleId, CancellationToken cancellationToken)
    {
        if (!await db.Rol.AnyAsync(role => role.IdRol == roleId, cancellationToken))
        {
            throw new KeyNotFoundException("Role not found.");
        }

        return await db.RolePermission
            .Where(rolePermission => rolePermission.IdRol == roleId)
            .OrderBy(rolePermission => rolePermission.PermissionCode)
            .Select(rolePermission => rolePermission.PermissionCode)
            .ToListAsync(cancellationToken);
    }

    public async Task SetUserRolesAsync(
        int userId,
        IReadOnlyCollection<int> roleIds,
        CancellationToken cancellationToken)
    {
        if (!await db.Usuario.AnyAsync(user => user.IdUsuario == userId, cancellationToken))
        {
            throw new KeyNotFoundException("User not found.");
        }

        var distinctRoleIds = roleIds.Distinct().ToArray();
        var enabledRoleIds = await db.Rol
            .Where(role => distinctRoleIds.Contains(role.IdRol) && role.Habilitado)
            .Select(role => role.IdRol)
            .ToArrayAsync(cancellationToken);

        if (enabledRoleIds.Length != distinctRoleIds.Length)
        {
            throw new ArgumentException("One or more roles are missing or disabled.", nameof(roleIds));
        }

        var current = await db.UserRole.Where(userRole => userRole.IdUsuario == userId).ToListAsync(cancellationToken);
        db.UserRole.RemoveRange(current);
        db.UserRole.AddRange(distinctRoleIds.Select(roleId => new UserRole { IdUsuario = userId, IdRol = roleId }));
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task SetRolePermissionsAsync(
        int roleId,
        IReadOnlyCollection<string> permissionCodes,
        CancellationToken cancellationToken)
    {
        if (!await db.Rol.AnyAsync(role => role.IdRol == roleId, cancellationToken))
        {
            throw new KeyNotFoundException("Role not found.");
        }

        var distinctCodes = permissionCodes.Distinct(StringComparer.Ordinal).ToArray();
        var knownCodes = await db.Permission
            .Where(permission => distinctCodes.Contains(permission.Code))
            .Select(permission => permission.Code)
            .ToArrayAsync(cancellationToken);

        if (knownCodes.Length != distinctCodes.Length)
        {
            throw new ArgumentException("One or more permission codes are unknown.", nameof(permissionCodes));
        }

        var current = await db.RolePermission.Where(rolePermission => rolePermission.IdRol == roleId).ToListAsync(cancellationToken);
        db.RolePermission.RemoveRange(current);
        db.RolePermission.AddRange(distinctCodes.Select(code =>
            new RolePermission { IdRol = roleId, PermissionCode = code }));
        await db.SaveChangesAsync(cancellationToken);
    }

    public async Task<int> BootstrapAdminAsync(
        BootstrapAdminRequestDto request,
        CancellationToken cancellationToken)
    {
        await using var transaction = await db.Database.BeginTransactionAsync(cancellationToken);

        var administratorRole = await db.Rol.SingleOrDefaultAsync(
            role => role.Nombre == AdministratorRoleName,
            cancellationToken);
        if (administratorRole is not null &&
            await db.UserRole.AnyAsync(userRole => userRole.IdRol == administratorRole.IdRol, cancellationToken))
        {
            throw new InvalidOperationException("Administrator bootstrap has already been completed.");
        }

        var username = request.NombreUsuario.Trim();
        var person = request.Persona;
        if (await db.Usuario.AnyAsync(user => user.NombreUsuario == username, cancellationToken) ||
            await db.Persona.AnyAsync(existing =>
                existing.Documento == person.Documento || existing.Correo == person.Correo,
                cancellationToken))
        {
            throw new InvalidOperationException("The username, document, or email is already registered.");
        }

        var availablePermissions = await db.Permission.Select(permission => permission.Code).ToListAsync(cancellationToken);
        if (availablePermissions.Count != Domain.Security.PermissionCodes.All.Count)
        {
            throw new InvalidOperationException("The RBAC permission catalog is incomplete; apply pending migrations.");
        }

        if (administratorRole is null)
        {
            administratorRole = new Rol
            {
                Nombre = AdministratorRoleName,
                Descripcion = "Full administrative access",
                Habilitado = true
            };
            db.Rol.Add(administratorRole);
        }

        var newPerson = new Persona
        {
            Nombres = person.Nombres.Trim(),
            Apellidos = person.Apellidos.Trim(),
            Documento = person.Documento.Trim(),
            Correo = person.Correo.Trim(),
            Telefono = person.Telefono.Trim(),
            IdGenero = person.IdGenero,
            IdEstadoCivil = person.IdEstadoCivil
        };
        var user = new Usuario
        {
            NombreUsuario = username,
            Habilitado = true,
            FechaCreacion = DateTime.UtcNow,
            Persona = newPerson
        };
        user.Contrasena = passwordHashingService.Hash(user, request.Contrasena);
        db.Usuario.Add(user);

        await db.SaveChangesAsync(cancellationToken);

        var assignedPermissions = await db.RolePermission
            .Where(rolePermission => rolePermission.IdRol == administratorRole.IdRol)
            .Select(rolePermission => rolePermission.PermissionCode)
            .ToArrayAsync(cancellationToken);
        db.RolePermission.AddRange(availablePermissions
            .Except(assignedPermissions, StringComparer.Ordinal)
            .Select(code => new RolePermission { IdRol = administratorRole.IdRol, PermissionCode = code }));
        db.UserRole.Add(new UserRole { IdUsuario = user.IdUsuario, IdRol = administratorRole.IdRol });
        await db.SaveChangesAsync(cancellationToken);
        await transaction.CommitAsync(cancellationToken);

        return user.IdUsuario;
    }
}
