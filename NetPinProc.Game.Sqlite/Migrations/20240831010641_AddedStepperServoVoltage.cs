using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedStepperServoVoltage : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte>(
                name: "Voltage",
                table: "Steppers",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<byte>(
                name: "Voltage",
                table: "Servos",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "Steppers");

            migrationBuilder.DropColumn(
                name: "Voltage",
                table: "Servos");
        }
    }
}
