using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _003_ExperienceCenterModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExpericenceCenterName",
                table: "ExperienceCenters");

            migrationBuilder.AddColumn<string>(
                name: "ExperienceCenterName",
                table: "ExperienceCenters",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExperienceCenterName",
                table: "ExperienceCenters");

            migrationBuilder.AddColumn<string>(
                name: "ExpericenceCenterName",
                table: "ExperienceCenters",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
