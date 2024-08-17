using Microsoft.EntityFrameworkCore.Migrations;

namespace NetPinProc.Game.Sqlite.Migrations
{
    public partial class AddedColorSets : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Colors",
                columns: table => new
                {
                    Name = table.Column<string>(maxLength: 6, nullable: false),
                    HtmlCode = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Colors", x => x.Name);
                });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "BLK", "010101" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "WHT", "FEFEFE" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "BLU", "0155FE" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "RED", "FF0000" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "YEL", "EDFE01" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "ORG", "FE9001" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "GRN", "3CDF13" });

            migrationBuilder.InsertData(
                table: "Colors",
                columns: new[] { "Name", "HtmlCode" },
                values: new object[] { "VIO", "A913DF" });

            migrationBuilder.CreateIndex(
                name: "IX_Colors_Name",
                table: "Colors",
                column: "Name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Colors");
        }
    }
}
