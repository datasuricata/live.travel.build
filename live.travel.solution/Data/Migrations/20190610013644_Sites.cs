using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace live.travel.solution.Data.Migrations
{
    public partial class Sites : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                newName: "AspNetUserTokens",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                newName: "AspNetUsers",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                newName: "AspNetUserRoles",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                newName: "AspNetUserLogins",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                newName: "AspNetUserClaims",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                newName: "AspNetRoles",
                newSchema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                newName: "AspNetRoleClaims",
                newSchema: "Core");

            migrationBuilder.CreateTable(
                name: "Person",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    PhotoUri = table.Column<string>(nullable: true),
                    CPF = table.Column<string>(nullable: true),
                    RG = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    City = table.Column<string>(nullable: true),
                    State = table.Column<string>(nullable: true),
                    Province = table.Column<string>(nullable: true),
                    CEP = table.Column<string>(nullable: true),
                    Number = table.Column<string>(nullable: true),
                    Complement = table.Column<string>(nullable: true),
                    IdentityUserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Person_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalSchema: "Core",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Site",
                schema: "Core",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    CreatedAt = table.Column<DateTimeOffset>(nullable: true),
                    UpdatedAt = table.Column<DateTimeOffset>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    BannerUri = table.Column<string>(nullable: true),
                    InstaUri = table.Column<string>(nullable: true),
                    FaceUri = table.Column<string>(nullable: true),
                    WhatsUri = table.Column<string>(nullable: true),
                    Job = table.Column<string>(nullable: true),
                    Presentation = table.Column<string>(nullable: true),
                    PersonId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Site", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Site_Person_PersonId",
                        column: x => x.PersonId,
                        principalSchema: "Core",
                        principalTable: "Person",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Person_IdentityUserId",
                schema: "Core",
                table: "Person",
                column: "IdentityUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Site_PersonId",
                schema: "Core",
                table: "Site",
                column: "PersonId",
                unique: true,
                filter: "[PersonId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Site",
                schema: "Core");

            migrationBuilder.DropTable(
                name: "Person",
                schema: "Core");

            migrationBuilder.RenameTable(
                name: "AspNetUserTokens",
                schema: "Core",
                newName: "AspNetUserTokens");

            migrationBuilder.RenameTable(
                name: "AspNetUsers",
                schema: "Core",
                newName: "AspNetUsers");

            migrationBuilder.RenameTable(
                name: "AspNetUserRoles",
                schema: "Core",
                newName: "AspNetUserRoles");

            migrationBuilder.RenameTable(
                name: "AspNetUserLogins",
                schema: "Core",
                newName: "AspNetUserLogins");

            migrationBuilder.RenameTable(
                name: "AspNetUserClaims",
                schema: "Core",
                newName: "AspNetUserClaims");

            migrationBuilder.RenameTable(
                name: "AspNetRoles",
                schema: "Core",
                newName: "AspNetRoles");

            migrationBuilder.RenameTable(
                name: "AspNetRoleClaims",
                schema: "Core",
                newName: "AspNetRoleClaims");
        }
    }
}
