using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class Expense : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Expense",
                columns: table => new
                {
                    ExpenseId = table.Column<string>(type: "TEXT", nullable: false),
                    ExpenseAmount = table.Column<double>(type: "REAL", nullable: false),
                    ExpenseDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true),
                    ExpenseReceipt = table.Column<string>(type: "TEXT", nullable: true),
                    Status = table.Column<string>(type: "TEXT", nullable: true),
                    DateOfExpense = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateOfSubmission = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateOfReimbursement = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ApprovalId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Expense", x => x.ExpenseId);
                    table.ForeignKey(
                        name: "FK_Expense_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_Expense_Users_ApprovalId",
                        column: x => x.ApprovalId,
                        principalTable: "Users",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ApprovalId",
                table: "Expense",
                column: "ApprovalId");

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ProjectId",
                table: "Expense",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Expense");
        }
    }
}
