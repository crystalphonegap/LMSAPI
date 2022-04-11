using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _007_LeadReminderAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LeadReminders",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: true),
                    LeadReminderOptionId = table.Column<int>(nullable: true),
                    Reminder = table.Column<string>(nullable: true),
                    RemindAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<int>(nullable: false),
                    ReminderCreatedBy = table.Column<string>(nullable: true),
                    ReminderCreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadReminders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadReminders_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadReminders_LeadReminderOptions_LeadReminderOptionId",
                        column: x => x.LeadReminderOptionId,
                        principalTable: "LeadReminderOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadReminders_LeadId",
                table: "LeadReminders",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadReminders_LeadReminderOptionId",
                table: "LeadReminders",
                column: "LeadReminderOptionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadReminders");
        }
    }
}
