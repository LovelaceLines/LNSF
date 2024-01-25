using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.src.LNSF.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class DBLNSFV03 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaritalStatus",
                table: "Peoples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RaceColor",
                table: "Peoples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "db347720-5fb2-4b30-b4ce-253ba1814234");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "e4cbf7e5-8903-43ed-a641-e19e71e86b05");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "2ed509b9-b93c-4839-8578-e7ca0582b86e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "bc6e6dfa-e59c-45af-b6a7-68ec3c0fb9c9");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "6709647e-956f-4a7b-9a38-6d116237ae84");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "63c885be-3b38-45ab-b81d-4cd2793cb479", "AQAAAAIAAYagAAAAEItDuqLJsQ9ciP/kDXv6y7FTKVam7K31HxFVz8YX+F6jynFvI2XFvu4TWNp+xBU9Og==", "9ce2cbf1-ea81-4a16-8b35-063ca385d4dd" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d6d07e7c-a96e-4058-84ab-6cce750bdbe7", "AQAAAAIAAYagAAAAEDmDGvG0T526OybwK+LCeSY9GZVuNUNa9EBmVZpofLXmkDLab2fEqv5M5p6XSlMMGA==", "934a7111-cdce-4783-a8ea-21662a4e257e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2681a793-2e4c-45be-aa32-5dde9e20ce72", "AQAAAAIAAYagAAAAENQZ1Zb5Jgoxpy6IKds3wcFBv2/yqFmXRbFMlxdYUPaemyk9QN8g3zELL3EQ2t2g6w==", "d7043f4f-e6b1-4ff7-a6a8-0822822be810" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "RaceColor",
                table: "Peoples");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "dfcf4dee-2d4f-49fb-97e0-6a721adb06fe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "50a6d35f-7765-4a51-8e11-f2ca075df885");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "65175c0f-5cfd-4217-b31e-687ccd84a20b");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "13f0dfe6-7bf9-4193-a1e0-81beecbaf62e");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "b9488930-7bb9-41aa-acfa-b30b3e4cca99");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "699053af-3068-4c03-bb62-b7fa4b47be7d", "AQAAAAIAAYagAAAAEAJWhw7eb3ZKjmfSCp5ZGLfmcDYkNACE2zglCQHgzfQUqAHqK3kYFh0zd/ktyQPRyA==", "391f5300-6337-4f93-a108-aa77f867f11e" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "47bc1a23-69a7-4b28-b5ab-335cae8910c5", "AQAAAAIAAYagAAAAEHvNWSiLtU8RyxRtmatiXINsFNwlXw08TXwFvOepKdy4DZBANTEmvYlRh1/nLY4kWg==", "9c9229b9-12e0-46b8-a864-2eaa7de71f3c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2dc9eead-3af0-4cec-bf59-1e99f2440d07", "AQAAAAIAAYagAAAAEGw0Sm6LU8fiRsu5hSdZzaoPLSVudKXVmxwE0gk1I83kdCBeG0T1LDsIKptZMoKlXg==", "e3434693-6603-476f-948c-7a34aa1b28ed" });
        }
    }
}
