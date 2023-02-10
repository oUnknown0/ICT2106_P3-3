using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class createFileRepo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "File",
                columns: table => new
                {
                    FileId = table.Column<string>(type: "TEXT", nullable: false),
                    FileName = table.Column<string>(type: "TEXT", nullable: true),
                    FileUrl = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_File", x => x.FileId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Expense_ExpenseReceipt",
                table: "Expense",
                column: "ExpenseReceipt");

            migrationBuilder.AddForeignKey(
                name: "FK_Expense_File_ExpenseReceipt",
                table: "Expense",
                column: "ExpenseReceipt",
                principalTable: "File",
                principalColumn: "FileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Expense_File_ExpenseReceipt",
                table: "Expense");

            migrationBuilder.DropTable(
                name: "File");

            migrationBuilder.DropIndex(
                name: "IX_Expense_ExpenseReceipt",
                table: "Expense");
        }
    }
}
