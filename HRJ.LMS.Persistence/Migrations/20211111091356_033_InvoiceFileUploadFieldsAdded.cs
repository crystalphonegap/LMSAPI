using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _033_InvoiceFileUploadFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "RemovedAt",
                table: "LeadInvoiceFileDetails",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "RemovedById",
                table: "LeadInvoiceFileDetails",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "UploadedByName",
                table: "LeadInvoiceFileDetails",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadInvoiceFileDetails_RemovedById",
                table: "LeadInvoiceFileDetails",
                column: "RemovedById");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadInvoiceFileDetails_AspNetUsers_RemovedById",
                table: "LeadInvoiceFileDetails",
                column: "RemovedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadInvoiceFileDetails_AspNetUsers_RemovedById",
                table: "LeadInvoiceFileDetails");

            migrationBuilder.DropIndex(
                name: "IX_LeadInvoiceFileDetails_RemovedById",
                table: "LeadInvoiceFileDetails");

            migrationBuilder.DropColumn(
                name: "RemovedAt",
                table: "LeadInvoiceFileDetails");

            migrationBuilder.DropColumn(
                name: "RemovedById",
                table: "LeadInvoiceFileDetails");

            migrationBuilder.DropColumn(
                name: "UploadedByName",
                table: "LeadInvoiceFileDetails");
        }
    }
}
