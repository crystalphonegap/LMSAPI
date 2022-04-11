using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _004_LeadCallerRemarks : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CallerRemarks",
                table: "Leads");

            migrationBuilder.CreateTable(
                name: "LeadCallerRemark",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    CallerRemark = table.Column<string>(nullable: true),
                    CallerRemarkAt = table.Column<DateTime>(nullable: false),
                    CallerRemarkBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadCallerRemark", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadCallerRemark_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadCallerRemark_LeadId",
                table: "LeadCallerRemark",
                column: "LeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadCallerRemark");

            migrationBuilder.AddColumn<string>(
                name: "CallerRemarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
