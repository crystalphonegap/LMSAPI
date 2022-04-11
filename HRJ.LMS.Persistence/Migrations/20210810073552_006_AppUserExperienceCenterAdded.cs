using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _006_AppUserExperienceCenterAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AppUserExperienceCenters",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<string>(nullable: true),
                    ExperienceCenterId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserExperienceCenters", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserExperienceCenters_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AppUserExperienceCenters_ExperienceCenters_ExperienceCenterId",
                        column: x => x.ExperienceCenterId,
                        principalTable: "ExperienceCenters",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppUserExperienceCenters_AppUserId",
                table: "AppUserExperienceCenters",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserExperienceCenters_ExperienceCenterId",
                table: "AppUserExperienceCenters",
                column: "ExperienceCenterId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppUserExperienceCenters");
        }
    }
}
