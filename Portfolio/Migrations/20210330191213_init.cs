using Microsoft.EntityFrameworkCore.Migrations;

namespace Portfolio.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Projects",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(nullable: false),
                    SourceURL = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    RepoLink = table.Column<string>(nullable: true),
                    DeployedLink = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Projects", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DeployedLink", "Description", "RepoLink", "SourceURL", "Title" },
                values: new object[] { -1, null, "SmallBoi is a two player coop platform puzzle game built in Unity.  It has both local and network multiple player options using Photon.", "https://github.com/AmeiliaAndTheSmallBois/SmallBoi/tree/main", "images/smallboi.png", "SmallBoi, The Game" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DeployedLink", "Description", "RepoLink", "SourceURL", "Title" },
                values: new object[] { 2, null, "LiteBerry Pi allows users to create and send designs to a RaspBerry Pi with a matrix of led lights attached.  The app uses an api to create and save designs.  The api also contains a route to send designs to the Pi using a SignalR server.", "https://github.com/Lite-Berry-pi/Lite-Berry-Pi", "images/liteberrypi.png", "LiteBerry Pi" });

            migrationBuilder.InsertData(
                table: "Projects",
                columns: new[] { "Id", "DeployedLink", "Description", "RepoLink", "SourceURL", "Title" },
                values: new object[] { 3, "https://scottfalbo.github.io/react-minesweeper-v2/", "A re-creation of the Window's classic Minesweeper.  I built this to practice components and state within a React App.", "https://github.com/scottfalbo/react-minesweeper-v2", "images/minesweeper.png", "React Minesweeper" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Projects");
        }
    }
}
