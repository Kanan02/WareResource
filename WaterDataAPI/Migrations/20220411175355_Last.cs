using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WaterDataAPI.Migrations
{
    public partial class Last : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Channels",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollutionLevel = table.Column<double>(type: "float", nullable: false),
                    StandardWaterHeight = table.Column<double>(type: "float", nullable: false),
                    CurrentWaterHeight = table.Column<double>(type: "float", nullable: false),
                    CriticalWaterLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Channels", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Farms",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Farms", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Fields",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Stage = table.Column<int>(type: "int", nullable: false),
                    SAT = table.Column<double>(type: "float", nullable: false),
                    PERC = table.Column<double>(type: "float", nullable: false),
                    Wl = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Fields", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "GroundWaterReservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollutionLevel = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    CurrentWaterLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GroundWaterReservoirs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RainWaterReservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollutionLevel = table.Column<double>(type: "float", nullable: false),
                    Height = table.Column<double>(type: "float", nullable: false),
                    Width = table.Column<double>(type: "float", nullable: false),
                    Length = table.Column<double>(type: "float", nullable: false),
                    CurrentWaterLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RainWaterReservoirs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WaterReservoirs",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PollutionLevel = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WaterReservoirs", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Channels",
                columns: new[] { "Id", "CriticalWaterLevel", "CurrentWaterHeight", "Name", "PollutionLevel", "StandardWaterHeight" },
                values: new object[,]
                {
                    { 1, 0.0, 0.0, "River1", 0.0, 0.0 },
                    { 2, 0.0, 0.0, "River2", 0.0, 0.0 }
                });

            migrationBuilder.InsertData(
                table: "GroundWaterReservoirs",
                columns: new[] { "Id", "CurrentWaterLevel", "Height", "Length", "Name", "PollutionLevel", "Width" },
                values: new object[,]
                {
                    { 1, 200.0, 220.0, 0.0, "GS1", 8.0, 100.0 },
                    { 2, 220.0, 320.0, 0.0, "GS2", 12.0, 170.0 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Channels");

            migrationBuilder.DropTable(
                name: "Farms");

            migrationBuilder.DropTable(
                name: "Fields");

            migrationBuilder.DropTable(
                name: "GroundWaterReservoirs");

            migrationBuilder.DropTable(
                name: "RainWaterReservoirs");

            migrationBuilder.DropTable(
                name: "WaterReservoirs");
        }
    }
}
