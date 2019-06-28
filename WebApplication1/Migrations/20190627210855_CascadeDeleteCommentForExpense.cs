using Microsoft.EntityFrameworkCore.Migrations;

namespace Lab2.Migrations
{
    public partial class CascadeDeleteCommentForExpense : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Expenses_ExpenseId",
                table: "Comments",
                column: "ExpenseId",
                principalTable: "Expenses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
