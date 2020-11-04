using Microsoft.EntityFrameworkCore.Migrations;

namespace BlindLowVisionProject.Migrations
{
    public partial class AlterCustomersSeedData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Department", "Email", "Name" },
                values: new object[] { 2, 1, "DrRohanP@gmail.com", "DrRohanPatel" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
