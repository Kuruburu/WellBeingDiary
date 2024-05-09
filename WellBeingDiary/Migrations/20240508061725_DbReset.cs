using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WellBeingDiary.Migrations
{
    /// <inheritdoc />
    public partial class DbReset : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "8750841b-ff07-49b9-b013-4cb0b7fd640e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b83ceb85-8d8a-4efb-9e4a-952f4c650e67");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3ea8e2f7-ecb6-494c-babf-f37489ea0071", null, "Admin", "ADMIN" },
                    { "c0d40412-685f-4655-a1c1-906eccf5c861", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3ea8e2f7-ecb6-494c-babf-f37489ea0071");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c0d40412-685f-4655-a1c1-906eccf5c861");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "8750841b-ff07-49b9-b013-4cb0b7fd640e", null, "User", "USER" },
                    { "b83ceb85-8d8a-4efb-9e4a-952f4c650e67", null, "Admin", "ADMIN" }
                });
        }
    }
}
