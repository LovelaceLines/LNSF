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
            migrationBuilder.CreateTable(
                name: "ServiceRecords",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    PatientId = table.Column<int>(type: "INTEGER", nullable: false),
                    SocialProgram = table.Column<string>(type: "TEXT", nullable: false),
                    SocialProgramNote = table.Column<string>(type: "TEXT", nullable: false),
                    DomicileType = table.Column<int>(type: "INTEGER", nullable: false),
                    MaterialExternalWallsDomicile = table.Column<string>(type: "TEXT", nullable: false),
                    AccessElectricalEnergy = table.Column<int>(type: "INTEGER", nullable: false),
                    HasWaterSupply = table.Column<bool>(type: "INTEGER", nullable: false),
                    WayWaterSupply = table.Column<string>(type: "TEXT", nullable: false),
                    SanitaryDrainage = table.Column<int>(type: "INTEGER", nullable: false),
                    GarbageCollection = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberRooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberBedrooms = table.Column<int>(type: "INTEGER", nullable: false),
                    NumberPeoplePerBedroom = table.Column<int>(type: "INTEGER", nullable: false),
                    DomicileHasAccessibility = table.Column<int>(type: "INTEGER", nullable: false),
                    IsLocatedInRiskArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocatedInDifficultAccessArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    IsLocatedInConflictViolenceArea = table.Column<bool>(type: "INTEGER", nullable: false),
                    AccessToUnit = table.Column<int>(type: "INTEGER", nullable: false),
                    AccessToUnitNote = table.Column<string>(type: "TEXT", nullable: false),
                    FirstAttendanceReason = table.Column<string>(type: "TEXT", nullable: false),
                    DemandPresented = table.Column<string>(type: "TEXT", nullable: false),
                    Referrals = table.Column<string>(type: "TEXT", nullable: false),
                    Observations = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceRecords", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceRecords_Patients_PatientId",
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
                value: "a2bafb58-54be-45e0-97ca-d3fb279ca7ba");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "46c3d93d-649e-44e3-9e91-760598dfa137");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a26f56bb-1455-4cbd-8445-e563e04f8be2");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "de2e4547-8799-4ed9-9925-7f645cb2b528");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "e61f3632-2ad8-4508-b8df-1d11a35059c2");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f3954efc-e292-4ad9-bf27-d91bb084cacf", "AQAAAAIAAYagAAAAEKyMqq8Qg/OrO6hxR72GAXS0Hqqr9d1d6QVeBGBJNKxW+7Vhw4soANQvipYqGsNU3w==", "b70d1806-74fe-489e-a528-a7c67171896c" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "96dbf810-daa0-4038-ad57-7694aa4df660", "AQAAAAIAAYagAAAAEM9kxDMnfQ/W63Tz0rUhBC12bvuKLxVKZMqolLJlSqEY3KKpOOmzSC4J4w+AxoRA5g==", "e2060c52-cf2b-489c-ac52-ad00762ae585" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f2e9210e-26a9-40c1-afb4-ff778a80dc16", "AQAAAAIAAYagAAAAEFg2sRjjfHEuT4WS4sKENUGws0LF4gUcPTfMaQ/6S5iG3Pkb8w8vYXHFtiXwPbgWgQ==", "a94a11e8-29c7-4d65-a943-db6c057a1934" });

            migrationBuilder.CreateIndex(
                name: "IX_ServiceRecords_PatientId",
                table: "ServiceRecords",
                column: "PatientId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ServiceRecords");

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
        }
    }
}
