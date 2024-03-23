using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Sample2.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class ChangeProductItemOrderedToProductItemReference : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductItemOrdered_ItemOrderedId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductItemOrdered");

            migrationBuilder.CreateTable(
                name: "ProductItemReference",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PictureUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemReference", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductItemReference_ItemOrderedId",
                table: "OrderItem",
                column: "ItemOrderedId",
                principalTable: "ProductItemReference",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_ProductItemReference_ItemOrderedId",
                table: "OrderItem");

            migrationBuilder.DropTable(
                name: "ProductItemReference");

            migrationBuilder.CreateTable(
                name: "ProductItemOrdered",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Created = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    LastModified = table.Column<DateTimeOffset>(type: "datetimeoffset", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PictureUri = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductItemOrdered", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_ProductItemOrdered_ItemOrderedId",
                table: "OrderItem",
                column: "ItemOrderedId",
                principalTable: "ProductItemOrdered",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
