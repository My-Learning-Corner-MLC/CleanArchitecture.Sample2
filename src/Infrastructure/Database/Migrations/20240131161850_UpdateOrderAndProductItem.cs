using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample2.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class UpdateOrderAndProductItem : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProductItemId",
                table: "ProductItemOrdered");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Order",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Order");

            migrationBuilder.AddColumn<int>(
                name: "ProductItemId",
                table: "ProductItemOrdered",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
