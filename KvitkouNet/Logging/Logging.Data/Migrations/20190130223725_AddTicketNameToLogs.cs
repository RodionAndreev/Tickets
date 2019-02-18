using Microsoft.EntityFrameworkCore.Migrations;

namespace Logging.Data.Migrations
{
    public partial class AddTicketNameToLogs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TicketId",
                table: "TicketDealLogEntries",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TicketName",
                table: "TicketActionLogEntries",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TicketId",
                table: "TicketDealLogEntries");

            migrationBuilder.DropColumn(
                name: "TicketName",
                table: "TicketActionLogEntries");
        }
    }
}
