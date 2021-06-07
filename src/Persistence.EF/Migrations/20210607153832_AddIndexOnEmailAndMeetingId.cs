using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.EF.Migrations
{
    public partial class AddIndexOnEmailAndMeetingId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Participants_Email_MeetingId",
                table: "Participants",
                columns: new[] { "Email", "MeetingId" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Participants_Email_MeetingId",
                table: "Participants");
        }
    }
}
