using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class Cascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureAccessRight_AccessRights_AccessRightId",
                table: "FeatureAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureAccessRight_Features_FeatureId",
                table: "FeatureAccessRight");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureAccessRight_AccessRights_AccessRightId",
                table: "FeatureAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureAccessRight_Features_FeatureId",
                table: "FeatureAccessRight",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FeatureAccessRight_AccessRights_AccessRightId",
                table: "FeatureAccessRight");

            migrationBuilder.DropForeignKey(
                name: "FK_FeatureAccessRight_Features_FeatureId",
                table: "FeatureAccessRight");

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureAccessRight_AccessRights_AccessRightId",
                table: "FeatureAccessRight",
                column: "AccessRightId",
                principalTable: "AccessRights",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);

            migrationBuilder.AddForeignKey(
                name: "FK_FeatureAccessRight_Features_FeatureId",
                table: "FeatureAccessRight",
                column: "FeatureId",
                principalTable: "Features",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }
    }
}
