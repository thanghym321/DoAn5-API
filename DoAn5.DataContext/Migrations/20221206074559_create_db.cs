using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn5.DataContext.Migrations
{
    public partial class create_db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0),
                    Image = table.Column<string>(type: "varchar(500)", nullable: true),
                    ParentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Producers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Producers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Providers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Providers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Slides",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(type: "varchar(500)", nullable: true),
                    Link = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Slides", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(1500)", nullable: true),
                    Phone = table.Column<string>(type: "varchar(20)", nullable: true),
                    Email = table.Column<string>(type: "varchar(50)", nullable: true),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Category_Id = table.Column<int>(nullable: false),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: true),
                    Description = table.Column<string>(type: "ntext", nullable: true),
                    Image = table.Column<string>(type: "varchar(500)", nullable: true),
                    Producer_Id = table.Column<int>(nullable: false),
                    Unit_Id = table.Column<int>(nullable: false),
                    Status = table.Column<int>(nullable: false, defaultValue: 0)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_Category_Id",
                        column: x => x.Category_Id,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Producers_Producer_Id",
                        column: x => x.Producer_Id,
                        principalTable: "Producers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Units_Unit_Id",
                        column: x => x.Unit_Id,
                        principalTable: "Units",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Export_Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Export_Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    Customer_Id = table.Column<int>(nullable: false),
                    User_Id = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Export_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Export_Invoices_Customers_Customer_Id",
                        column: x => x.Customer_Id,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Export_Invoices_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Import_Invoices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Import_Date = table.Column<DateTime>(nullable: false, defaultValueSql: "getdate()"),
                    User_Id = table.Column<int>(nullable: false),
                    Provider_Id = table.Column<int>(nullable: false),
                    Total = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_Invoices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Import_Invoices_Providers_Provider_Id",
                        column: x => x.Provider_Id,
                        principalTable: "Providers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Import_Invoices_Users_User_Id",
                        column: x => x.User_Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Images",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(nullable: false),
                    Image = table.Column<string>(type: "varchar(500)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Images", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Images_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Product_Prices",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Product_Prices", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Product_Prices_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Export_Invoice_Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Export_Invoice_Id = table.Column<int>(nullable: false),
                    Product_Id = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Export_Invoice_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Export_Invoice_Details_Export_Invoices_Export_Invoice_Id",
                        column: x => x.Export_Invoice_Id,
                        principalTable: "Export_Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Export_Invoice_Details_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Import_Invoice_Details",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Product_Id = table.Column<int>(nullable: false),
                    Import_Invoice_Id = table.Column<int>(nullable: false),
                    Amount = table.Column<int>(nullable: false),
                    Price = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Import_Invoice_Details", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Import_Invoice_Details_Import_Invoices_Import_Invoice_Id",
                        column: x => x.Import_Invoice_Id,
                        principalTable: "Import_Invoices",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Import_Invoice_Details_Products_Product_Id",
                        column: x => x.Product_Id,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Export_Invoice_Details_Export_Invoice_Id",
                table: "Export_Invoice_Details",
                column: "Export_Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Export_Invoice_Details_Product_Id",
                table: "Export_Invoice_Details",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Export_Invoices_Customer_Id",
                table: "Export_Invoices",
                column: "Customer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Export_Invoices_User_Id",
                table: "Export_Invoices",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Import_Invoice_Details_Import_Invoice_Id",
                table: "Import_Invoice_Details",
                column: "Import_Invoice_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Import_Invoice_Details_Product_Id",
                table: "Import_Invoice_Details",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Import_Invoices_Provider_Id",
                table: "Import_Invoices",
                column: "Provider_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Import_Invoices_User_Id",
                table: "Import_Invoices",
                column: "User_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Images_Product_Id",
                table: "Product_Images",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Product_Prices_Product_Id",
                table: "Product_Prices",
                column: "Product_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Category_Id",
                table: "Products",
                column: "Category_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Producer_Id",
                table: "Products",
                column: "Producer_Id");

            migrationBuilder.CreateIndex(
                name: "IX_Products_Unit_Id",
                table: "Products",
                column: "Unit_Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Export_Invoice_Details");

            migrationBuilder.DropTable(
                name: "Import_Invoice_Details");

            migrationBuilder.DropTable(
                name: "Product_Images");

            migrationBuilder.DropTable(
                name: "Product_Prices");

            migrationBuilder.DropTable(
                name: "Slides");

            migrationBuilder.DropTable(
                name: "Export_Invoices");

            migrationBuilder.DropTable(
                name: "Import_Invoices");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Providers");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Producers");

            migrationBuilder.DropTable(
                name: "Units");
        }
    }
}
