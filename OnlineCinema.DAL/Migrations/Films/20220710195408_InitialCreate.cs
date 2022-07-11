using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace OnlineCinema.DAL.Migrations.Films
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Films",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FilmName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genres = table.Column<int>(type: "int", nullable: false),
                    IMDbRaiting = table.Column<float>(type: "real", nullable: false),
                    RottenTomatoesRaiting = table.Column<float>(type: "real", nullable: false),
                    ReleaseData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagnetLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Films", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Serials",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Genres = table.Column<int>(type: "int", nullable: false),
                    IMDbRaiting = table.Column<float>(type: "real", nullable: false),
                    RottenTomatoesRaiting = table.Column<float>(type: "real", nullable: false),
                    ReleaseData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    AverageRuntime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagnetLink = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Serials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Seasons",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SeasonNumber = table.Column<int>(type: "int", nullable: false),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    SeasonName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMDbRaiting = table.Column<float>(type: "real", nullable: false),
                    RottenTomatoesRaiting = table.Column<float>(type: "real", nullable: false),
                    ReleaseData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagnetLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SerialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Seasons", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Seasons_Serials_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Episodes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EpisodeNumber = table.Column<int>(type: "int", nullable: false),
                    SeasonNumber = table.Column<int>(type: "int", nullable: false),
                    SerialName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PosterSource = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IMDbRaiting = table.Column<float>(type: "real", nullable: false),
                    RottenTomatoesRaiting = table.Column<float>(type: "real", nullable: false),
                    ReleaseData = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Runtime = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    MagnetLink = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Episodes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Episodes_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Actors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: true),
                    FilmId = table.Column<int>(type: "int", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    SerialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Actors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Actors_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actors_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actors_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Actors_Serials_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Directors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: true),
                    FilmId = table.Column<int>(type: "int", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    SerialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Directors", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Directors_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Directors_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Directors_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Directors_Serials_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Writers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PersonalName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    Country = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DateOfBirth = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EpisodeId = table.Column<int>(type: "int", nullable: true),
                    FilmId = table.Column<int>(type: "int", nullable: true),
                    SeasonId = table.Column<int>(type: "int", nullable: true),
                    SerialId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Writers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Writers_Episodes_EpisodeId",
                        column: x => x.EpisodeId,
                        principalTable: "Episodes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Writers_Films_FilmId",
                        column: x => x.FilmId,
                        principalTable: "Films",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Writers_Seasons_SeasonId",
                        column: x => x.SeasonId,
                        principalTable: "Seasons",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Writers_Serials_SerialId",
                        column: x => x.SerialId,
                        principalTable: "Serials",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Actors_EpisodeId",
                table: "Actors",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_FilmId",
                table: "Actors",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SeasonId",
                table: "Actors",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Actors_SerialId",
                table: "Actors",
                column: "SerialId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_EpisodeId",
                table: "Directors",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_FilmId",
                table: "Directors",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_SeasonId",
                table: "Directors",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Directors_SerialId",
                table: "Directors",
                column: "SerialId");

            migrationBuilder.CreateIndex(
                name: "IX_Episodes_SeasonId",
                table: "Episodes",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Seasons_SerialId",
                table: "Seasons",
                column: "SerialId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_EpisodeId",
                table: "Writers",
                column: "EpisodeId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_FilmId",
                table: "Writers",
                column: "FilmId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_SeasonId",
                table: "Writers",
                column: "SeasonId");

            migrationBuilder.CreateIndex(
                name: "IX_Writers_SerialId",
                table: "Writers",
                column: "SerialId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Actors");

            migrationBuilder.DropTable(
                name: "Directors");

            migrationBuilder.DropTable(
                name: "Writers");

            migrationBuilder.DropTable(
                name: "Episodes");

            migrationBuilder.DropTable(
                name: "Films");

            migrationBuilder.DropTable(
                name: "Seasons");

            migrationBuilder.DropTable(
                name: "Serials");
        }
    }
}
