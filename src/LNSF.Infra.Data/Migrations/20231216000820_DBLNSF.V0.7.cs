using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV07 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "586b22b9-bf85-4d1f-b7ae-11eac16cc9a3");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "d185422f-b039-4701-b306-be491c9ca978");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "0a5ab808-d8fc-4d56-b03e-8ede5177105d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "df691bc0-0525-4221-8c59-3a1497cccd13");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "e408633f-9a91-4c2e-a5e2-203695b376d7");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "16c404ef-64db-41f9-b246-48bcf5552969", "AQAAAAIAAYagAAAAEEthRfYKzlvxcXVURojClgyAb6KHFtNG5zqiDM4bhiMAtU6b8V3OViRWGaXSfsIYcw==", "93a6584d-07f1-400e-a959-0fe0e6cb2a00" });

            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[,]
                {
                    { "2", 0, "25546e0a-bba0-4aa5-a3ee-3ce00c7c7f13", "lnsf@gmail.com", false, false, null, "LNSF@GMAIL.COM", "LNSF", "AQAAAAIAAYagAAAAEHlZ2mwH2IOnqY3pM5lpgVQnU7PHvM6EJaCmXYuvcATycC6s5fwjx+NZIxwn3yWn9A==", "(11) 11 1 1111-1111", false, "478c7c0f-1342-4a65-ba47-44a0d4e253ac", false, "lnsf" },
                    { "3", 0, "95cc53b0-8ca0-4bf8-971e-512b23a7b7e0", "lnsf2@gmail.com", false, false, null, "LNSF2@GMAIL.COM", "LNSF2", "AQAAAAIAAYagAAAAEIVZ9qq1KANd6Q/gx83GQ1l2VYKb/j4SjbeG9IAQUuZzVP56ZCGYnfKHYOhmB3eECg==", "(22) 22 2 2222-2222", false, "e1ee8b88-4383-4dbd-a2c2-590023535004", false, "lnsf2" }
                });

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "2" },
                    { "5", "3" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "2" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "5", "3" });

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2");

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3");

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
    }
}
