using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV02 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "5b145b38-742f-48b2-ab86-5fc2c0f847e9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "8206759b-7a98-4b98-a280-482a2e44dc59");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "dec7fe9a-38de-4fd9-9e60-31e9c178b079");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "da47eebf-11eb-4fa4-95df-ca3662a7c12e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "16623bfe-c780-4022-988a-011108928f8e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "69a7bce9-bdd8-42b6-b08c-be9488e35288");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "1", 0, "fa0d110e-4eae-4e55-861b-80af4da30f44", "georgemaiaf@gmail.com", false, false, null, "GEORGEMAIAF@GMAIL.COM", "GEORGEMAIA", "AQAAAAIAAYagAAAAEOf5PY/9LaSSD7LzJqptN665Dng6fMtFPxueeFzWwOVzX3xTWgyo8U9Q2IvRLzgS/w==", "(55) 88 9 9246-5315", false, "9f769f4f-2d53-4048-b7f8-fa92475e53c4", false, "GeorgeMaia" });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[] { "1", "1" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "1", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3d4387f8-4576-4ffc-b399-43e77f3ca5c0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "7cad6bda-74d6-4975-840c-0d1c2957a1a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "b70416d8-d365-4bad-ae42-aab0240c8b5a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "ad570c03-8b34-4ac8-a7d5-c6b559c1b324");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "0f032caa-760b-4ac0-a6c7-e7785b1c182f");

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { "5b145b38-742f-48b2-ab86-5fc2c0f847e9", 0, "43a2ad87-90d3-4307-865d-21673fd0d734", "georgemaiaf@gmail.com", false, false, null, "GEORGEMAIAF@GMAIL.COM", "GEORGEMAIA", "AQAAAAIAAYagAAAAEM90wu7FmjzOpm00k34QrtyDRlxwSLR45sEJj5b3qQbSWqGwJ71ylFL0jLhTZLC+Aw==", "(55) 88 9 9246-5315", false, "e505dad0-9d5c-4772-9dd9-69317950677a", false, "George Maia" });
        }
    }
}
