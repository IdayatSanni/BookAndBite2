using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAndBite2.Data.Migrations
{
    /// <inheritdoc />
    public partial class cartreview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CartId",
                table: "Reviews",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reviews_CartId",
                table: "Reviews",
                column: "CartId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reviews_Carts_CartId",
                table: "Reviews",
                column: "CartId",
                principalTable: "Carts",
                principalColumn: "CartId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reviews_Carts_CartId",
                table: "Reviews");

            migrationBuilder.DropIndex(
                name: "IX_Reviews_CartId",
                table: "Reviews");

            migrationBuilder.DropColumn(
                name: "CartId",
                table: "Reviews");
        }
    }
}
