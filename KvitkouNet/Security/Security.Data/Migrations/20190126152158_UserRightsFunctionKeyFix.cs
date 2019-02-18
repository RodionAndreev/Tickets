using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class UserRightsFunctionKeyFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRightsAccessFunction",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_UserRightsAccessFunction_UserId_AccessFunctionId",
                table: "UserRightsAccessFunction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRightsAccessFunction",
                table: "UserRightsAccessFunction",
                columns: new[] { "UserId", "AccessFunctionId" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRightsAccessFunction",
                table: "UserRightsAccessFunction");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRightsAccessFunction",
                table: "UserRightsAccessFunction",
                column: "UserId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_UserRightsAccessFunction_UserId_AccessFunctionId",
                table: "UserRightsAccessFunction",
                columns: new[] { "UserId", "AccessFunctionId" });
        }
    }
}
