using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedStepperStopSwitch : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "StopSwitch",
                table: "Steppers",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StopSwitch",
                table: "Steppers");
        }
    }
}
