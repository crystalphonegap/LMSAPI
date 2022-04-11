using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _032_InvoiceFileUpload : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadInvoiceFileDetails",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: true),
                    FileName = table.Column<string>(nullable: true),
                    SystemFileName = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    UploadedAt = table.Column<DateTime>(nullable: false),
                    UploadedById = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadInvoiceFileDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadInvoiceFileDetails_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadInvoiceFileDetails_AspNetUsers_UploadedById",
                        column: x => x.UploadedById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadInvoiceFileDetails_LeadId",
                table: "LeadInvoiceFileDetails",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadInvoiceFileDetails_UploadedById",
                table: "LeadInvoiceFileDetails",
                column: "UploadedById");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadInvoiceFileDetails");
        }
    }
}
