using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClothesShop.API.Migrations
{
    public partial class UpdateTableRating : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UsersId",
                table: "Ratings",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UsersId",
                table: "Ratings",
                column: "UsersId");

            migrationBuilder.AddForeignKey(
                name: "FK_Ratings_Users_UsersId",
                table: "Ratings",
                column: "UsersId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Ratings_Users_UsersId",
                table: "Ratings");

            migrationBuilder.DropIndex(
                name: "IX_Ratings_UsersId",
                table: "Ratings");

            migrationBuilder.DropColumn(
                name: "UsersId",
                table: "Ratings");
        }
    }
}
