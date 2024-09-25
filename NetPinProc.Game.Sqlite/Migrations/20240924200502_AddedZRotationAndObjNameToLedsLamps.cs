using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedZRotationAndObjNameToLedsLamps : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ObjName",
                table: "Leds",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ZRot",
                table: "Leds",
                type: "REAL",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ObjName",
                table: "Lamps",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ZRot",
                table: "Lamps",
                type: "REAL",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ObjName",
                table: "Leds");

            migrationBuilder.DropColumn(
                name: "ZRot",
                table: "Leds");

            migrationBuilder.DropColumn(
                name: "ObjName",
                table: "Lamps");

            migrationBuilder.DropColumn(
                name: "ZRot",
                table: "Lamps");
        }
    }
}
