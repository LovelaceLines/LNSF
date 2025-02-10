using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.Migrations
{
    /// <inheritdoc />
    public partial class Init_People : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "RaceColor",
                table: "Peoples",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "RG",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "Neighborhood",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatus",
                table: "Peoples",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "IssuingBody",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Peoples",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Peoples",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateOnly),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "afc726c1-3ecc-4695-aff1-68ed4327f15e", new DateTime(2024, 11, 18, 13, 46, 16, 16, DateTimeKind.Local).AddTicks(9143) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "15d82fd3-cae3-4664-8c11-d0b3b784dca2", new DateTime(2024, 11, 18, 13, 46, 16, 16, DateTimeKind.Local).AddTicks(9163) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "695c4371-4c26-49c8-8aa3-600c317df4d6", new DateTime(2024, 11, 18, 13, 46, 16, 16, DateTimeKind.Local).AddTicks(9178) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "428fcd73-4c7a-4143-aaf4-0300aa046bb2", new DateTime(2024, 11, 18, 13, 46, 16, 16, DateTimeKind.Local).AddTicks(9182) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "214e0968-2907-4ead-8ff1-667335911469", new DateTime(2024, 11, 18, 13, 46, 16, 16, DateTimeKind.Local).AddTicks(9192) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2ee36274-1bae-4842-b5d1-4244e20340e4", new DateTime(2024, 11, 18, 13, 46, 16, 78, DateTimeKind.Local).AddTicks(2007), "AQAAAAIAAYagAAAAEKvmFdw7eeqsiyaAF/c4+AaLMvsSze0tmrfuzSIk8a8k3KL+mnFzIenlU4GQnwT5MQ==", "86360908-1c7b-4c13-b70b-cbabe57bb364" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "92fd5ca4-ce11-4edb-b791-02d698a95b9b", new DateTime(2024, 11, 18, 13, 46, 16, 139, DateTimeKind.Local).AddTicks(3048), "AQAAAAIAAYagAAAAEJEpslWFc9O+B4ofiWJh90le5k6v9yOpbIdbFmzdG8jzcXP2anKFnT1It4w/P5TK1w==", "75c64b07-7e09-46cd-9c4e-c7b7ac64b88b" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4e1a3dfe-c65b-4e79-a03b-3016295206c7", new DateTime(2024, 11, 18, 13, 46, 16, 201, DateTimeKind.Local).AddTicks(5534), "AQAAAAIAAYagAAAAEJWUzTgWxgPStkA1V7iqWcH0ksSpmuQVKSGsmA/CCH7+NXK+O9mvfcadho8y9WPbuQ==", "322a92ed-4926-4c48-a901-055478c06c05" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "132ab906-82e8-44ef-bfd4-02d8ac078b06", new DateTime(2024, 11, 18, 13, 46, 16, 263, DateTimeKind.Local).AddTicks(4249), "AQAAAAIAAYagAAAAEFlIuiKeG6jnBU2UMy8BFXspgddgwuvmfvMtEvhDzZ4Dj6eqkbSzEwvsOjJR4f3WSA==", "5cc2a40e-e5ef-44fd-b791-1e1ba5ec19d0" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "50a5388a-f2ea-49ee-9d2e-77e21d7fae8a", new DateTime(2024, 11, 18, 13, 46, 16, 324, DateTimeKind.Local).AddTicks(7308), "AQAAAAIAAYagAAAAECeQazfTB+5We25BciB3RfeCCMojsCrYMEXGm9uxDXAhRCdKirE5siz5pZ0btBw2BA==", "7ed1aacf-3b44-40f0-bab4-72855c772715" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Street",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "State",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "RaceColor",
                table: "Peoples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RG",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Neighborhood",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "MaritalStatus",
                table: "Peoples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "IssuingBody",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HouseNumber",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Gender",
                table: "Peoples",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "City",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CPF",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateOnly>(
                name: "BirthDate",
                table: "Peoples",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1),
                oldClrType: typeof(DateOnly),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "5338cce6-69c1-4d23-8b06-830cec06c955", new DateTime(2024, 11, 14, 16, 34, 22, 172, DateTimeKind.Local).AddTicks(1665) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "cccaced1-949f-4d3f-98a9-967c2cdeff44", new DateTime(2024, 11, 14, 16, 34, 22, 172, DateTimeKind.Local).AddTicks(1684) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "3a3acd80-4297-4a8b-b8ae-c5e88298282e", new DateTime(2024, 11, 14, 16, 34, 22, 172, DateTimeKind.Local).AddTicks(1688) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "4d56b4b5-e6c1-416d-bcc9-91bfc6181d58", new DateTime(2024, 11, 14, 16, 34, 22, 172, DateTimeKind.Local).AddTicks(1693) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "3dca0a05-332b-4a0e-b1cd-54ba58503c67", new DateTime(2024, 11, 14, 16, 34, 22, 172, DateTimeKind.Local).AddTicks(1706) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1f7b8932-49f8-4579-ad50-f57b67b64c61", new DateTime(2024, 11, 14, 16, 34, 22, 234, DateTimeKind.Local).AddTicks(3365), "AQAAAAIAAYagAAAAEH6qqSlgsEVPfJa4lEr/bMWWPwZGDLPygzRQqG8muCDPiC5mlIUVj3WPrpFk0KWrJg==", "03b15530-6b56-4f8a-9cb4-dacbe3a79581" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "089e2866-78f6-47d2-b615-10688fe15f42", new DateTime(2024, 11, 14, 16, 34, 22, 297, DateTimeKind.Local).AddTicks(8456), "AQAAAAIAAYagAAAAEHSITRFDSK/eTiC4R5EKqHZbA3N3cvuMwQywJENFPbJw5ZvRLvqoC0smdY/63tIDtA==", "f5481651-e417-4c44-9958-1105ea21d38e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "17370081-8770-41a9-83ca-ef4e5331a9d3", new DateTime(2024, 11, 14, 16, 34, 22, 362, DateTimeKind.Local).AddTicks(8615), "AQAAAAIAAYagAAAAECLu921+/mKInzp0dWnnkci4HK7w5LqTZxEHCo9qHxtr6uCb++k0JihQ6lrm4AILAQ==", "92577979-4f5d-4bbd-8d2b-0a07fe7cbd9e" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e00dbd7f-2558-445c-b336-1e57153b93eb", new DateTime(2024, 11, 14, 16, 34, 22, 425, DateTimeKind.Local).AddTicks(913), "AQAAAAIAAYagAAAAENfJ+V4NVROhlcdRbSwvRtGb+4A6tgBEdVfPa1rFbQCXE6bHX6L6S2ACubjdXCTFgQ==", "7ed4be6c-df1b-4fde-9900-9ece24d88c56" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b892d64a-56f7-46bc-83de-b4fa92665bdf", new DateTime(2024, 11, 14, 16, 34, 22, 487, DateTimeKind.Local).AddTicks(4461), "AQAAAAIAAYagAAAAEMbNPgWlw/DLDC3xAfr0RdOGx8fBB188A4HltXwHwAU/Y61vcCV5PWJ5nzY2YjIDjQ==", "e5b3a2d7-5d16-47b2-9270-7d562d7c3ace" });
        }
    }
}
