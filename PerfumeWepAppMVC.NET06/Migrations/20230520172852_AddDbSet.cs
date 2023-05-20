using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeWepAppMVC.NET06.Migrations
{
    /// <inheritdoc />
    public partial class AddDbSet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Cart_User_User_ID",
                table: "Cart");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Cart_Cart_ID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItem_Products_Product_ID",
                table: "CartItem");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewProduct_Products_Product_ID",
                table: "ReviewProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_User",
                table: "User");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewProduct",
                table: "ReviewProduct");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Cart",
                table: "Cart");

            migrationBuilder.RenameTable(
                name: "User",
                newName: "Users");

            migrationBuilder.RenameTable(
                name: "ReviewProduct",
                newName: "ReviewProducts");

            migrationBuilder.RenameTable(
                name: "CartItem",
                newName: "CartItems");

            migrationBuilder.RenameTable(
                name: "Cart",
                newName: "Carts");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewProduct_Product_ID",
                table: "ReviewProducts",
                newName: "IX_ReviewProducts_Product_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_Product_ID",
                table: "CartItems",
                newName: "IX_CartItems_Product_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItem_Cart_ID",
                table: "CartItems",
                newName: "IX_CartItems_Cart_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Cart_User_ID",
                table: "Carts",
                newName: "IX_Carts_User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Users",
                table: "Users",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewProducts",
                table: "ReviewProducts",
                column: "Review_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems",
                column: "CartItems_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Carts",
                table: "Carts",
                column: "Cart_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Carts_Cart_ID",
                table: "CartItems",
                column: "Cart_ID",
                principalTable: "Carts",
                principalColumn: "Cart_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItems_Products_Product_ID",
                table: "CartItems",
                column: "Product_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_User_ID",
                table: "Carts",
                column: "User_ID",
                principalTable: "Users",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewProducts_Products_Product_ID",
                table: "ReviewProducts",
                column: "Product_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Carts_Cart_ID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_CartItems_Products_Product_ID",
                table: "CartItems");

            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_User_ID",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_ReviewProducts_Products_Product_ID",
                table: "ReviewProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Users",
                table: "Users");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ReviewProducts",
                table: "ReviewProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Carts",
                table: "Carts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CartItems",
                table: "CartItems");

            migrationBuilder.RenameTable(
                name: "Users",
                newName: "User");

            migrationBuilder.RenameTable(
                name: "ReviewProducts",
                newName: "ReviewProduct");

            migrationBuilder.RenameTable(
                name: "Carts",
                newName: "Cart");

            migrationBuilder.RenameTable(
                name: "CartItems",
                newName: "CartItem");

            migrationBuilder.RenameIndex(
                name: "IX_ReviewProducts_Product_ID",
                table: "ReviewProduct",
                newName: "IX_ReviewProduct_Product_ID");

            migrationBuilder.RenameIndex(
                name: "IX_Carts_User_ID",
                table: "Cart",
                newName: "IX_Cart_User_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_Product_ID",
                table: "CartItem",
                newName: "IX_CartItem_Product_ID");

            migrationBuilder.RenameIndex(
                name: "IX_CartItems_Cart_ID",
                table: "CartItem",
                newName: "IX_CartItem_Cart_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_User",
                table: "User",
                column: "User_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ReviewProduct",
                table: "ReviewProduct",
                column: "Review_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Cart",
                table: "Cart",
                column: "Cart_ID");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CartItem",
                table: "CartItem",
                column: "CartItems_ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Cart_User_User_ID",
                table: "Cart",
                column: "User_ID",
                principalTable: "User",
                principalColumn: "User_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Cart_Cart_ID",
                table: "CartItem",
                column: "Cart_ID",
                principalTable: "Cart",
                principalColumn: "Cart_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CartItem_Products_Product_ID",
                table: "CartItem",
                column: "Product_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ReviewProduct_Products_Product_ID",
                table: "ReviewProduct",
                column: "Product_ID",
                principalTable: "Products",
                principalColumn: "Product_ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
