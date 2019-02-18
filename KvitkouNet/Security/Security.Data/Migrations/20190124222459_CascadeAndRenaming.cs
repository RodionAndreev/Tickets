using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class CascadeAndRenaming : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessFunctions_AccessFunctionId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessRights_AccessRightId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessFunction_AccessFunctions_AccessFunctionId",
                table: "RoleAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessFunction_Roles_RoleId",
                table: "RoleAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessRight_AccessRights_AccessRightId",
                table: "RoleAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessRight_Roles_RoleId",
                table: "RoleAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessFunction_AccessFunctions_AccessFunctionId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessFunction_UsersRights_UserId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessRight_AccessRights_AccessRightId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessRight_UsersRights_UserId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsRole_Roles_RoleId",
                table: "UserRightsRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsRole_UsersRights_UserId",
                table: "UserRightsRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRightsRole_RoleId",
                table: "UserRightsRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsRole_RoleId",
                table: "UserRightsRole",
                column: "RoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessFunctions_AccessFunctionId",
                table: "AccessFunctionAccessRight",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessRights_AccessRightId",
                table: "AccessFunctionAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessFunction_AccessFunctions_AccessFunctionId",
                table: "RoleAccessFunction",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessFunction_Roles_RoleId",
                table: "RoleAccessFunction",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessRight_AccessRights_AccessRightId",
                table: "RoleAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessRight_Roles_RoleId",
                table: "RoleAccessRight",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessFunction_AccessFunctions_AccessFunctionId",
                table: "UserRightsAccessFunction",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessFunction_UsersRights_UserId",
                table: "UserRightsAccessFunction",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessRight_AccessRights_AccessRightId",
                table: "UserRightsAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessRight_UsersRights_UserId",
                table: "UserRightsAccessRight",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsRole_Roles_RoleId",
                table: "UserRightsRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsRole_UsersRights_UserId",
                table: "UserRightsRole",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessFunctions_AccessFunctionId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessRights_AccessRightId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessFunction_AccessFunctions_AccessFunctionId",
                table: "RoleAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessFunction_Roles_RoleId",
                table: "RoleAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessRight_AccessRights_AccessRightId",
                table: "RoleAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_RoleAccessRight_Roles_RoleId",
                table: "RoleAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessFunction_AccessFunctions_AccessFunctionId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessFunction_UsersRights_UserId",
                table: "UserRightsAccessFunction");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessRight_AccessRights_AccessRightId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsAccessRight_UsersRights_UserId",
                table: "UserRightsAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsRole_Roles_RoleId",
                table: "UserRightsRole");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRightsRole_UsersRights_UserId",
                table: "UserRightsRole");

            migrationBuilder.DropIndex(
                name: "IX_UserRightsRole_RoleId",
                table: "UserRightsRole");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsRole_RoleId",
                table: "UserRightsRole",
                column: "RoleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessFunctions_AccessFunctionId",
                table: "AccessFunctionAccessRight",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_AccessFunctionAccessRight_AccessRights_AccessRightId",
                table: "AccessFunctionAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessFunction_AccessFunctions_AccessFunctionId",
                table: "RoleAccessFunction",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessFunction_Roles_RoleId",
                table: "RoleAccessFunction",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessRight_AccessRights_AccessRightId",
                table: "RoleAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_RoleAccessRight_Roles_RoleId",
                table: "RoleAccessRight",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessFunction_AccessFunctions_AccessFunctionId",
                table: "UserRightsAccessFunction",
                column: "AccessFunctionId",
                principalTable: "AccessFunctions",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessFunction_UsersRights_UserId",
                table: "UserRightsAccessFunction",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessRight_AccessRights_AccessRightId",
                table: "UserRightsAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsAccessRight_UsersRights_UserId",
                table: "UserRightsAccessRight",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsRole_Roles_RoleId",
                table: "UserRightsRole",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRightsRole_UsersRights_UserId",
                table: "UserRightsRole",
                column: "UserId",
                principalTable: "UsersRights",
                principalColumn: "UserId",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
