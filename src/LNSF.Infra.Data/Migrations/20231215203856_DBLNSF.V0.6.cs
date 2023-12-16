using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV06 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "e44fb29a-6b00-465d-88e7-44812c9f76e0");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "cbe361d0-341e-44ab-a5d1-b528d8087d2a");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "c2f731b6-8ca9-4dce-b4f6-f34ed6e0ccb4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "a988dcd2-7852-4884-8825-fb0635e0be92");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "932d6b5f-9660-49d8-a4ca-9927f9bc10cd");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0fd91294-6052-493e-bfd1-f4c63f5d7dd3", "AQAAAAIAAYagAAAAEFb3VSmjqds2BsBrbwRK3jNjntB2FVogLPwhz6s647tW196K0EEljfkq+m/y+p/aSg==", "59f2c5d0-1f52-4349-8a1c-f1e37f89a835" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "a9fb5d1f-8ae6-4b3f-a74b-d0f3f6203f95", "AQAAAAIAAYagAAAAEOEJYeUclK2xs8hlgZFcd8O4Xjkylvfa+O/f4ot0qd4EzKa+vDBvoZmqy1K3DmpVDw==", "68a474ca-8583-4ef6-b563-60448c432349" });
        }
    }
}
