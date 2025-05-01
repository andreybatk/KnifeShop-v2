using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KnifeShop.DB.Migrations
{
    /// <inheritdoc />
    public partial class AddedFieldToCategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "Categories",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "Categories");
        }
    }
}
