using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MyQuangCoBa.Migrations
{
    /// <inheritdoc />
    public partial class SeedDataForRole : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { new Guid("19b04a7c-ef76-4d28-b016-7c69adc161d2"), null, "Admin", null },
                    { new Guid("e0c2cc7d-f258-428e-99ae-19736a74095f"), null, "User", null }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("19b04a7c-ef76-4d28-b016-7c69adc161d2"));

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: new Guid("e0c2cc7d-f258-428e-99ae-19736a74095f"));
        }
    }
}
