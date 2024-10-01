using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSFreeway.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Highways",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    RoadClass = table.Column<int>(type: "INTEGER", nullable: false),
                    RoadNumber = table.Column<ushort>(type: "INTEGER", nullable: false),
                    BeginCity = table.Column<string>(type: "TEXT", nullable: false),
                    BeginState = table.Column<string>(type: "TEXT", nullable: false),
                    EndCity = table.Column<string>(type: "TEXT", nullable: false),
                    EndState = table.Column<string>(type: "TEXT", nullable: false),
                    Length = table.Column<uint>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Highways", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Highways");
        }
    }
}
