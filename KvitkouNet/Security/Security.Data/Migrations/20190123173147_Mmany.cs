using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    public partial class Mmany : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeatureAccessRight_AccessRightId",
                table: "FeatureAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_AccessFunctionAccessRight_AccessRightId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureAccessRight_AccessRightId",
                table: "FeatureAccessRight",
                column: "AccessRightId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunctionAccessRight_AccessRightId",
                table: "AccessFunctionAccessRight",
                column: "AccessRightId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_FeatureAccessRight_AccessRightId",
                table: "FeatureAccessRight");

            migrationBuilder.DropIndex(
                name: "IX_AccessFunctionAccessRight_AccessRightId",
                table: "AccessFunctionAccessRight");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureAccessRight_AccessRightId",
                table: "FeatureAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunctionAccessRight_AccessRightId",
                table: "AccessFunctionAccessRight",
                column: "AccessRightId",
                unique: true);
        }
    }
}
