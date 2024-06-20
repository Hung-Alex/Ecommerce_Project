using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class seedrole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c01"), null, "SupperAdmin", null },
                    { new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c02"), null, "User", null },
                    { new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c03"), null, "Employee", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c01"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c02"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("911b0cbd-4eed-4eb0-8488-1b2cdd915c03"));
        }
    }
}
