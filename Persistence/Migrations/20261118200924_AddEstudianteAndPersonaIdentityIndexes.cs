using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddEstudianteAndPersonaIdentityIndexes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Estudiante",
                columns: table => new
                {
                    id_estudiante = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    codigo = table.Column<string>(type: "varchar(30)", maxLength: 30, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    nombres = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    apellidos = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    documento = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fecha_nacimiento = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    correo = table.Column<string>(type: "varchar(150)", maxLength: 150, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    id_tipo_identificacion = table.Column<int>(type: "int", nullable: false),
                    id_genero = table.Column<int>(type: "int", nullable: false),
                    id_estado_civil = table.Column<int>(type: "int", nullable: false),
                    id_usuario = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Estudiante", x => x.id_estudiante);
                    table.ForeignKey(
                        name: "FK_Estudiante_EstadoCivil_id_estado_civil",
                        column: x => x.id_estado_civil,
                        principalTable: "EstadoCivil",
                        principalColumn: "IdEstadoCivil",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estudiante_Genero_id_genero",
                        column: x => x.id_genero,
                        principalTable: "Genero",
                        principalColumn: "IdGenero",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estudiante_TipoIdentificacion_id_tipo_identificacion",
                        column: x => x.id_tipo_identificacion,
                        principalTable: "TipoIdentificacion",
                        principalColumn: "IdTipoIdentificacion",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Estudiante_Usuario_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "Usuario",
                        principalColumn: "id_usuario",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_codigo",
                table: "Estudiante",
                column: "codigo",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_documento",
                table: "Estudiante",
                column: "documento",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_id_estado_civil",
                table: "Estudiante",
                column: "id_estado_civil");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_id_genero",
                table: "Estudiante",
                column: "id_genero");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_id_tipo_identificacion",
                table: "Estudiante",
                column: "id_tipo_identificacion");

            migrationBuilder.CreateIndex(
                name: "IX_Estudiante_id_usuario",
                table: "Estudiante",
                column: "id_usuario",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Estudiante");
        }
    }
}
