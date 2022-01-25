using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class ChangeRelationProfileHashtag : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserProfileHashTags");

            migrationBuilder.CreateTable(
                name: "HashTagUserProfile",
                columns: table => new
                {
                    HashTagsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfilesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HashTagUserProfile", x => new { x.HashTagsId, x.UserProfilesId });
                    table.ForeignKey(
                        name: "FK_HashTagUserProfile_HashTags_HashTagsId",
                        column: x => x.HashTagsId,
                        principalTable: "HashTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HashTagUserProfile_UserProfiles_UserProfilesId",
                        column: x => x.UserProfilesId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HashTagUserProfile_UserProfilesId",
                table: "HashTagUserProfile",
                column: "UserProfilesId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HashTagUserProfile");

            migrationBuilder.CreateTable(
                name: "UserProfileHashTags",
                columns: table => new
                {
                    HashTagId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserProfileId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfileHashTags", x => new { x.HashTagId, x.UserProfileId });
                    table.ForeignKey(
                        name: "FK_UserProfileHashTags_HashTags_HashTagId",
                        column: x => x.HashTagId,
                        principalTable: "HashTags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserProfileHashTags_UserProfiles_UserProfileId",
                        column: x => x.UserProfileId,
                        principalTable: "UserProfiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserProfileHashTags_UserProfileId",
                table: "UserProfileHashTags",
                column: "UserProfileId");
        }
    }
}
