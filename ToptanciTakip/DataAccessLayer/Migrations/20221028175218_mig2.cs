using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "StockCategories",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_StockCategories_AppUserId",
                table: "StockCategories",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_StockCategories_AspNetUsers_AppUserId",
                table: "StockCategories",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_StockCategories_AspNetUsers_AppUserId",
                table: "StockCategories");

            migrationBuilder.DropIndex(
                name: "IX_StockCategories_AppUserId",
                table: "StockCategories");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "StockCategories");
        }
    }
}
