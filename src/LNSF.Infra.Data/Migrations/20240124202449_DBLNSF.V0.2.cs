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
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "65175c0f-5cfd-4217-b31e-687ccd84a20b", "AssistenteSocial" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "13f0dfe6-7bf9-4193-a1e0-81beecbaf62e", "Secretario" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "b9488930-7bb9-41aa-acfa-b30b3e4cca99", "Voluntario" });

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "f088ee92-75bb-4231-b424-6dde72ffce71");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "6baf65ff-c374-456b-bff8-1bb6ed8f16cf");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "62d1bf66-2e39-4cd3-8dc0-97a8f492fb92", "Assistente Social" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "55700132-6049-4701-b766-0a0572f17b2b", "Secretário" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                columns: new[] { "ConcurrencyStamp", "Name" },
                values: new object[] { "f574cee5-02d7-43e2-8cec-0bd1d4945775", "Voluntário" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ef5cfb8-c75f-43b0-b6dd-a269893af758", "AQAAAAIAAYagAAAAEG/CVBwMUAKGJOj6Y6BL3jG8fA9yF6BnJ66ntovBB7qZDf6Q/x7QUCde4SUAb3d5Rw==", "37acc48d-1823-4470-b5b5-088a51a88675" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "180ec830-7270-47f6-bd4c-aedef4bbedbd", "AQAAAAIAAYagAAAAEJgvTcOls83LpJcPkGH3+PhYwyjTyCrbGhBzl2eQmVX3oOpiWlnZfAEbH5IVWIUpew==", "6a7c7a38-d08e-46b5-aa07-1a780669bbe5" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d080845-5438-4560-8019-f20442bdccfa", "AQAAAAIAAYagAAAAED7Imi+jKGTNsswCaC+IRhMD5mFITwsk3TfbxB2S7xa5bb1Bq+ru7qhoZMyXk1Rmpw==", "036827ec-1afc-4e30-83cd-695f966af647" });
        }
    }
}
