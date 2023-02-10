using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class restructurev1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    username = table.Column<string>(type: "TEXT", nullable: true),
                    Email = table.Column<string>(type: "TEXT", nullable: true),
                    Password = table.Column<string>(type: "TEXT", nullable: true),
                    Role = table.Column<string>(type: "TEXT", nullable: true),
                    phoneNumber = table.Column<string>(type: "TEXT", nullable: true),
                    address = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    EmployeeNationalId = table.Column<string>(type: "TEXT", nullable: true),
                    BankName = table.Column<string>(type: "TEXT", nullable: true),
                    BankAccountNumber = table.Column<string>(type: "TEXT", nullable: true),
                    PAYE = table.Column<string>(type: "TEXT", nullable: true),
                    DateJoined = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeeType = table.Column<string>(type: "TEXT", nullable: true),
                    EmployeeRole = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Employee_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Volunteer",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "TEXT", nullable: false),
                    VolunteerNationalId = table.Column<string>(type: "TEXT", nullable: true),
                    VolunteerDateJoined = table.Column<string>(type: "TEXT", nullable: true),
                    VolunteerDateBirth = table.Column<string>(type: "TEXT", nullable: true),
                    Qualifications = table.Column<string>(type: "TEXT", nullable: true),
                    CriminalHistory = table.Column<string>(type: "TEXT", nullable: true),
                    CriminalHistoryDesc = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovalStatus = table.Column<string>(type: "TEXT", nullable: true),
                    ApprovedBy = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Volunteer", x => x.UserId);
                    table.ForeignKey(
                        name: "FK_Volunteer_Users_ApprovedBy",
                        column: x => x.ApprovedBy,
                        principalTable: "Users",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_Volunteer_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceCenter",
                columns: table => new
                {
                    id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ServiceCenterName = table.Column<string>(type: "TEXT", nullable: true),
                    ServiceCenterAddress = table.Column<string>(type: "TEXT", nullable: true),
                    RegionalDirectorId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceCenter", x => x.id);
                    table.ForeignKey(
                        name: "FK_ServiceCenter_Employee_RegionalDirectorId",
                        column: x => x.RegionalDirectorId,
                        principalTable: "Employee",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceCenter_RegionalDirectorId",
                table: "ServiceCenter",
                column: "RegionalDirectorId");

            migrationBuilder.CreateIndex(
                name: "IX_Volunteer_ApprovedBy",
                table: "Volunteer",
                column: "ApprovedBy");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceCenter");

            migrationBuilder.DropTable(
                name: "Volunteer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
