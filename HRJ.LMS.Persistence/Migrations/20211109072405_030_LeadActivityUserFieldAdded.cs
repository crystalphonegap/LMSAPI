using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _030_LeadActivityUserFieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "AppUserId",
                table: "LeadActivities",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_AppUserId",
                table: "LeadActivities",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadActivities_AspNetUsers_AppUserId",
                table: "LeadActivities",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadActivities_AspNetUsers_AppUserId",
                table: "LeadActivities");

            migrationBuilder.DropIndex(
                name: "IX_LeadActivities_AppUserId",
                table: "LeadActivities");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "LeadActivities");
        }
    }
}
