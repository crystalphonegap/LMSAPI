using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _016_JustDialAndStateMapping : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JustDialLeads",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    LeadId = table.Column<string>(nullable: true),
                    LeadType = table.Column<string>(nullable: true),
                    Prefix = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Mobile = table.Column<string>(nullable: true),
                    Phone = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Category = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    Area = table.Column<string>(nullable: true),
                    BranchArea = table.Column<string>(nullable: true),
                    DNCMobile = table.Column<int>(nullable: false),
                    DNCPhone = table.Column<int>(nullable: false),
                    Company = table.Column<string>(nullable: true),
                    Pincode = table.Column<string>(nullable: true),
                    Time = table.Column<string>(nullable: true),
                    BranchPin = table.Column<string>(nullable: true),
                    ParentId = table.Column<string>(nullable: true),
                    LeadCreatedAt = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JustDialLeads", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "StateCityMappings",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StateName = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StateCityMappings", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JustDialLeads");

            migrationBuilder.DropTable(
                name: "StateCityMappings");
        }
    }
}
