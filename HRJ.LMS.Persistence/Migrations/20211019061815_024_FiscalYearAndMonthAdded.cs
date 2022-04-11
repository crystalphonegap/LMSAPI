using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _024_FiscalYearAndMonthAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FiscalYears",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearDuration = table.Column<string>(nullable: true),
                    StartYearDate = table.Column<DateTime>(nullable: false),
                    EndYearDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalYears", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "FiscalMonths",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FiscalYearId = table.Column<int>(nullable: false),
                    FiscalYearDuration = table.Column<string>(nullable: true),
                    FiscalMonthLabel = table.Column<string>(nullable: true),
                    StartMonthDate = table.Column<DateTime>(nullable: false),
                    EndMonthDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FiscalMonths", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FiscalMonths_FiscalYears_FiscalYearId",
                        column: x => x.FiscalYearId,
                        principalTable: "FiscalYears",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_FiscalMonths_FiscalYearId",
                table: "FiscalMonths",
                column: "FiscalYearId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FiscalMonths");

            migrationBuilder.DropTable(
                name: "FiscalYears");
        }
    }
}
