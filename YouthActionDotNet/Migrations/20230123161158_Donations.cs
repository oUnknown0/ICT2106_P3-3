using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class Donations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Donations",
                columns: table => new
                {
                    DonationsId = table.Column<string>(type: "TEXT", nullable: false),
                    DonationType = table.Column<string>(type: "TEXT", nullable: true),
                    DonationAmount = table.Column<string>(type: "TEXT", nullable: true),
                    DonationContstraint = table.Column<string>(type: "TEXT", nullable: true),
                    DonationDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DonorId = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donations", x => x.DonationsId);
                    table.ForeignKey(
                        name: "FK_Donations_Donor_DonorId",
                        column: x => x.DonorId,
                        principalTable: "Donor",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Donations_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Donations_DonorId",
                table: "Donations",
                column: "DonorId");

            migrationBuilder.CreateIndex(
                name: "IX_Donations_ProjectId",
                table: "Donations",
                column: "ProjectId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Donations");
        }
    }
}
