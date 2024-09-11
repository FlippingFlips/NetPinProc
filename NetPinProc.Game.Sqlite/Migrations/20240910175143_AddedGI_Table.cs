using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetPinProc.Game.Sqlite.Migrations
{
    /// <inheritdoc />
    public partial class AddedGI_Table : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GI",
                columns: table => new
                {
                    Number = table.Column<string>(type: "TEXT", nullable: false),
                    Polarity = table.Column<bool>(type: "INTEGER", nullable: false),
                    Tags = table.Column<string>(type: "TEXT", nullable: true),
                    Name = table.Column<string>(type: "TEXT", maxLength: 50, nullable: false),
                    DisplayName = table.Column<string>(type: "TEXT", nullable: true),
                    Conn = table.Column<string>(type: "TEXT", nullable: true),
                    Location = table.Column<string>(type: "TEXT", nullable: true),
                    XPos = table.Column<double>(type: "REAL", nullable: true),
                    YPos = table.Column<double>(type: "REAL", nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GI", x => x.Number);
                });

            migrationBuilder.CreateIndex(
                name: "IX_GI_Name",
                table: "GI",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_GI_Number",
                table: "GI",
                column: "Number",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GI");
        }
    }
}
