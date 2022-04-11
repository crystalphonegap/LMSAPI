using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _018_AssignedAtToEC_FieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "AssignedAtToEC",
                table: "Leads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AssignedAtToEC",
                table: "Leads");
        }
    }
}
