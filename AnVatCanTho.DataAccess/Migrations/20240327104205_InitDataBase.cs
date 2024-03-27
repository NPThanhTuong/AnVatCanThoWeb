using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnVatCanTho.DataAccess.Migrations
{
    public partial class InitDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tel = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    Display_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Districts",
                columns: table => new
                {
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Districts", x => x.Name);
                });

            migrationBuilder.CreateTable(
                name: "Product_Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Types", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    Permission = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Snack_Bars",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tel = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    CoverImage = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Display_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Snack_Bars", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wards",
                columns: table => new
                {
                    District_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wards", x => new { x.District_Name, x.Name });
                    table.UniqueConstraint("AK_Wards_Name_District_Name", x => new { x.Name, x.District_Name });
                    table.ForeignKey(
                        name: "FK_Wards_Districts_District_Name",
                        column: x => x.District_Name,
                        principalTable: "Districts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductType_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Categories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Categories_Product_Types_ProductType_Id",
                        column: x => x.ProductType_Id,
                        principalTable: "Product_Types",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Administrators",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<int>(type: "int", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Tel = table.Column<string>(type: "char(10)", unicode: false, fixedLength: true, maxLength: 10, nullable: false),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Avatar = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(512)", maxLength: 512, nullable: false),
                    DOB = table.Column<DateTime>(type: "date", nullable: false),
                    City = table.Column<string>(type: "nvarchar(40)", maxLength: 40, nullable: false),
                    Display_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Administrators", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Administrators_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Bills",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Total = table.Column<int>(type: "int", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bills", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bills_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Addresses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    District_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    DistrictName = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Ward_Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    NoAndStreet = table.Column<string>(type: "nvarchar(128)", maxLength: 128, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Addresses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Addresses_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Districts_DistrictName",
                        column: x => x.DistrictName,
                        principalTable: "Districts",
                        principalColumn: "Name",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Addresses_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Addresses_Wards_Ward_Name_District_Name",
                        columns: x => new { x.Ward_Name, x.District_Name },
                        principalTable: "Wards",
                        principalColumns: new[] { "Name", "District_Name" });
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    ProductCategory_Id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Description = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Ingredient = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    Stock = table.Column<int>(type: "int", nullable: false),
                    UnitPrice = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => new { x.SnackBar_Id, x.Id });
                    table.UniqueConstraint("AK_Products_Id_SnackBar_Id", x => new { x.Id, x.SnackBar_Id });
                    table.ForeignKey(
                        name: "FK_Products_Product_Categories_ProductCategory_Id",
                        column: x => x.ProductCategory_Id,
                        principalTable: "Product_Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Comments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(1024)", maxLength: 1024, nullable: false),
                    LikeQuantity = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Comments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Comments_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Comments_Products_Product_Id_SnackBar_Id",
                        columns: x => new { x.Product_Id, x.SnackBar_Id },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "SnackBar_Id" });
                    table.ForeignKey(
                        name: "FK_Comments_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Order_Details",
                columns: table => new
                {
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Order_Id = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Price = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Order_Details", x => new { x.SnackBar_Id, x.Product_Id, x.Order_Id });
                    table.ForeignKey(
                        name: "FK_Order_Details_Orders_Order_Id",
                        column: x => x.Order_Id,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Order_Details_Products_Product_Id_SnackBar_Id",
                        columns: x => new { x.Product_Id, x.SnackBar_Id },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "SnackBar_Id" });
                    table.ForeignKey(
                        name: "FK_Order_Details_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Images",
                columns: table => new
                {
                    Path_Name = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false),
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Images", x => x.Path_Name);
                    table.ForeignKey(
                        name: "FK_Product_Images_Products_Product_Id_SnackBar_Id",
                        columns: x => new { x.Product_Id, x.SnackBar_Id },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "SnackBar_Id" },
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Product_Images_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    SnackBar_Id = table.Column<int>(type: "int", nullable: false),
                    Product_Id = table.Column<int>(type: "int", nullable: false),
                    Customer_Id = table.Column<int>(type: "int", nullable: false),
                    Star = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => new { x.SnackBar_Id, x.Product_Id, x.Customer_Id });
                    table.ForeignKey(
                        name: "FK_Ratings_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Ratings_Products_Product_Id_SnackBar_Id",
                        columns: x => new { x.Product_Id, x.SnackBar_Id },
                        principalTable: "Products",
                        principalColumns: new[] { "Id", "SnackBar_Id" });
                    table.ForeignKey(
                        name: "FK_Ratings_Snack_Bars_SnackBar_Id",
                        column: x => x.SnackBar_Id,
                        principalTable: "Snack_Bars",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ADDRESS_PK",
                table: "Addresses",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AddressBelongToWard_FK",
                table: "Addresses",
                columns: new[] { "District_Name", "Ward_Name" });

            migrationBuilder.CreateIndex(
                name: "CustomerHasAddress_FK",
                table: "Addresses",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_DistrictName",
                table: "Addresses",
                column: "DistrictName");

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_Ward_Name_District_Name",
                table: "Addresses",
                columns: new[] { "Ward_Name", "District_Name" });

            migrationBuilder.CreateIndex(
                name: "SnackBarHasAddress_FK",
                table: "Addresses",
                column: "SnackBar_Id");

            migrationBuilder.CreateIndex(
                name: "ADMINISTRATOR_PK",
                table: "Administrators",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "AdministratorHasRole_FK",
                table: "Administrators",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "BILL_PK",
                table: "Bills",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "BillOfOrder_FK",
                table: "Bills",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "COMMENT_PK",
                table: "Comments",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "CommentOfProduct_FK",
                table: "Comments",
                columns: new[] { "SnackBar_Id", "Product_Id" });

            migrationBuilder.CreateIndex(
                name: "CustomerHasComment_FK",
                table: "Comments",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Comments_Product_Id_SnackBar_Id",
                table: "Comments",
                columns: new[] { "Product_Id", "SnackBar_Id" });

            migrationBuilder.CreateIndex(
                name: "CUSTOMER_PK",
                table: "Customers",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "DISTRICT_PK",
                table: "Districts",
                column: "Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_Details_Product_Id_SnackBar_Id",
                table: "Order_Details",
                columns: new[] { "Product_Id", "SnackBar_Id" });

            migrationBuilder.CreateIndex(
                name: "OrderDetailOfOrder_FK",
                table: "Order_Details",
                column: "Order_Id");

            migrationBuilder.CreateIndex(
                name: "OrderDetailOfProduct_FK",
                table: "Order_Details",
                columns: new[] { "SnackBar_Id", "Product_Id" });

            migrationBuilder.CreateIndex(
                name: "CustomerHasOrders_FK",
                table: "Orders",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "ORDER_PK",
                table: "Orders",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PRODUCT_CATEGORY_PK",
                table: "Product_Categories",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ProductCategoryOfProductType_FK",
                table: "Product_Categories",
                column: "ProductType_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_Product_Id_SnackBar_Id",
                table: "Product_Images",
                columns: new[] { "Product_Id", "SnackBar_Id" });

            migrationBuilder.CreateIndex(
                name: "PRODUCT_IMAGE_PK",
                table: "Product_Images",
                column: "Path_Name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ProductHasImage_FK",
                table: "Product_Images",
                columns: new[] { "SnackBar_Id", "Product_Id" });

            migrationBuilder.CreateIndex(
                name: "PRODUCT_TYPE_PK",
                table: "Product_Types",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "PRODUCT_PK",
                table: "Products",
                columns: new[] { "SnackBar_Id", "Id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ProductOfProductCategory_FK",
                table: "Products",
                column: "ProductCategory_Id");

            migrationBuilder.CreateIndex(
                name: "DetailRatingOfCustomer_FK",
                table: "Ratings",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "DetailRatingofProduct_FK",
                table: "Ratings",
                columns: new[] { "Product_Id", "SnackBar_Id" });

            migrationBuilder.CreateIndex(
                name: "ROLE_PK",
                table: "Roles",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "SNACK_BAR_PK",
                table: "Snack_Bars",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "WARD_PK",
                table: "Wards",
                columns: new[] { "District_Name", "Name" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Addresses");

            migrationBuilder.DropTable(
                name: "Administrators");

            migrationBuilder.DropTable(
                name: "Bills");

            migrationBuilder.DropTable(
                name: "Comments");

            migrationBuilder.DropTable(
                name: "Order_Details");

            migrationBuilder.DropTable(
                name: "Product_Images");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Wards");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Districts");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Product_Categories");

            migrationBuilder.DropTable(
                name: "Snack_Bars");

            migrationBuilder.DropTable(
                name: "Product_Types");
        }
    }
}
