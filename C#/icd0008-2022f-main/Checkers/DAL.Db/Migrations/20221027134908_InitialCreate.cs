﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DAL.Db.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "CheckersOptions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Width = table.Column<int>(type: "INTEGER", nullable: false),
                    Height = table.Column<int>(type: "INTEGER", nullable: false),
                    RandomMoves = table.Column<int>(type: "INTEGER", nullable: false),
                    WhiteStarts = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckersOptions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CheckersGames",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StartedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndedAt = table.Column<DateTime>(type: "TEXT", nullable: true),
                    GameWonPlayer = table.Column<string>(type: "TEXT", nullable: true),
                    Player1Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Player1Type = table.Column<int>(type: "INTEGER", nullable: false),
                    Player2Name = table.Column<string>(type: "TEXT", maxLength: 128, nullable: false),
                    Player2Type = table.Column<int>(type: "INTEGER", nullable: false),
                    CheckersOptionId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckersGames", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckersGames_CheckersOptions_CheckersOptionId",
                        column: x => x.CheckersOptionId,
                        principalTable: "CheckersOptions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckersGameStates",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    SerializedGameState = table.Column<string>(type: "TEXT", nullable: false),
                    CheckersGameId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckersGameStates", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckersGameStates_CheckersGames_CheckersGameId",
                        column: x => x.CheckersGameId,
                        principalTable: "CheckersGames",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckersGames_CheckersOptionId",
                table: "CheckersGames",
                column: "CheckersOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckersGameStates_CheckersGameId",
                table: "CheckersGameStates",
                column: "CheckersGameId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckersGameStates");

            migrationBuilder.DropTable(
                name: "CheckersGames");

            migrationBuilder.DropTable(
                name: "CheckersOptions");
        }
    }
}
