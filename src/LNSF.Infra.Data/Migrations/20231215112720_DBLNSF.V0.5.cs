using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV05 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "3ea1c0e0-4068-47ea-8cd4-bd7a6490775a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "dd6f7da7-94ef-4697-8810-5843cd7c91fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "819911d0-fc1d-4c49-8844-eae516d2a959");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "be201744-6a22-4186-834e-9915e92ed7b0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "64b98556-fe11-4881-9686-4126d67f4729");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "a9fb5d1f-8ae6-4b3f-a74b-d0f3f6203f95", "GEORGEDEV", "AQAAAAIAAYagAAAAEOEJYeUclK2xs8hlgZFcd8O4Xjkylvfa+O/f4ot0qd4EzKa+vDBvoZmqy1K3DmpVDw==", "68a474ca-8583-4ef6-b563-60448c432349", "georgedev" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "8e0a0f1d-547d-43e3-8cd8-0091809fe1aa", "GEORGEMAIA", "AQAAAAIAAYagAAAAEL2SNwWRxqNldeHoTo8ELV7GeIaWVv68EUmy/xReNVZiOBM+Pb9Vt3Cwq8Z/nfiavA==", "a798b806-cba5-4b2f-b711-dd34ecfe9675", "dev" });
        }
    }
}
