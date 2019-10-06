using Microsoft.EntityFrameworkCore.Migrations;

namespace MusicWorm.Migrations
{
    public partial class NewMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "UnitPrice",
                table: "OrderItem",
                newName: "Price");

            migrationBuilder.RenameColumn(
                name: "Quantity",
                table: "OrderItem",
                newName: "Amount");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem");

            migrationBuilder.RenameColumn(
                name: "Price",
                table: "OrderItem",
                newName: "UnitPrice");

            migrationBuilder.RenameColumn(
                name: "Amount",
                table: "OrderItem",
                newName: "Quantity");

            migrationBuilder.AlterColumn<int>(
                name: "ProductId",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<int>(
                name: "OrderId",
                table: "OrderItem",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Orders_OrderId",
                table: "OrderItem",
                column: "OrderId",
                principalTable: "Orders",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderItem_Products_ProductId",
                table: "OrderItem",
                column: "ProductId",
                principalTable: "Products",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
