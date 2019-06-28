using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class addDbContextEntries : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRole_Users_UserId",
                table: "UserUserRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRole_UserRole_UserRoleId",
                table: "UserUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUserRole",
                table: "UserUserRole");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole");

            migrationBuilder.RenameTable(
                name: "UserUserRole",
                newName: "UserUserRoles");

            migrationBuilder.RenameTable(
                name: "UserRole",
                newName: "UserRoles");

            migrationBuilder.RenameIndex(
                name: "IX_UserUserRole_UserRoleId",
                table: "UserUserRoles",
                newName: "IX_UserUserRoles_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserUserRole_UserId",
                table: "UserUserRoles",
                newName: "IX_UserUserRoles_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUserRoles",
                table: "UserUserRoles",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRoles_Users_UserId",
                table: "UserUserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRoles_UserRoles_UserRoleId",
                table: "UserUserRoles",
                column: "UserRoleId",
                principalTable: "UserRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRoles_Users_UserId",
                table: "UserUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_UserUserRoles_UserRoles_UserRoleId",
                table: "UserUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserUserRoles",
                table: "UserUserRoles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRoles",
                table: "UserRoles");

            migrationBuilder.RenameTable(
                name: "UserUserRoles",
                newName: "UserUserRole");

            migrationBuilder.RenameTable(
                name: "UserRoles",
                newName: "UserRole");

            migrationBuilder.RenameIndex(
                name: "IX_UserUserRoles_UserRoleId",
                table: "UserUserRole",
                newName: "IX_UserUserRole_UserRoleId");

            migrationBuilder.RenameIndex(
                name: "IX_UserUserRoles_UserId",
                table: "UserUserRole",
                newName: "IX_UserUserRole_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserUserRole",
                table: "UserUserRole",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRole",
                table: "UserRole",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRole_Users_UserId",
                table: "UserUserRole",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserUserRole_UserRole_UserRoleId",
                table: "UserUserRole",
                column: "UserRoleId",
                principalTable: "UserRole",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
