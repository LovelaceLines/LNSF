using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV04 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "688a9e58-f27f-40f8-b4f1-df092bd4b3b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "952ee9f1-9ca1-4052-b887-23befa3613ac");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "513dd5db-9cdd-4cf8-8d04-62de855e0cf1");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "3a7b4892-ea60-4212-a24d-3bb4371a36f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "647be897-6cce-407d-a235-aa05881ddbbe");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8e0a0f1d-547d-43e3-8cd8-0091809fe1aa", "AQAAAAIAAYagAAAAEL2SNwWRxqNldeHoTo8ELV7GeIaWVv68EUmy/xReNVZiOBM+Pb9Vt3Cwq8Z/nfiavA==", "a798b806-cba5-4b2f-b711-dd34ecfe9675", "dev" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "fa0d110e-4eae-4e55-861b-80af4da30f44", "AQAAAAIAAYagAAAAEOf5PY/9LaSSD7LzJqptN665Dng6fMtFPxueeFzWwOVzX3xTWgyo8U9Q2IvRLzgS/w==", "9f769f4f-2d53-4048-b7f8-fa92475e53c4", "GeorgeMaia" });
        }
    }
}
