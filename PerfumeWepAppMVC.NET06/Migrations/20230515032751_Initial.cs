using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PerfumeWepAppMVC.NET06.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Category_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Category_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Category_ID);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Product_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Category_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Product_Name = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Product_Price = table.Column<int>(type: "int", nullable: false),
                    Product_Origin = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Product_Gender = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Product_Style = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Product_ReleaseYear = table.Column<int>(type: "int", nullable: false),
                    Product_Volume = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Product_IsNew = table.Column<bool>(type: "bit", nullable: false),
                    Product_IsRecommend = table.Column<bool>(type: "bit", nullable: false),
                    Product_IsTrending = table.Column<bool>(type: "bit", nullable: false),
                    Product_Intro = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_Products_Categories_Category_ID",
                        column: x => x.Category_ID,
                        principalTable: "Categories",
                        principalColumn: "Category_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductSpecs",
                columns: table => new
                {
                    Product_ID = table.Column<string>(type: "nvarchar(5)", maxLength: 5, nullable: false),
                    Product_Des = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: false),
                    Concentration = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Fragrance_Group = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Longevity = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: false),
                    Sillage = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Top_Note = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Middle_Note = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Base_Note = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: false),
                    Right_Time = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Recommended_Age = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductSpecs", x => x.Product_ID);
                    table.ForeignKey(
                        name: "FK_ProductSpecs_Products_Product_ID",
                        column: x => x.Product_ID,
                        principalTable: "Products",
                        principalColumn: "Product_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_ID",
                table: "Products",
                column: "Category_ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductSpecs");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Categories");
        }
    }
}
