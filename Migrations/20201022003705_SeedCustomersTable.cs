using Microsoft.EntityFrameworkCore.Migrations;

namespace BlindLowVisionProject.Migrations
{
    public partial class SeedCustomersTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 1, 2, "nikhil@gmail.com", "Nikhil" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
