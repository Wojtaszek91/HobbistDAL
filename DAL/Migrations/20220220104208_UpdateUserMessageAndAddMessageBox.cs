using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateUserMessageAndAddMessageBox : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasBeenSend",
                table: "UserMessages");

            migrationBuilder.DropColumn(
                name: "SenderProfileId",
                table: "UserMessages");

            migrationBuilder.RenameColumn(
                name: "TargetUserName",
                table: "UserMessages",
                newName: "SenderName");

            migrationBuilder.RenameColumn(
                name: "TargetProfileId",
                table: "UserMessages",
                newName: "MessageBoxId");

            migrationBuilder.CreateTable(
                name: "MessageBoxes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileOneId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    ProfileTwoId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MessageBoxes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserMessages_MessageBoxId",
                table: "UserMessages",
                column: "MessageBoxId");

            migrationBuilder.AddForeignKey(
                name: "FK_UserMessages_MessageBoxes_MessageBoxId",
                table: "UserMessages",
                column: "MessageBoxId",
                principalTable: "MessageBoxes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserMessages_MessageBoxes_MessageBoxId",
                table: "UserMessages");

            migrationBuilder.DropTable(
                name: "MessageBoxes");

            migrationBuilder.DropIndex(
                name: "IX_UserMessages_MessageBoxId",
                table: "UserMessages");

            migrationBuilder.RenameColumn(
                name: "SenderName",
                table: "UserMessages",
                newName: "TargetUserName");

            migrationBuilder.RenameColumn(
                name: "MessageBoxId",
                table: "UserMessages",
                newName: "TargetProfileId");

            migrationBuilder.AddColumn<bool>(
                name: "HasBeenSend",
                table: "UserMessages",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<Guid>(
                name: "SenderProfileId",
                table: "UserMessages",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
