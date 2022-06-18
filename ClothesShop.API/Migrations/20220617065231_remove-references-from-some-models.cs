using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ClotheShop.API.Migrations
{
    public partial class removereferencesfromsomemodels : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Clothes_Categories_CategoryID",
                table: "Clothes");

            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clothes_ClotheID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Clothes_ClotheID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ClotheID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClotheID",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Clothes_CategoryID",
                table: "Clothes");

            migrationBuilder.DropColumn(
                name: "CategoryID",
                table: "Clothes");

            migrationBuilder.AddColumn<int>(
                name: "ClothesID",
                table: "Rating",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ClothesID",
                table: "Images",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ClothesID",
                table: "Rating",
                column: "ClothesID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClothesID",
                table: "Images",
                column: "ClothesID");

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Clothes_ClothesID",
                table: "Images",
                column: "ClothesID",
                principalTable: "Clothes",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Clothes_ClothesID",
                table: "Rating",
                column: "ClothesID",
                principalTable: "Clothes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Images_Clothes_ClothesID",
                table: "Images");

            migrationBuilder.DropForeignKey(
                name: "FK_Rating_Clothes_ClothesID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Rating_ClothesID",
                table: "Rating");

            migrationBuilder.DropIndex(
                name: "IX_Images_ClothesID",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ClothesID",
                table: "Rating");

            migrationBuilder.DropColumn(
                name: "ClothesID",
                table: "Images");

            migrationBuilder.AddColumn<int>(
                name: "CategoryID",
                table: "Clothes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Rating_ClotheID",
                table: "Rating",
                column: "ClotheID");

            migrationBuilder.CreateIndex(
                name: "IX_Images_ClotheID",
                table: "Images",
                column: "ClotheID");

            migrationBuilder.CreateIndex(
                name: "IX_Clothes_CategoryID",
                table: "Clothes",
                column: "CategoryID");

            migrationBuilder.AddForeignKey(
                name: "FK_Clothes_Categories_CategoryID",
                table: "Clothes",
                column: "CategoryID",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Images_Clothes_ClotheID",
                table: "Images",
                column: "ClotheID",
                principalTable: "Clothes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rating_Clothes_ClotheID",
                table: "Rating",
                column: "ClotheID",
                principalTable: "Clothes",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
