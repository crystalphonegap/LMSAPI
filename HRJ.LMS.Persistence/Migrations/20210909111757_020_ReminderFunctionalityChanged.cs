using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _020_ReminderFunctionalityChanged : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LeadReminders_LeadReminderOptions_LeadReminderOptionId",
                table: "LeadReminders");

            migrationBuilder.DropIndex(
                name: "IX_LeadReminders_LeadReminderOptionId",
                table: "LeadReminders");

            migrationBuilder.DropColumn(
                name: "LeadReminderOptionId",
                table: "LeadReminders");

            migrationBuilder.DropColumn(
                name: "Reminder",
                table: "LeadReminders");

            migrationBuilder.AddColumn<string>(
                name: "UserRole",
                table: "LeadReminders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserRole",
                table: "LeadReminders");

            migrationBuilder.AddColumn<int>(
                name: "LeadReminderOptionId",
                table: "LeadReminders",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reminder",
                table: "LeadReminders",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_LeadReminders_LeadReminderOptionId",
                table: "LeadReminders",
                column: "LeadReminderOptionId");

            migrationBuilder.AddForeignKey(
                name: "FK_LeadReminders_LeadReminderOptions_LeadReminderOptionId",
                table: "LeadReminders",
                column: "LeadReminderOptionId",
                principalTable: "LeadReminderOptions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
