using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace NetPinProc.Game.Sqlite.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Adjustments",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    MenuName = table.Column<string>(nullable: false),
                    SubMenuName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: false),
                    ValueDefault = table.Column<int>(nullable: false),
                    Value = table.Column<int>(nullable: false),
                    OptionType = table.Column<int>(nullable: false),
                    Options = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Adjustments", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Audits",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<int>(nullable: false),
                    Type = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Audits", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Coils",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    PulseTime = table.Column<int>(nullable: false),
                    Bus = table.Column<string>(nullable: true),
                    Polarity = table.Column<bool>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    Search = table.Column<int>(nullable: false),
                    ReturnWire = table.Column<string>(nullable: true),
                    VoltageWire = table.Column<string>(nullable: true),
                    Voltage = table.Column<byte>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coils", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "GamesPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Started = table.Column<DateTime>(nullable: false),
                    Ended = table.Column<DateTime>(nullable: false),
                    Adjustments = table.Column<string>(nullable: true),
                    BallsPerGame = table.Column<byte>(nullable: false),
                    GameTime = table.Column<double>(type: "DOUBLE(18, 2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GamesPlayed", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Lamps",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Bus = table.Column<string>(nullable: true),
                    Polarity = table.Column<bool>(nullable: false),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lamps", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Leds",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Bus = table.Column<string>(nullable: true),
                    Polarity = table.Column<bool>(nullable: false),
                    Tags = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Leds", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Machine",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    MachineType = table.Column<int>(nullable: false),
                    NumBalls = table.Column<int>(nullable: false),
                    DisplayMonitor = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    Version = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Machine", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Initials = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Default = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Switches",
                columns: table => new
                {
                    Number = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    Tags = table.Column<string>(nullable: true),
                    SearchReset = table.Column<string>(nullable: true),
                    SearchStop = table.Column<string>(nullable: true),
                    InputWire = table.Column<string>(nullable: true),
                    GroundWire = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Switches", x => x.Number);
                });

            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Points = table.Column<long>(nullable: false),
                    PlayerId = table.Column<int>(nullable: false),
                    GamePlayedId = table.Column<int>(nullable: false),
                    ExtraBallsPlayed = table.Column<byte>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Scores_GamesPlayed_GamePlayedId",
                        column: x => x.GamePlayedId,
                        principalTable: "GamesPlayed",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Scores_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BallsPlayed",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PlayerId = table.Column<int>(nullable: false),
                    ScoreId = table.Column<int>(nullable: false),
                    Ball = table.Column<byte>(nullable: false),
                    Points = table.Column<long>(nullable: false),
                    Time = table.Column<double>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BallsPlayed", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BallsPlayed_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BallsPlayed_Scores_ScoreId",
                        column: x => x.ScoreId,
                        principalTable: "Scores",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Adjustments_Id",
                table: "Adjustments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BallsPlayed_PlayerId",
                table: "BallsPlayed",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_BallsPlayed_ScoreId",
                table: "BallsPlayed",
                column: "ScoreId");

            migrationBuilder.CreateIndex(
                name: "IX_Coils_Name",
                table: "Coils",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Coils_Number",
                table: "Coils",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lamps_Name",
                table: "Lamps",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Lamps_Number",
                table: "Lamps",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leds_Name",
                table: "Leds",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Leds_Number",
                table: "Leds",
                column: "Number",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Players_Name_Initials",
                table: "Players",
                columns: new[] { "Name", "Initials" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Scores_GamePlayedId",
                table: "Scores",
                column: "GamePlayedId");

            migrationBuilder.CreateIndex(
                name: "IX_Scores_PlayerId",
                table: "Scores",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Switches_Name",
                table: "Switches",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Switches_Number",
                table: "Switches",
                column: "Number",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Adjustments");

            migrationBuilder.DropTable(
                name: "Audits");

            migrationBuilder.DropTable(
                name: "BallsPlayed");

            migrationBuilder.DropTable(
                name: "Coils");

            migrationBuilder.DropTable(
                name: "Lamps");

            migrationBuilder.DropTable(
                name: "Leds");

            migrationBuilder.DropTable(
                name: "Machine");

            migrationBuilder.DropTable(
                name: "Switches");

            migrationBuilder.DropTable(
                name: "Scores");

            migrationBuilder.DropTable(
                name: "GamesPlayed");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
