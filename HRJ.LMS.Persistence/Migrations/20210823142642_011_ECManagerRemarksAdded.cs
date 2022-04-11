using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _011_ECManagerRemarksAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadCallerRemark_Leads_LeadId",
                table: "LeadCallerRemark");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadCallerRemark",
                table: "LeadCallerRemark");

            migrationBuilder.RenameTable(
                name: "LeadCallerRemark",
                newName: "LeadCallerRemarks");

            migrationBuilder.RenameIndex(
                name: "IX_LeadCallerRemark_LeadId",
                table: "LeadCallerRemarks",
                newName: "IX_LeadCallerRemarks_LeadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadCallerRemarks",
                table: "LeadCallerRemarks",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "LeadECManagerRemarks",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    ECManagerRemark = table.Column<string>(nullable: true),
                    ECManagerRemarkAt = table.Column<DateTime>(nullable: false),
                    ECManagerRemarkBy = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadECManagerRemarks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadECManagerRemarks_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadECManagerRemarks_LeadId",
                table: "LeadECManagerRemarks",
                column: "LeadId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadCallerRemarks_Leads_LeadId",
                table: "LeadCallerRemarks",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadCallerRemarks_Leads_LeadId",
                table: "LeadCallerRemarks");

            migrationBuilder.DropTable(
                name: "LeadECManagerRemarks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_LeadCallerRemarks",
                table: "LeadCallerRemarks");

            migrationBuilder.RenameTable(
                name: "LeadCallerRemarks",
                newName: "LeadCallerRemark");

            migrationBuilder.RenameIndex(
                name: "IX_LeadCallerRemarks_LeadId",
                table: "LeadCallerRemark",
                newName: "IX_LeadCallerRemark_LeadId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_LeadCallerRemark",
                table: "LeadCallerRemark",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadCallerRemark_Leads_LeadId",
                table: "LeadCallerRemark",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
