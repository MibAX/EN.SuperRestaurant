using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EN.SuperRestaurant.MVC.Data.Migrations
{
    /// <inheritdoc />
    public partial class Meals_imageName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "Meals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "Meals");
        }
    }
}
