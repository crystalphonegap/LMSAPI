using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _009_ExcelTemplate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadContactDetails_Leads_LeadId",
                table: "LeadContactDetails");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeadId",
                table: "LeadContactDetails",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "UploadExcelTemplates",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LeadSource = table.Column<string>(nullable: true),
                    ColumnName = table.Column<string>(nullable: true),
                    ColumnOrder = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UploadExcelTemplates", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_LeadContactDetails_Leads_LeadId",
                table: "LeadContactDetails",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadContactDetails_Leads_LeadId",
                table: "LeadContactDetails");

            migrationBuilder.DropTable(
                name: "UploadExcelTemplates");

            migrationBuilder.AlterColumn<Guid>(
                name: "LeadId",
                table: "LeadContactDetails",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_LeadContactDetails_Leads_LeadId",
                table: "LeadContactDetails",
                column: "LeadId",
                principalTable: "Leads",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
