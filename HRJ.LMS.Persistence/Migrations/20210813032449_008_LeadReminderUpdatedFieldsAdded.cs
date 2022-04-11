using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _008_LeadReminderUpdatedFieldsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "ReminderUpdatedAt",
                table: "LeadReminders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReminderUpdatedBy",
                table: "LeadReminders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReminderUpdatedAt",
                table: "LeadReminders");

            migrationBuilder.DropColumn(
                name: "ReminderUpdatedBy",
                table: "LeadReminders");
        }
    }
}
