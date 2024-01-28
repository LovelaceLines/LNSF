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
            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRooms_Hostings_HostingId",
                table: "PeoplesRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRooms_Peoples_PeopleId",
                table: "PeoplesRooms");

            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRooms_Rooms_RoomId",
                table: "PeoplesRooms");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeoplesRooms",
                table: "PeoplesRooms");

            migrationBuilder.RenameTable(
                name: "PeoplesRooms",
                newName: "PeoplesRoomsHostings");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesRooms_PeopleId",
                table: "PeoplesRoomsHostings",
                newName: "IX_PeoplesRoomsHostings_PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesRooms_HostingId",
                table: "PeoplesRoomsHostings",
                newName: "IX_PeoplesRoomsHostings_HostingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeoplesRoomsHostings",
                table: "PeoplesRoomsHostings",
                columns: new[] { "RoomId", "PeopleId", "HostingId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "311f9b7f-dc3b-4e0b-bff1-ebf561a58bae");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "579f56d2-a224-40bf-ac48-46ef13a84841");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "a6fba774-47e3-4e13-a504-7cc3a744032d");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "d846ebca-c7d9-4593-80ce-13969d76e0c5");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "d44f6bd8-c824-4571-8831-4310c99034f3");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ccf9ab23-704f-4c9d-b4d0-bf3087ea663b", "AQAAAAIAAYagAAAAEPZBNmO7HP14XcLXiIhfEcVpWx9u+SdweGG1g5f/GV5M/4h+BtNVduRgTlfTeMY+/Q==", "d1c3a219-765b-4773-9896-00e7ecab7e0d" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0642c815-cd01-4502-aeb4-74bd06d8b616", "AQAAAAIAAYagAAAAEMYVPzmS4Psn8TaPagMU6j3d7XwA6yBBvw266T88+lhUkj9LH14xXDIT62oInw5adw==", "87809cc5-6d24-4850-a628-d40d1d094434" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "d89e614a-7373-408c-a5b3-6848efb043fa", "AQAAAAIAAYagAAAAEBysXUaP0284GcmIADueynjaG13jfVNXQP0QHyOhlC0nyUTfU0ER23mVXqSn7KLZow==", "fb905573-50b6-4f4f-a680-10b9a3ef0d88" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRoomsHostings_Hostings_HostingId",
                table: "PeoplesRoomsHostings",
                column: "HostingId",
                principalTable: "Hostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRoomsHostings_Peoples_PeopleId",
                table: "PeoplesRoomsHostings",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRoomsHostings_Rooms_RoomId",
                table: "PeoplesRoomsHostings",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRoomsHostings_Hostings_HostingId",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRoomsHostings_Peoples_PeopleId",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropForeignKey(
                name: "FK_PeoplesRoomsHostings_Rooms_RoomId",
                table: "PeoplesRoomsHostings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeoplesRoomsHostings",
                table: "PeoplesRoomsHostings");

            migrationBuilder.RenameTable(
                name: "PeoplesRoomsHostings",
                newName: "PeoplesRooms");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesRoomsHostings_PeopleId",
                table: "PeoplesRooms",
                newName: "IX_PeoplesRooms_PeopleId");

            migrationBuilder.RenameIndex(
                name: "IX_PeoplesRoomsHostings_HostingId",
                table: "PeoplesRooms",
                newName: "IX_PeoplesRooms_HostingId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeoplesRooms",
                table: "PeoplesRooms",
                columns: new[] { "RoomId", "PeopleId", "HostingId" });

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "1",
                column: "ConcurrencyStamp",
                value: "da3669c3-c653-4551-b4e4-5936104884b4");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2",
                column: "ConcurrencyStamp",
                value: "12d6d878-431d-4f95-9413-099372782292");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "3",
                column: "ConcurrencyStamp",
                value: "15416e12-075b-408b-8286-f89123d02bfe");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "4",
                column: "ConcurrencyStamp",
                value: "e904e8f3-1556-4650-9f5a-00eade546a46");

            migrationBuilder.UpdateData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5",
                column: "ConcurrencyStamp",
                value: "8f7e1e96-b189-4216-8ebd-f81881061531");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "566a31f0-637e-441b-ade3-f14a931ddc78", "AQAAAAIAAYagAAAAEG83TVyYd4ydaeb9vyCdm4Szkkdp3o6j8e8Vhee4h+z2D+5ESLhyKNXnsB5K3UR4Vg==", "38b1c6c5-1136-4678-94d2-806fbbf7d6d4" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "2",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "b44e6f1b-e2c5-43f3-8a53-61ffeed27ff7", "AQAAAAIAAYagAAAAEBgiW9Y+CIQ0wcVlOvLucD3lBy2QWt6LtU2jDyrsHeA9Nybp8biV+omNbms5ZrlghA==", "1729ad39-892a-4234-9290-cd989533528f" });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "3",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "0ce89535-3e57-4afa-9fef-b3c1b47c73cb", "AQAAAAIAAYagAAAAEKbOkSMfz6LeScWHyUFYnnpzu9CI0mj3XDjQHBTrK11bhbDt01h4bJrVG97ZTKMh2Q==", "d6ee07cc-63b4-4108-972f-5e655e83db91" });

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRooms_Hostings_HostingId",
                table: "PeoplesRooms",
                column: "HostingId",
                principalTable: "Hostings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRooms_Peoples_PeopleId",
                table: "PeoplesRooms",
                column: "PeopleId",
                principalTable: "Peoples",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PeoplesRooms_Rooms_RoomId",
                table: "PeoplesRooms",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
