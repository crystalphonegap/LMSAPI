using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _021_LeadActivitiesAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadEventStores");

            migrationBuilder.CreateTable(
                name: "LeadActivities",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<Guid>(nullable: false),
                    ActionTakenBy = table.Column<string>(nullable: true),
                    ActionTakenOn = table.Column<DateTime>(nullable: false),
                    LeadActivityRemarks = table.Column<string>(nullable: true),
                    IsEventToDisplay = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadActivities", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadActivities_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadActivities_LeadId",
                table: "LeadActivities",
                column: "LeadId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LeadActivities");

            migrationBuilder.CreateTable(
                name: "LeadEventStores",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ActionTakenById = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ActionTakenOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    IsEventToDisplay = table.Column<int>(type: "int", nullable: false),
                    LeadId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    LeadStatusId = table.Column<int>(type: "int", nullable: true),
                    Remarks = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeadEventStores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_AspNetUsers_ActionTakenById",
                        column: x => x.ActionTakenById,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_Leads_LeadId",
                        column: x => x.LeadId,
                        principalTable: "Leads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeadEventStores_LeadStatuses_LeadStatusId",
                        column: x => x.LeadStatusId,
                        principalTable: "LeadStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_ActionTakenById",
                table: "LeadEventStores",
                column: "ActionTakenById");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_LeadId",
                table: "LeadEventStores",
                column: "LeadId");

            migrationBuilder.CreateIndex(
                name: "IX_LeadEventStores_LeadStatusId",
                table: "LeadEventStores",
                column: "LeadStatusId");
        }
    }
}
