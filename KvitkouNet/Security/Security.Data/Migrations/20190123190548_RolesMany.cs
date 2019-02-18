using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class RolesMany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRightsAccessRight_AccessRightId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_UserRightsAccessFunction_AccessFunctionId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccessRight_AccessRightId",
                table: "RoleAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccessFunction_AccessFunctionId",
                table: "RoleAccessFunction");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessRight_AccessRightId",
                table: "UserRightsAccessRight",
                column: "AccessRightId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessFunction_AccessFunctionId",
                table: "UserRightsAccessFunction",
                column: "AccessFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessRight_AccessRightId",
                table: "RoleAccessRight",
                column: "AccessRightId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessFunction_AccessFunctionId",
                table: "RoleAccessFunction",
                column: "AccessFunctionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_UserRightsAccessRight_AccessRightId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_UserRightsAccessFunction_AccessFunctionId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccessRight_AccessRightId",
                table: "RoleAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_RoleAccessFunction_AccessFunctionId",
                table: "RoleAccessFunction");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessRight_AccessRightId",
                table: "UserRightsAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessFunction_AccessFunctionId",
                table: "UserRightsAccessFunction",
                column: "AccessFunctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessRight_AccessRightId",
                table: "RoleAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessFunction_AccessFunctionId",
                table: "RoleAccessFunction",
                column: "AccessFunctionId",
                unique: true);
        }
    }
}
