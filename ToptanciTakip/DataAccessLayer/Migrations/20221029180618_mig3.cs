using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class mig3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AppUserId",
                table: "Stocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Stocks_AppUserId",
                table: "Stocks",
                column: "AppUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stocks_AspNetUsers_AppUserId",
                table: "Stocks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stocks_AspNetUsers_AppUserId",
                table: "Stocks");

            migrationBuilder.DropIndex(
                name: "IX_Stocks_AppUserId",
                table: "Stocks");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Stocks");
        }
    }
}
