using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagement.Migrations
{
    /// <inheritdoc />
    public partial class SecondMIgrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "521e2785-e0cb-4081-b6eb-a46d4d06b980");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d33894c0-5d30-4f86-bf77-dcc1779aa003");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8114dd03-0862-4c34-8b8f-c63ae28e8611", "1", "Admin", "Admin" },
                    { "e20cebc8-bcb2-4357-a397-44d1c4627b24", "2", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8114dd03-0862-4c34-8b8f-c63ae28e8611");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e20cebc8-bcb2-4357-a397-44d1c4627b24");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "521e2785-e0cb-4081-b6eb-a46d4d06b980", "1", "Admin", "Admin" },
                    { "d33894c0-5d30-4f86-bf77-dcc1779aa003", "2", "User", "User" }
                });
        }
    }
}
