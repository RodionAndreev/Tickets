using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class UserRightsKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRightsAccessRight",
                table: "UserRightsAccessRight");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserRightsAccessRight_UserId_AccessRightId",
                table: "UserRightsAccessRight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRightsAccessRight",
                table: "UserRightsAccessRight",
                columns: new[] { "UserId", "AccessRightId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRightsAccessRight",
                table: "UserRightsAccessRight");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRightsAccessRight",
                table: "UserRightsAccessRight",
                column: "UserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserRightsAccessRight_UserId_AccessRightId",
                table: "UserRightsAccessRight",
                columns: new[] { "UserId", "AccessRightId" });
        }
    }
}
