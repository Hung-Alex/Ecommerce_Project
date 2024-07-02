using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updatecategory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategories_SubCategories_SubCategoryId",
                table: "ProductSubCategories");

            migrationBuilder.DropTable(
                name: "SubCategories");

            migrationBuilder.RenameColumn(
                name: "SubCategoryId",
                table: "ProductSubCategories",
                newName: "CategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategories_SubCategoryId",
                table: "ProductSubCategories",
                newName: "IX_ProductSubCategories_CategoryId");

            migrationBuilder.AddColumn<Guid>(
                name: "ParrentId",
                table: "Categories",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Categories_ParrentId",
                table: "Categories",
                column: "ParrentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_Categories_ParrentId",
                table: "Categories",
                column: "ParrentId",
                principalTable: "Categories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategories_Categories_CategoryId",
                table: "ProductSubCategories",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Categories_Categories_ParrentId",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_ProductSubCategories_Categories_CategoryId",
                table: "ProductSubCategories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_ParrentId",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "ParrentId",
                table: "Categories");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "ProductSubCategories",
                newName: "SubCategoryId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductSubCategories_CategoryId",
                table: "ProductSubCategories",
                newName: "IX_ProductSubCategories_SubCategoryId");

            migrationBuilder.CreateTable(
                name: "SubCategories",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CategoryId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    UrlSlug = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SubCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SubCategories_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SubCategories_CategoryId",
                table: "SubCategories",
                column: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductSubCategories_SubCategories_SubCategoryId",
                table: "ProductSubCategories",
                column: "SubCategoryId",
                principalTable: "SubCategories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
