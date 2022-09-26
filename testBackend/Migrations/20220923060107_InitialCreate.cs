using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace testBackend.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PermissionType",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Descripcion = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Permission description")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PermissionType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Permission",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NombreEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Employee Forename"),
                    ApellidoEmpleado = table.Column<string>(type: "nvarchar(max)", nullable: false, comment: "Employee Surname"),
                    TipoPermisoId = table.Column<int>(type: "int", nullable: false, comment: "Permission Type"),
                    FechaPermiso = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false, comment: "Permission granted on Date")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permission", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Permission_PermissionType_TipoPermisoId",
                        column: x => x.TipoPermisoId,
                        principalTable: "PermissionType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "PermissionType",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 1, "Admin" });

            migrationBuilder.InsertData(
                table: "PermissionType",
                columns: new[] { "Id", "Descripcion" },
                values: new object[] { 2, "User" });

            migrationBuilder.CreateIndex(
                name: "IX_Permission_TipoPermisoId",
                table: "Permission",
                column: "TipoPermisoId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permission");

            migrationBuilder.DropTable(
                name: "PermissionType");
        }
    }
}
