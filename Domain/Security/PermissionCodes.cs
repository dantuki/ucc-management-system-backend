namespace Domain.Security;

public static class PermissionCodes
{
    public const string CatalogsRead = "catalogs.read";
    public const string CatalogsWrite = "catalogs.write";
    public const string PeopleRead = "people.read";
    public const string PeopleWrite = "people.write";
    public const string UsersRead = "users.read";
    public const string UsersWrite = "users.write";
    public const string StudentsRead = "students.read";
    public const string StudentsWrite = "students.write";
    public const string RolesRead = "roles.read";
    public const string RolesWrite = "roles.write";
    public const string RbacManage = "rbac.manage";

    public static readonly IReadOnlyDictionary<string, string> All =
        new Dictionary<string, string>
        {
            [CatalogsRead] = "Read reference catalogs",
            [CatalogsWrite] = "Manage reference catalogs",
            [PeopleRead] = "Read people",
            [PeopleWrite] = "Manage people",
            [UsersRead] = "Read users",
            [UsersWrite] = "Manage users",
            [StudentsRead] = "Read students",
            [StudentsWrite] = "Manage students",
            [RolesRead] = "Read roles",
            [RolesWrite] = "Manage roles",
            [RbacManage] = "Assign roles and permissions"
        };
}
