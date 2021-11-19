using Microsoft.EntityFrameworkCore.Migrations;

namespace HotelListingAPI.Migrations
{
    public partial class AddedDefaultRoles : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "3a213438-cf9f-4a4e-9e25-1783c244414a", "40423ce2-5f10-418c-a3c7-6bf0dbd915d7", "User", "USER" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7fe3777e-c9e6-4040-89ea-ea34bb4fb826", "09663c35-931a-40ff-a7df-cf9d6349cf34", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3a213438-cf9f-4a4e-9e25-1783c244414a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7fe3777e-c9e6-4040-89ea-ea34bb4fb826");
        }
    }
}
