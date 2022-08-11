using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataLayer.Migrations
{
    public partial class UserReportTableAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserReports",
                columns: table => new
                {
                    UserReportID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserReporterID = table.Column<int>(type: "int", nullable: false),
                    ReportAgainstUserID = table.Column<int>(type: "int", nullable: false),
                    ReportTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReportDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserReports", x => x.UserReportID);
                    table.ForeignKey(
                        name: "FK_UserReports_Users_ReportAgainstUserID",
                        column: x => x.ReportAgainstUserID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                    table.ForeignKey(
                        name: "FK_UserReports_Users_UserReporterID",
                        column: x => x.UserReporterID,
                        principalTable: "Users",
                        principalColumn: "UserID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_ReportAgainstUserID",
                table: "UserReports",
                column: "ReportAgainstUserID");

            migrationBuilder.CreateIndex(
                name: "IX_UserReports_UserReporterID",
                table: "UserReports",
                column: "UserReporterID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserReports");
        }
    }
}
