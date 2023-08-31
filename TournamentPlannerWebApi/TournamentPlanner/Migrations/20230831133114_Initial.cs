using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TournamentPlanner.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Round = table.Column<int>(type: "int", nullable: false),
                    FirstPlayerID = table.Column<int>(type: "int", nullable: false),
                    SecondPlayerID = table.Column<int>(type: "int", nullable: false),
                    WinnerID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matches_Players_FirstPlayerID",
                        column: x => x.FirstPlayerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_Players_SecondPlayerID",
                        column: x => x.SecondPlayerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matches_Players_WinnerID",
                        column: x => x.WinnerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Matches_FirstPlayerID",
                table: "Matches",
                column: "FirstPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_SecondPlayerID",
                table: "Matches",
                column: "SecondPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_WinnerID",
                table: "Matches",
                column: "WinnerID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropTable(
                name: "Players");
        }
    }
}
