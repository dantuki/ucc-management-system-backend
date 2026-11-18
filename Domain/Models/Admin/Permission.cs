namespace Domain.Models.Admin;

public sealed class Permission
{
    public string Code { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public ICollection<RolePermission> RolePermissions { get; set; } = [];
}

public sealed class RolePermission
{
    public int IdRol { get; set; }
    public string PermissionCode { get; set; } = string.Empty;
    public Rol Role { get; set; } = null!;
    public Permission Permission { get; set; } = null!;
}

public sealed class UserRole
{
    public int IdUsuario { get; set; }
    public int IdRol { get; set; }
    public Domain.Models.AdminModels.Usuario User { get; set; } = null!;
    public Rol Role { get; set; } = null!;
}
