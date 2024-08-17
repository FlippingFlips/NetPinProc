using Microsoft.EntityFrameworkCore.Migrations;

namespace NetPinProc.Game.Sqlite.Migrations
{
    public partial class ServosSerialLeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsEnabled",
                table: "Steppers",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Lpd8806Leds",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    BoardId = table.Column<byte>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    First = table.Column<uint>(nullable: false),
                    Last = table.Column<uint>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lpd8806Leds", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Servos",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Index = table.Column<uint>(nullable: false),
                    BoardId = table.Column<byte>(nullable: false),
                    MinValue = table.Column<int>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Servos", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "WS281xLeds",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    BoardId = table.Column<byte>(nullable: false),
                    Index = table.Column<int>(nullable: false),
                    First = table.Column<uint>(nullable: false),
                    Last = table.Column<uint>(nullable: false),
                    IsEnabled = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WS281xLeds", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Lpd8806Leds_Name",
                table: "Lpd8806Leds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lpd8806Leds_BoardId_Index",
                table: "Lpd8806Leds",
                columns: new[] { "BoardId", "Index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servos_Name",
                table: "Servos",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Servos_BoardId_Index",
                table: "Servos",
                columns: new[] { "BoardId", "Index" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WS281xLeds_Name",
                table: "WS281xLeds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_WS281xLeds_BoardId_Index",
                table: "WS281xLeds",
                columns: new[] { "BoardId", "Index" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lpd8806Leds");

            migrationBuilder.DropTable(
                name: "Servos");

            migrationBuilder.DropTable(
                name: "WS281xLeds");

            migrationBuilder.DropColumn(
                name: "IsEnabled",
                table: "Steppers");
        }
    }
}
