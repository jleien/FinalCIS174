using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace FinalCIS174.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Races_RaceID",
                table: "Players");

            migrationBuilder.AlterColumn<string>(
                name: "RaceID",
                table: "Players",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "Classes",
                columns: new[] { "ClassID", "Name" },
                values: new object[,]
                {
                    { "druid", "Druid" },
                    { "gunzerker", "Gunzerker" },
                    { "monk", "Monk" },
                    { "operator", "Operator" },
                    { "standuser", "Stand-User" }
                });

            migrationBuilder.InsertData(
                table: "Races",
                columns: new[] { "RaceID", "Name" },
                values: new object[,]
                {
                    { "aasimar", "Aasimar" },
                    { "celestial", "Celestial" },
                    { "plasmoid", "Plasmoid" },
                    { "tabaxi", "Tabaxi" },
                    { "terra", "Terra" },
                    { "tiefling", "Tiefling" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Races_RaceID",
                table: "Players",
                column: "RaceID",
                principalTable: "Races",
                principalColumn: "RaceID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Players_Races_RaceID",
                table: "Players");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassID",
                keyValue: "druid");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassID",
                keyValue: "gunzerker");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassID",
                keyValue: "monk");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassID",
                keyValue: "operator");

            migrationBuilder.DeleteData(
                table: "Classes",
                keyColumn: "ClassID",
                keyValue: "standuser");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "aasimar");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "celestial");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "plasmoid");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "tabaxi");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "terra");

            migrationBuilder.DeleteData(
                table: "Races",
                keyColumn: "RaceID",
                keyValue: "tiefling");

            migrationBuilder.AlterColumn<string>(
                name: "RaceID",
                table: "Players",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Races_RaceID",
                table: "Players",
                column: "RaceID",
                principalTable: "Races",
                principalColumn: "RaceID");
        }
    }
}
