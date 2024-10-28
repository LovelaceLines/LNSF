using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.Migrations
{
    /// <inheritdoc />
    public partial class AddAudit : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Treatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Treatments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Treatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Treatments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Tours",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Tours",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Tours",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Tours",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ServiceRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "ServiceRecords",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "ServiceRecords",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "ServiceRecords",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Rooms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Rooms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Rooms",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Rooms",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PeoplesRoomsHostings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PeoplesRoomsHostings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PeoplesRoomsHostings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "PeoplesRoomsHostings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Peoples",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Peoples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Peoples",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Peoples",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "PatientsTreatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "PatientsTreatments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "PatientsTreatments",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "PatientsTreatments",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Patients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Patients",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Patients",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "HostingsEscorts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "HostingsEscorts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "HostingsEscorts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "HostingsEscorts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Hostings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Hostings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Hostings",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Hostings",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Hospitals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Hospitals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Hospitals",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Hospitals",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "FamilyGroupProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "FamilyGroupProfiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "FamilyGroupProfiles",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "FamilyGroupProfiles",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Escorts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Escorts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Escorts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "Escorts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "EmergencyContacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "EmergencyContacts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "EmergencyContacts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdatedBy",
                table: "EmergencyContacts",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "b8f34316-3e7b-4c6f-be2c-04076fbb436f", new DateTime(2024, 10, 25, 22, 37, 32, 838, DateTimeKind.Local).AddTicks(4614) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ffe83f1c-43bd-4539-871d-4460985c67d5", new DateTime(2024, 10, 25, 22, 37, 32, 838, DateTimeKind.Local).AddTicks(4647) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "Name" },
                values: new object[] { "884a444b-bb71-474f-9faa-ef2e932736a9", new DateTime(2024, 10, 25, 22, 37, 32, 838, DateTimeKind.Local).AddTicks(4652), "Assistente Social" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "2aa9813e-5ce0-4557-ae35-5e33b012736e", new DateTime(2024, 10, 25, 22, 37, 32, 838, DateTimeKind.Local).AddTicks(4657) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "99247015-08fe-4d37-9336-5b012dca3868", new DateTime(2024, 10, 25, 22, 37, 32, 838, DateTimeKind.Local).AddTicks(4661) });

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "Treatments",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CreatedAt", "CreatedBy", "UpdatedAt", "UpdatedBy" },
                values: new object[] { null, null, null, null });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 25, 22, 37, 33, 129, DateTimeKind.Local).AddTicks(1735));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 25, 22, 37, 33, 129, DateTimeKind.Local).AddTicks(1756));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 25, 22, 37, 33, 129, DateTimeKind.Local).AddTicks(1758));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 25, 22, 37, 33, 129, DateTimeKind.Local).AddTicks(1760));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 3 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 25, 22, 37, 33, 129, DateTimeKind.Local).AddTicks(1762));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5c867732-05d9-409f-a8c6-aa52751af27a", new DateTime(2024, 10, 25, 22, 37, 32, 917, DateTimeKind.Local).AddTicks(8858), "AQAAAAIAAYagAAAAEAG7GIsR6npXBdUoPXNgFJW1CuXYResjUJYghBr4kT3C46Q3NXX9ZDkbU7+DspVx+A==", "d871d24b-1370-47cb-9e31-8572057c7167" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9fc7d275-9ef7-4e91-80bc-5ada6b5047c0", new DateTime(2024, 10, 25, 22, 37, 33, 37, DateTimeKind.Local).AddTicks(3069), "AQAAAAIAAYagAAAAEIEbng/PsETcVKfmd2kRo0aLcoCvRA266ChjHVCNIq9qgMQtpTLpEJyPmEJ42aJNSg==", "46e3f719-8977-433f-b655-ec2a9b578f79" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ce125269-a95c-4989-825b-9e689e46cffe", new DateTime(2024, 10, 25, 22, 37, 33, 126, DateTimeKind.Local).AddTicks(6919), "AQAAAAIAAYagAAAAEGNvjE2qeGOGEITzELqQqPymQm61CbXLMeE2rSnXCbeJxS8FX/29q8IgzKDuKcDe0A==", "537f23f2-cb63-482a-bed0-eed59458d4bf" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Treatments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Tours");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "ServiceRecords");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Peoples");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "PatientsTreatments");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "PatientsTreatments");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "PatientsTreatments");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "PatientsTreatments");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Patients");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "HostingsEscorts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "HostingsEscorts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "HostingsEscorts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "HostingsEscorts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Hostings");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Hostings");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Hostings");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Hostings");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Hospitals");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "FamilyGroupProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "FamilyGroupProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "FamilyGroupProfiles");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "FamilyGroupProfiles");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Escorts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Escorts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Escorts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "Escorts");

            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "EmergencyContacts");

            migrationBuilder.DropColumn(
                name: "UpdatedBy",
                table: "EmergencyContacts");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "63dc6a45-87b3-4da2-80a3-bf935ad184cd", new DateTime(2024, 10, 23, 20, 23, 21, 909, DateTimeKind.Local).AddTicks(6903) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ae07ab74-ea5e-4b1c-b914-453b3618834c", new DateTime(2024, 10, 23, 20, 23, 21, 909, DateTimeKind.Local).AddTicks(6929) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "Name" },
                values: new object[] { "b439e8bf-feae-46bc-a6f1-fe62cc7813ef", new DateTime(2024, 10, 23, 20, 23, 21, 909, DateTimeKind.Local).AddTicks(6945), "AssistenteSocial" });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "868ab74e-2408-4947-acc0-e8b804e69614", new DateTime(2024, 10, 23, 20, 23, 21, 909, DateTimeKind.Local).AddTicks(6950) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ad291550-69cd-4ef8-b05b-83f162a2df15", new DateTime(2024, 10, 23, 20, 23, 21, 909, DateTimeKind.Local).AddTicks(6962) });

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 1, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 23, 22, 212, DateTimeKind.Local).AddTicks(8998));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 23, 22, 212, DateTimeKind.Local).AddTicks(9032));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 3, 1 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 23, 22, 212, DateTimeKind.Local).AddTicks(9034));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 2, 2 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 23, 22, 212, DateTimeKind.Local).AddTicks(9036));

            migrationBuilder.UpdateData(
                table: "UserRoles",
                keyColumns: new[] { "RoleId", "UserId" },
                keyValues: new object[] { 5, 3 },
                column: "CreatedAt",
                value: new DateTime(2024, 10, 23, 20, 23, 22, 212, DateTimeKind.Local).AddTicks(9037));

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "e37acf6b-2983-4c47-8738-8cc4d551150d", new DateTime(2024, 10, 23, 20, 23, 21, 992, DateTimeKind.Local).AddTicks(8345), "AQAAAAIAAYagAAAAEMK//XvhzUabJihkZ2fkll+gfDQIxKGpkBNxKUZ6V+n6ZT0o1US3ccpm62fk4axZqw==", "a8432e0f-513b-43b9-addc-5e29f4455632" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0caca49b-097a-4fc8-8a68-db7a0e0feec5", new DateTime(2024, 10, 23, 20, 23, 22, 117, DateTimeKind.Local).AddTicks(6976), "AQAAAAIAAYagAAAAEEVe71TDW1Hff+ZYGd4RttZACfvuNVktEoGNG3dkeY1m/0sbK/VCUViqu8a8uyKShw==", "d90050a1-0240-48d9-9fd3-fbfacaa3c41f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "de7f031e-b134-4e34-82a4-6c2fcd534ca7", new DateTime(2024, 10, 23, 20, 23, 22, 209, DateTimeKind.Local).AddTicks(5876), "AQAAAAIAAYagAAAAEJdv6443OJMDNlDfiu4ToTL/TQzr1MWVdsn71KbiGgE2sn4w07buDHQQKfJaGo5YNA==", "8a8bcd0e-1398-48fb-ac82-2c2792bdd4a8" });
        }
    }
}
