using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace StatisticUser.Data.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MessagesUsersOnSiteDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    MessageCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessagesUsersOnSiteDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RatingDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    Positive = table.Column<int>(nullable: false),
                    Negative = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RatingDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ResourcesUrlDB",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ResourceUrl = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ResourcesUrlDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TimeOnSiteDB",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    UserId = table.Column<int>(nullable: false),
                    DataTimeAuthorization = table.Column<DateTime>(nullable: false),
                    Timeuser = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeOnSiteDB", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OpenResourcesDb",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Created = table.Column<DateTime>(nullable: false),
                    Modified = table.Column<DateTime>(nullable: true),
                    ResourceIdId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    TimeOnResource = table.Column<TimeSpan>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OpenResourcesDb", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OpenResourcesDb_ResourcesUrlDB_ResourceIdId",
                        column: x => x.ResourceIdId,
                        principalTable: "ResourcesUrlDB",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OpenResourcesDb_ResourceIdId",
                table: "OpenResourcesDb",
                column: "ResourceIdId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MessagesUsersOnSiteDB");

            migrationBuilder.DropTable(
                name: "OpenResourcesDb");

            migrationBuilder.DropTable(
                name: "RatingDB");

            migrationBuilder.DropTable(
                name: "TimeOnSiteDB");

            migrationBuilder.DropTable(
                name: "ResourcesUrlDB");
        }
    }
}
