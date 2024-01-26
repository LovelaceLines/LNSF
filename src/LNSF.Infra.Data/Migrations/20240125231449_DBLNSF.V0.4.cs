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
            migrationBuilder.CreateTable(
                name: "FamilyGroupProfiles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Kinship = table.Column<string>(type: "TEXT", nullable: false),
                    Age = table.Column<int>(type: "INTEGER", nullable: false),
                    Profession = table.Column<string>(type: "TEXT", nullable: false),
                    Income = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FamilyGroupProfiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FamilyGroupProfiles_Patients_PatientId",
                        column: x => x.PatientId,
                        principalTable: "Patients",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "160b165f-31fb-4d6c-a98e-08111203b935");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "776aa72d-0a73-42f5-b833-5464ab609cd5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "9fa020f2-6737-4d21-abea-e8afb8480aad");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "9b80db07-ccec-4e5d-8a16-59cbc5d07739");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "df8a1bce-0005-43f5-8eb6-a8068d9ec99e");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "bbae0f5c-43f9-48e1-9d4f-8c1d7c36a6cc", "AQAAAAIAAYagAAAAEE7ZS13FMJnXDn8OCYv64TUmMo9fB7jQbNjxHJVToPtXNtbPWJjOTBUZscnRWQhTMw==", "ea87b918-55b0-4ac0-ac46-600a4980889b" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b640756d-ffa7-4029-8d00-ccc2f211bd47", "AQAAAAIAAYagAAAAEFRon9o1CeAugM9cyON/u3ksGoKvCvZ4SjfMIDm+8D+GaqJsyjbJIING+GyeH9fizg==", "fa78426b-e0a5-4c3a-8b87-83229aead149" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "285d800c-0dca-4274-9f36-2941d9408177", "AQAAAAIAAYagAAAAEDk3PCFbrDnRlLRLBdCnO5mCINmDG0fREYUoGryXfdZuf553rdW8dnz3WWN6Era9Aw==", "491e4530-a563-4154-8746-efe93f967d21" });

            migrationBuilder.CreateIndex(
                name: "IX_FamilyGroupProfiles_PatientId",
                table: "FamilyGroupProfiles",
                column: "PatientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "FamilyGroupProfiles");

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
    }
}
