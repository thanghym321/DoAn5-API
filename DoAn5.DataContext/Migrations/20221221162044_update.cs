using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn5.DataContext.Migrations
{
    public partial class update : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Export_Invoices_Users_User_Id",
                table: "Export_Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Export_Invoices_User_Id",
                table: "Export_Invoices");

            migrationBuilder.DropColumn(
                name: "Total",
                table: "Export_Invoices");

            migrationBuilder.DropColumn(
                name: "User_Id",
                table: "Export_Invoices");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Export_Invoices",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Export_Invoices");

            migrationBuilder.AddColumn<decimal>(
                name: "Total",
                table: "Export_Invoices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<int>(
                name: "User_Id",
                table: "Export_Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Export_Invoices_User_Id",
                table: "Export_Invoices",
                column: "User_Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Export_Invoices_Users_User_Id",
                table: "Export_Invoices",
                column: "User_Id",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
