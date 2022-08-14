using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Project.Infrastructure.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BikeStations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Departure = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departure_station_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Departure_station_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return_station_id = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Return_station_name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Covered_distance = table.Column<double>(type: "float", nullable: false),
                    Duration = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BikeStations", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BikeStations");
        }
    }
}
