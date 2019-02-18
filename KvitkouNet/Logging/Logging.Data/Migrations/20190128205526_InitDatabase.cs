using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Logging.Data.Migrations
{
    public partial class InitDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccountLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    UserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccountLogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InternalErrorLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    ExceptionType = table.Column<string>(nullable: true),
                    HResult = table.Column<int>(nullable: false),
                    InnerExceptionString = table.Column<string>(nullable: true),
                    Message = table.Column<string>(nullable: true),
                    Source = table.Column<string>(nullable: true),
                    StackTrace = table.Column<string>(nullable: true),
                    TargetSiteName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InternalErrorLogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PaymentLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    SenderId = table.Column<string>(nullable: true),
                    RecieverId = table.Column<string>(nullable: true),
                    Transfer = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PaymentLogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "SearchQueryLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    SearchCriterium = table.Column<string>(nullable: true),
                    FilterInfo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SearchQueryLogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketActionLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    TicketId = table.Column<string>(nullable: true),
                    Type = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketActionLogEntries", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TicketDealLogEntries",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    EventDate = table.Column<DateTime>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    OwnerId = table.Column<string>(nullable: true),
                    RecieverId = table.Column<string>(nullable: true),
                    Price = table.Column<double>(nullable: true),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TicketDealLogEntries", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccountLogEntries");

            migrationBuilder.DropTable(
                name: "InternalErrorLogEntries");

            migrationBuilder.DropTable(
                name: "PaymentLogEntries");

            migrationBuilder.DropTable(
                name: "SearchQueryLogEntries");

            migrationBuilder.DropTable(
                name: "TicketActionLogEntries");

            migrationBuilder.DropTable(
                name: "TicketDealLogEntries");
        }
    }
}
