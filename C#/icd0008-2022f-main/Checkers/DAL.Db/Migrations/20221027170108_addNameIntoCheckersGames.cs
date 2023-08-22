using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Db.Migrations
{
    public partial class addNameIntoCheckersGames : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "CheckersGames",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "CheckersGames");
        }
    }
}
