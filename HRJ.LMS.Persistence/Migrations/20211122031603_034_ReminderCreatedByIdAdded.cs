using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _034_ReminderCreatedByIdAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedById",
                table: "LeadReminders",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadReminders_CreatedById",
                table: "LeadReminders",
                column: "CreatedById");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadReminders_AspNetUsers_CreatedById",
                table: "LeadReminders",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadReminders_AspNetUsers_CreatedById",
                table: "LeadReminders");

            migrationBuilder.DropIndex(
                name: "IX_LeadReminders_CreatedById",
                table: "LeadReminders");

            migrationBuilder.DropColumn(
                name: "CreatedById",
                table: "LeadReminders");
        }
    }
}
