using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedSwitchDebouncAndSingleColorOptionToLed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "NonDebounce",
                table: "Switches",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "SingleColor",
                table: "Leds",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NonDebounce",
                table: "Switches");

            migrationBuilder.DropColumn(
                name: "SingleColor",
                table: "Leds");
        }
    }
}
