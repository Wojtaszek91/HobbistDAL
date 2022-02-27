using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.Migrations
{
    public partial class UpdateMessageBoxForUserNames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProfileOneUsername",
                table: "MessageBoxes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProfileTwoUsername",
                table: "MessageBoxes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfileOneUsername",
                table: "MessageBoxes");

            migrationBuilder.DropColumn(
                name: "ProfileTwoUsername",
                table: "MessageBoxes");
        }
    }
}
