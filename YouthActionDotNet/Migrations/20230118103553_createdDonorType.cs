using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class createdDonorType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RegionalDirectorName",
                table: "ServiceCenter",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Donor",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    donorName = table.Column<string>(type: "TEXT", nullable: true),
                    donorType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donor", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Donor_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donor");

            migrationBuilder.DropColumn(
                name: "RegionalDirectorName",
                table: "ServiceCenter");
        }
    }
}
