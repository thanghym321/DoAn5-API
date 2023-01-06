using Microsoft.EntityFrameworkCore.Migrations;

namespace DoAn5.DataContext.Migrations
{
    public partial class update05012023 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Import_Invoice_Details");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Export_Invoice_Details");

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Import_Invoice_Details",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Export_Invoice_Details",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Import_Invoice_Details");

            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Export_Invoice_Details");

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Import_Invoice_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Amount",
                table: "Export_Invoice_Details",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
