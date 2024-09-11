using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedDataToPlayer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Data",
                table: "Players",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Players");
        }
    }
}
