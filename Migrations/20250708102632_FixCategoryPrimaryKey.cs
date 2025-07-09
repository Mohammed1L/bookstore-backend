using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BookStoreBackend.Migrations
{
    /// <inheritdoc />
    public partial class FixCategoryPrimaryKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Books",
                newName: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "id",
                table: "Books",
                newName: "Id");
        }
    }
}
