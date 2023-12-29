using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV09 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "091d4f47-19da-46d6-9d62-17c2a70650f9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "a1639c63-150f-4e08-ad47-f2b7175d5759");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "e630881c-f652-439d-87a7-9836ce466a8b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "2ccf053e-94b4-4f35-9c75-a66d202b1826");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "22d326a6-60ca-4460-85fd-27b68276a33d");

            migrationBuilder.InsertData(
                table: "AspNetUserRoles",
                columns: new[] { "RoleId", "UserId" },
                values: new object[,]
                {
                    { "2", "1" },
                    { "3", "1" }
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3f7332de-cfa2-400a-9b8d-60a47ce59424", "AQAAAAIAAYagAAAAEKP/KR9t0v2tuUybcr9wKJzJL39E8Jba1CahRVYehkfk8I7zhEl2VHdBJJoqq0JZPQ==", "2a38c57f-43e2-438a-a005-56e766ce2421" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "530bc4a7-eb81-4327-b577-2a7eb1d5a876", "AQAAAAIAAYagAAAAEKiUCAxT+KCRvH1VmWx6OUELObs4Wad5kNrOf0kTIbIJoyS6p/f6VH4eVUkBTnydDA==", "fddbf082-1be8-4b1d-bdf9-88c5f755ea29" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "303d201d-5aeb-4a35-8561-0dc55eee421d", "AQAAAAIAAYagAAAAED1qzu9ddfKYgSvs0et1OEClDqKPPb9x5fhEOdILuNUCnm9HcCWWr9LkJyHIHO8/ig==", "4bb6069c-a52f-412c-8a56-3dc029d55e97" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "2", "1" });

            migrationBuilder.DeleteData(
                table: "AspNetUserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { "3", "1" });

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

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "25546e0a-bba0-4aa5-a3ee-3ce00c7c7f13", "AQAAAAIAAYagAAAAEHlZ2mwH2IOnqY3pM5lpgVQnU7PHvM6EJaCmXYuvcATycC6s5fwjx+NZIxwn3yWn9A==", "478c7c0f-1342-4a65-ba47-44a0d4e253ac" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "95cc53b0-8ca0-4bf8-971e-512b23a7b7e0", "AQAAAAIAAYagAAAAEIVZ9qq1KANd6Q/gx83GQ1l2VYKb/j4SjbeG9IAQUuZzVP56ZCGYnfKHYOhmB3eECg==", "e1ee8b88-4383-4dbd-a2c2-590023535004" });
        }
    }
}
