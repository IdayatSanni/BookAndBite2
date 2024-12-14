using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookAndBite2.Data.Migrations
{
    /// <inheritdoc />
    public partial class bookcart : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "BookCart",
                columns: table => new
                {
                    BooksBookId = table.Column<int>(type: "int", nullable: false),
                    CartsCartId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookCart", x => new { x.BooksBookId, x.CartsCartId });
                    table.ForeignKey(
                        name: "FK_BookCart_Books_BooksBookId",
                        column: x => x.BooksBookId,
                        principalTable: "Books",
                        principalColumn: "BookId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BookCart_Carts_CartsCartId",
                        column: x => x.CartsCartId,
                        principalTable: "Carts",
                        principalColumn: "CartId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BookCart_CartsCartId",
                table: "BookCart",
                column: "CartsCartId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BookCart");
        }
    }
}
