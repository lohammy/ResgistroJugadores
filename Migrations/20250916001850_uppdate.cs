using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ResgistroJugadores.Migrations
{
    /// <inheritdoc />
    public partial class uppdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Partida",
                table: "Jugadores",
                newName: "Victorias");

            migrationBuilder.AddColumn<int>(
                name: "Derrotas",
                table: "Jugadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Empates",
                table: "Jugadores",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Derrotas",
                table: "Jugadores");

            migrationBuilder.DropColumn(
                name: "Empates",
                table: "Jugadores");

            migrationBuilder.RenameColumn(
                name: "Victorias",
                table: "Jugadores",
                newName: "Partida");
        }
    }
}
