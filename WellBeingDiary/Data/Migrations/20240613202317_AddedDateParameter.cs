using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WellBeingDiary.Migrations
{
    /// <inheritdoc />
    public partial class AddedDateParameter : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2148c315-3ff7-4b9c-89c4-1a8996f47ec2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "62e65692-1755-4886-b631-b2358232edbc");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DiaryNotes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "00fbce2f-edd0-4195-b9d2-aaf80f73ba20", null, "Admin", "ADMIN" },
                    { "3fb6fbbf-b8d6-41a7-91e1-df749c58f25c", null, "User", "USER" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "00fbce2f-edd0-4195-b9d2-aaf80f73ba20");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3fb6fbbf-b8d6-41a7-91e1-df749c58f25c");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "DiaryNotes");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "2148c315-3ff7-4b9c-89c4-1a8996f47ec2", null, "Admin", "ADMIN" },
                    { "62e65692-1755-4886-b631-b2358232edbc", null, "User", "USER" }
                });
        }
    }
}
