using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _015_LeadModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeInSqureFeet",
                table: "Leads");

            migrationBuilder.AddColumn<int>(
                name: "VolumeInSquareFeet",
                table: "Leads",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VolumeInSquareFeet",
                table: "Leads");

            migrationBuilder.AddColumn<int>(
                name: "VolumeInSqureFeet",
                table: "Leads",
                type: "int",
                nullable: true);
        }
    }
}
