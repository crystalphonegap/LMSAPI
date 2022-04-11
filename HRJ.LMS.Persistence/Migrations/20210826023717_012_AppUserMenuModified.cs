using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _012_AppUserMenuModified : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ECRemarks",
                table: "Leads");

            migrationBuilder.AddColumn<int>(
                name: "RowOrder",
                table: "AppUserMenus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RowOrder",
                table: "AppUserMenus");

            migrationBuilder.AddColumn<string>(
                name: "ECRemarks",
                table: "Leads",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
