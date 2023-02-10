using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace YouthActionDotNet.Migrations
{
    /// <inheritdoc />
    public partial class createdVolunteerWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCenter",
                table: "ServiceCenter");

            migrationBuilder.DropColumn(
                name: "id",
                table: "ServiceCenter");

            migrationBuilder.AddColumn<string>(
                name: "ServiceCenterId",
                table: "ServiceCenter",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCenter",
                table: "ServiceCenter",
                column: "ServiceCenterId");

            migrationBuilder.CreateTable(
                name: "Project",
                columns: table => new
                {
                    ProjectId = table.Column<string>(type: "TEXT", nullable: false),
                    ProjectName = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectDescription = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectStartDate = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectEndDate = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectCompletionDate = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectStatus = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectType = table.Column<string>(type: "TEXT", nullable: true),
                    ProjectBudget = table.Column<double>(type: "REAL", nullable: false),
                    ServiceCenterId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.ProjectId);
                    table.ForeignKey(
                        name: "FK_Project_ServiceCenter_ServiceCenterId",
                        column: x => x.ServiceCenterId,
                        principalTable: "ServiceCenter",
                        principalColumn: "ServiceCenterId");
                });

            migrationBuilder.CreateTable(
                name: "VolunteerWork",
                columns: table => new
                {
                    VolunteerWorkId = table.Column<string>(type: "TEXT", nullable: false),
                    ShiftStart = table.Column<string>(type: "TEXT", nullable: true),
                    ShiftEnd = table.Column<string>(type: "TEXT", nullable: true),
                    SupervisingEmployee = table.Column<string>(type: "TEXT", nullable: true),
                    VolunteerId = table.Column<string>(type: "TEXT", nullable: true),
                    projectId = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VolunteerWork", x => x.VolunteerWorkId);
                    table.ForeignKey(
                        name: "FK_VolunteerWork_Employee_SupervisingEmployee",
                        column: x => x.SupervisingEmployee,
                        principalTable: "Employee",
                        principalColumn: "UserId");
                    table.ForeignKey(
                        name: "FK_VolunteerWork_Project_projectId",
                        column: x => x.projectId,
                        principalTable: "Project",
                        principalColumn: "ProjectId");
                    table.ForeignKey(
                        name: "FK_VolunteerWork_Volunteer_VolunteerId",
                        column: x => x.VolunteerId,
                        principalTable: "Volunteer",
                        principalColumn: "UserId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Project_ServiceCenterId",
                table: "Project",
                column: "ServiceCenterId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerWork_projectId",
                table: "VolunteerWork",
                column: "projectId");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerWork_SupervisingEmployee",
                table: "VolunteerWork",
                column: "SupervisingEmployee");

            migrationBuilder.CreateIndex(
                name: "IX_VolunteerWork_VolunteerId",
                table: "VolunteerWork",
                column: "VolunteerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "VolunteerWork");

            migrationBuilder.DropTable(
                name: "Project");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceCenter",
                table: "ServiceCenter");

            migrationBuilder.DropColumn(
                name: "ServiceCenterId",
                table: "ServiceCenter");

            migrationBuilder.AddColumn<int>(
                name: "id",
                table: "ServiceCenter",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0)
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceCenter",
                table: "ServiceCenter",
                column: "id");
        }
    }
}
