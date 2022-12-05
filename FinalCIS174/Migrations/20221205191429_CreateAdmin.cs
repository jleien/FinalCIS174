using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FinalCIS174.Migrations
{
    /// <inheritdoc />
    public partial class CreateAdmin : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CreatorOfCharacter",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Players",
                keyColumn: "PlayerID",
                keyValue: "1",
                column: "CreatorOfCharacter",
                value: "DIO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatorOfCharacter",
                table: "Players");
        }
    }
}
