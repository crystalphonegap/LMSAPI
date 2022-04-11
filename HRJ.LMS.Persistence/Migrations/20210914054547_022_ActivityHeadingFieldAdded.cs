using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _022_ActivityHeadingFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ActivityHeading",
                table: "LeadActivities",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ActivityHeading",
                table: "LeadActivities");
        }
    }
}
