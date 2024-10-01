using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NSFreeway.Migrations
{
    /// <inheritdoc />
    public partial class AddFieldTollRoad : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsTollRoad",
                table: "Highways",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsTollRoad",
                table: "Highways");
        }
    }
}
