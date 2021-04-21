using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Infrustructure.Migrations
{
    public partial class CatDescAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CatDescription",
                table: "Categories",
                type: "varchar(150)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CatDescription",
                table: "Categories");
        }
    }
}
