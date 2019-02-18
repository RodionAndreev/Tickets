using Microsoft.EntityFrameworkCore.Migrations;

namespace Security.Data.Migrations
{
    internal partial class Init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AccessRights",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessRights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Features",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Features", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "UsersRights",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    UserLogin = table.Column<string>(maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(maxLength: 100, nullable: false),
                    MiddleName = table.Column<string>(maxLength: 100, nullable: true),
                    LastName = table.Column<string>(maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersRights", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "AccessFunctions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(maxLength: 100, nullable: false),
                    FeatureId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessFunctions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AccessFunctions_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "FeatureAccessRight",
                columns: table => new
                {
                    FeatureId = table.Column<int>(nullable: false),
                    AccessRightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FeatureAccessRight", x => new { x.FeatureId, x.AccessRightId });
                    table.ForeignKey(
                        name: "FK_FeatureAccessRight_AccessRights_AccessRightId",
                        column: x => x.AccessRightId,
                        principalTable: "AccessRights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_FeatureAccessRight_Features_FeatureId",
                        column: x => x.FeatureId,
                        principalTable: "Features",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoleAccessRight",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    AccessRightId = table.Column<int>(nullable: false),
                    IsDenied = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccessRight", x => new { x.RoleId, x.AccessRightId });
                    table.ForeignKey(
                        name: "FK_RoleAccessRight_AccessRights_AccessRightId",
                        column: x => x.AccessRightId,
                        principalTable: "AccessRights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoleAccessRight_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRightsAccessRight",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AccessRightId = table.Column<int>(nullable: false),
                    IsDenied = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRightsAccessRight", x => x.UserId);
                    table.UniqueConstraint("AK_UserRightsAccessRight_UserId_AccessRightId", x => new { x.UserId, x.AccessRightId });
                    table.ForeignKey(
                        name: "FK_UserRightsAccessRight_AccessRights_AccessRightId",
                        column: x => x.AccessRightId,
                        principalTable: "AccessRights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserRightsAccessRight_UsersRights_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersRights",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRightsRole",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRightsRole", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRightsRole_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserRightsRole_UsersRights_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersRights",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "AccessFunctionAccessRight",
                columns: table => new
                {
                    AccessFunctionId = table.Column<int>(nullable: false),
                    AccessRightId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AccessFunctionAccessRight", x => new { x.AccessFunctionId, x.AccessRightId });
                    table.ForeignKey(
                        name: "FK_AccessFunctionAccessRight_AccessFunctions_AccessFunctionId",
                        column: x => x.AccessFunctionId,
                        principalTable: "AccessFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_AccessFunctionAccessRight_AccessRights_AccessRightId",
                        column: x => x.AccessRightId,
                        principalTable: "AccessRights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "RoleAccessFunction",
                columns: table => new
                {
                    RoleId = table.Column<int>(nullable: false),
                    AccessFunctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleAccessFunction", x => new { x.RoleId, x.AccessFunctionId });
                    table.ForeignKey(
                        name: "FK_RoleAccessFunction_AccessFunctions_AccessFunctionId",
                        column: x => x.AccessFunctionId,
                        principalTable: "AccessFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_RoleAccessFunction_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateTable(
                name: "UserRightsAccessFunction",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    AccessFunctionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRightsAccessFunction", x => x.UserId);
                    table.UniqueConstraint("AK_UserRightsAccessFunction_UserId_AccessFunctionId", x => new { x.UserId, x.AccessFunctionId });
                    table.ForeignKey(
                        name: "FK_UserRightsAccessFunction_AccessFunctions_AccessFunctionId",
                        column: x => x.AccessFunctionId,
                        principalTable: "AccessFunctions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.SetNull);
                    table.ForeignKey(
                        name: "FK_UserRightsAccessFunction_UsersRights_UserId",
                        column: x => x.UserId,
                        principalTable: "UsersRights",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.SetNull);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunctionAccessRight_AccessFunctionId",
                table: "AccessFunctionAccessRight",
                column: "AccessFunctionId");

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunctionAccessRight_AccessRightId",
                table: "AccessFunctionAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AccessFunctions_FeatureId",
                table: "AccessFunctions",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_FeatureAccessRight_AccessRightId",
                table: "FeatureAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_FeatureAccessRight_FeatureId",
                table: "FeatureAccessRight",
                column: "FeatureId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessFunction_AccessFunctionId",
                table: "RoleAccessFunction",
                column: "AccessFunctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessFunction_RoleId",
                table: "RoleAccessFunction",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessRight_AccessRightId",
                table: "RoleAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_RoleAccessRight_RoleId",
                table: "RoleAccessRight",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessFunction_AccessFunctionId",
                table: "UserRightsAccessFunction",
                column: "AccessFunctionId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsAccessRight_AccessRightId",
                table: "UserRightsAccessRight",
                column: "AccessRightId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsRole_RoleId",
                table: "UserRightsRole",
                column: "RoleId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserRightsRole_UserId",
                table: "UserRightsRole",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AccessFunctionAccessRight");

            migrationBuilder.DropTable(
                name: "FeatureAccessRight");

            migrationBuilder.DropTable(
                name: "RoleAccessFunction");

            migrationBuilder.DropTable(
                name: "RoleAccessRight");

            migrationBuilder.DropTable(
                name: "UserRightsAccessFunction");

            migrationBuilder.DropTable(
                name: "UserRightsAccessRight");

            migrationBuilder.DropTable(
                name: "UserRightsRole");

            migrationBuilder.DropTable(
                name: "AccessFunctions");

            migrationBuilder.DropTable(
                name: "AccessRights");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "UsersRights");

            migrationBuilder.DropTable(
                name: "Features");
        }
    }
}
