using Microsoft.EntityFrameworkCore.Migrations;

namespace NetPinProc.Game.Sqlite.Migrations
{
    public partial class AddedPartsAndSteppers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Parts",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    PartNo = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ZPos = table.Column<double>(nullable: true),
                    Cost = table.Column<decimal>(nullable: true),
                    PartType = table.Column<int>(nullable: true),
                    Material = table.Column<int>(nullable: true),
                    Shape = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: true),
                    Height = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parts", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Steppers",
                columns: table => new
                {
                    Name = table.Column<string>(nullable: false),
                    DisplayName = table.Column<string>(nullable: true),
                    Conn = table.Column<string>(nullable: true),
                    Location = table.Column<string>(nullable: true),
                    XPos = table.Column<double>(nullable: true),
                    YPos = table.Column<double>(nullable: true),
                    ItemType = table.Column<string>(nullable: true),
                    IsStepper1 = table.Column<bool>(nullable: false),
                    BoardId = table.Column<byte>(nullable: false),
                    Speed = table.Column<uint>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Steppers", x => x.Name);
                });

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Name",
                keyValue: "RED",
                column: "HtmlCode",
                value: "FF0000");

            migrationBuilder.CreateIndex(
                name: "IX_Steppers_Name",
                table: "Steppers",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steppers_BoardId_IsStepper1",
                table: "Steppers",
                columns: new[] { "BoardId", "IsStepper1" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Parts");

            migrationBuilder.DropTable(
                name: "Steppers");

            migrationBuilder.UpdateData(
                table: "Colors",
                keyColumn: "Name",
                keyValue: "RED",
                column: "HtmlCode",
                value: "FEFEFE");
        }
    }
}
