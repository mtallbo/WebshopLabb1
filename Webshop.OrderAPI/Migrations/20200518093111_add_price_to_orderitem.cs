using Microsoft.EntityFrameworkCore.Migrations;

namespace Webshop.OrderAPI.Migrations
{
    public partial class add_price_to_orderitem : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "OrderItem",
                nullable: false,
                defaultValue: 0m);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "OrderItem");
        }
    }
}
