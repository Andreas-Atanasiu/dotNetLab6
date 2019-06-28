using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class addOwnerForExpenseAndComm : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Expenses",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "OwnerId",
                table: "Comments",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Expenses_OwnerId",
                table: "Expenses",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Users_OwnerId",
                table: "Comments",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Expenses_Users_OwnerId",
                table: "Expenses",
                column: "OwnerId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Users_OwnerId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Expenses_Users_OwnerId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Expenses_OwnerId",
                table: "Expenses");

            migrationBuilder.DropIndex(
                name: "IX_Comments_OwnerId",
                table: "Comments");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Expenses");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Comments");
        }
    }
}
