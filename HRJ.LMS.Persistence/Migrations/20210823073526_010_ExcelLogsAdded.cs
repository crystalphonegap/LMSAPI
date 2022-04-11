using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _010_ExcelLogsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UploadExcelLogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ExcelFileName = table.Column<string>(nullable: true),
                    UploadedRemarks = table.Column<string>(nullable: true),
                    UploadedById = table.Column<string>(nullable: true),
                    UploadedByName = table.Column<string>(nullable: true),
                    UploadedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadExcelLogs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UploadExcelLogs_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UploadExcelLogs_UploadedById",
                table: "UploadExcelLogs",
                column: "UploadedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UploadExcelLogs");
        }
    }
}
