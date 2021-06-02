using Microsoft.EntityFrameworkCore.Migrations;

namespace Persistence.EF.Migrations
{
    public partial class RemovedCreteUpdateUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Participants");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Meetings");

            migrationBuilder.DropColumn(
                name: "LastModifiedBy",
                table: "Meetings");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Participants",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CreatedBy",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastModifiedBy",
                table: "Meetings",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
