using Microsoft.EntityFrameworkCore.Migrations;

namespace HRJ.LMS.Persistence.Migrations
{
    public partial class _019_Remarks_FieldAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "LeadStatus",
                table: "LeadECManagerRemarks",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CallingStatus",
                table: "LeadCallerRemarks",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LeadStatus",
                table: "LeadECManagerRemarks");

            migrationBuilder.DropColumn(
                name: "CallingStatus",
                table: "LeadCallerRemarks");
        }
    }
}
