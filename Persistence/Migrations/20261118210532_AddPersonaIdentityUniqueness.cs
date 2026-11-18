using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPersonaIdentityUniqueness : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Code = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "varchar(160)", maxLength: 160, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Code);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "UsuarioRol",
                columns: table => new
                {
                    IdUsuario = table.Column<int>(type: "int", nullable: false),
                    IdRol = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRol", x => new { x.IdUsuario, x.IdRol });
                    table.ForeignKey(
                        name: "FK_UsuarioRol_Usuario_IdUsuario",
                        column: x => x.IdUsuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRol_admin.Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "admin.Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RolPermiso",
                columns: table => new
                {
                    IdRol = table.Column<int>(type: "int", nullable: false),
                    PermissionCode = table.Column<string>(type: "varchar(64)", maxLength: 64, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RolPermiso", x => new { x.IdRol, x.PermissionCode });
                    table.ForeignKey(
                        name: "FK_RolPermiso_Permission_PermissionCode",
                        column: x => x.PermissionCode,
                        principalTable: "Permission",
                        principalColumn: "Code",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_RolPermiso_admin.Rol_IdRol",
                        column: x => x.IdRol,
                        principalTable: "admin.Rol",
                        principalColumn: "IdRol",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Permission",
                columns: new[] { "Code", "Description" },
                values: new object[,]
                {
                    { "catalogs.read", "Read reference catalogs" },
                    { "catalogs.write", "Manage reference catalogs" },
                    { "people.read", "Read people" },
                    { "people.write", "Manage people" },
                    { "rbac.manage", "Assign roles and permissions" },
                    { "roles.read", "Read roles" },
                    { "roles.write", "Manage roles" },
                    { "students.read", "Read students" },
                    { "students.write", "Manage students" },
                    { "users.read", "Read users" },
                    { "users.write", "Manage users" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Persona_correo",
                table: "Persona",
                column: "correo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_documento",
                table: "Persona",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RolPermiso_PermissionCode",
                table: "RolPermiso",
                column: "PermissionCode");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRol_IdRol",
                table: "UsuarioRol",
                column: "IdRol");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RolPermiso");

            migrationBuilder.DropTable(
                name: "UsuarioRol");

            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropIndex(
                name: "IX_Persona_correo",
                table: "Persona");

            migrationBuilder.DropIndex(
                name: "IX_Persona_documento",
                table: "Persona");
        }
    }
}
