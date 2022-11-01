using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig6 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "MarketAdress",
                table: "ProductRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MarketName",
                table: "ProductRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "MarketPhone",
                table: "ProductRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ProductName",
                table: "ProductRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MarketAdress",
                table: "ProductRequests");

            migrationBuilder.DropColumn(
                name: "MarketName",
                table: "ProductRequests");

            migrationBuilder.DropColumn(
                name: "MarketPhone",
                table: "ProductRequests");

            migrationBuilder.DropColumn(
                name: "ProductName",
                table: "ProductRequests");
        }
    }
}
