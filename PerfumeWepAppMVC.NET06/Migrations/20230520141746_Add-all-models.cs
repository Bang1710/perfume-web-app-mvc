using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeWepAppMVC.NET06.Migrations
{
    /// <inheritdoc />
    public partial class Addallmodels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReviewProduct",
                columns: table => new
                {
                    Review_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    User_Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    User_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Rating = table.Column<int>(type: "int", nullable: false),
                    ReviewTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Content_Review = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReviewProduct", x => x.Review_ID);
                    table.ForeignKey(
                        name: "FK_ReviewProduct_Products_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Products",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    User_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_Password = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    User_Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    User_PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    User_Address = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.User_ID);
                });

            migrationBuilder.CreateTable(
                name: "Cart",
                columns: table => new
                {
                    Cart_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    User_ID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cart", x => x.Cart_ID);
                    table.ForeignKey(
                        name: "FK_Cart_User_User_ID",
                        column: x => x.User_ID,
                        principalTable: "User",
                        principalColumn: "User_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CartItem",
                columns: table => new
                {
                    CartItems_ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Cart_ID = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Product_Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Product_Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CartItem", x => x.CartItems_ID);
                    table.ForeignKey(
                        name: "FK_CartItem_Cart_Cart_ID",
                        column: x => x.Cart_ID,
                        principalTable: "Cart",
                        principalColumn: "Cart_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CartItem_Products_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Products",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cart_User_ID",
                table: "Cart",
                column: "User_ID",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_Cart_ID",
                table: "CartItem",
                column: "Cart_ID");

            migrationBuilder.CreateIndex(
                name: "IX_CartItem_Product_ID",
                table: "CartItem",
                column: "Product_ID");

            migrationBuilder.CreateIndex(
                name: "IX_ReviewProduct_Product_ID",
                table: "ReviewProduct",
                column: "Product_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CartItem");

            migrationBuilder.DropTable(
                name: "ReviewProduct");

            migrationBuilder.DropTable(
                name: "Cart");

            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
