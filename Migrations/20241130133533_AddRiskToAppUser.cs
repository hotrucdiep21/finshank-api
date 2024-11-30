﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace api.Migrations
{
    /// <inheritdoc />
    public partial class AddRiskToAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "56dd8ead-895c-4a03-ab6b-078563fbb319");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cac044a2-f301-4b87-b643-ce77ab67e5fc");

            migrationBuilder.RenameColumn(
                name: "MyProperty",
                table: "AspNetUsers",
                newName: "Risk");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "3b4dbacf-c3d1-4420-be5c-ef5645e03906", null, "User", "USER" },
                    { "f21633da-a37e-477b-8d10-73970f4543b1", null, "Admin", "ADMIN" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3b4dbacf-c3d1-4420-be5c-ef5645e03906");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f21633da-a37e-477b-8d10-73970f4543b1");

            migrationBuilder.RenameColumn(
                name: "Risk",
                table: "AspNetUsers",
                newName: "MyProperty");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "56dd8ead-895c-4a03-ab6b-078563fbb319", null, "Admin", "ADMIN" },
                    { "cac044a2-f301-4b87-b643-ce77ab67e5fc", null, "User", "USER" }
                });
        }
    }
}
