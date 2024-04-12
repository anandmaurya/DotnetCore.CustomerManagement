using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CustomerManagement.Migrations
{
    /// <inheritdoc />
    public partial class ThirdMIgrations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>("FirstName", "AspNetUsers", "varchar(50)",
        unicode: false, maxLength: 50, nullable: true);
            migrationBuilder.AddColumn<string>("LastName", "AspNetUsers", "varchar(50)",
       unicode: false, maxLength: 50, nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
           
        }
    }
}
