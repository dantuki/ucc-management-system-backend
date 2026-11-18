using System.ComponentModel.DataAnnotations;
using Domain.Dtos.AdminDtos;

namespace Domain.DTOs.AdminDTOs;

public sealed record PermissionDto(string Code, string Description);

public sealed record RoleAssignmentRequestDto
{
    [Required]
    public required int[] RoleIds { get; init; }
}

public sealed record RolePermissionsRequestDto
{
    [Required]
    public required string[] PermissionCodes { get; init; }
}

public sealed record BootstrapAdminRequestDto
{
    [Required]
    public required PersonaDto Persona { get; init; }

    [Required, StringLength(100, MinimumLength = 3)]
    public required string NombreUsuario { get; init; }

    [Required, StringLength(128, MinimumLength = 12)]
    public required string Contrasena { get; init; }
}
