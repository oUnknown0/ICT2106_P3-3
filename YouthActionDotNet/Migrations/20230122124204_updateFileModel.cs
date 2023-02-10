using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class updateFileModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "FileMIME",
                table: "File",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileMIME",
                table: "File");
        }
    }
}
