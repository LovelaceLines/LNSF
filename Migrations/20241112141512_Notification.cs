using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LNSF.Migrations
{
    /// <inheritdoc />
    public partial class Notification : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Title = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false),
                    Content = table.Column<string>(type: "TEXT", maxLength: 500, nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "NotificationsUsers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    ReadAt = table.Column<DateTime>(type: "TEXT", nullable: false),
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false),
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NotificationsUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NotificationsUsers_Notifications_NotificationId",
                        column: x => x.NotificationId,
                        principalTable: "Notifications",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NotificationsUsers_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "ccdd198e-4273-4bd3-8e3e-6759a001c7d9", new DateTime(2024, 11, 12, 11, 15, 11, 671, DateTimeKind.Local).AddTicks(6497) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "42fdf3fd-a029-486e-8e90-3c2f2742dff8", new DateTime(2024, 11, 12, 11, 15, 11, 671, DateTimeKind.Local).AddTicks(6516) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "48516945-e69d-4c0d-aa6f-65940c92cb54", new DateTime(2024, 11, 12, 11, 15, 11, 671, DateTimeKind.Local).AddTicks(6520) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "25ae3d41-721a-4b95-bfe6-635de8c0690c", new DateTime(2024, 11, 12, 11, 15, 11, 671, DateTimeKind.Local).AddTicks(6535) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "2d37cd68-718d-4961-80b1-e63ba51878db", new DateTime(2024, 11, 12, 11, 15, 11, 671, DateTimeKind.Local).AddTicks(6547) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3a988256-080b-44c3-889c-0e1686e80d19", new DateTime(2024, 11, 12, 11, 15, 11, 733, DateTimeKind.Local).AddTicks(6775), "AQAAAAIAAYagAAAAEPa7iMpfK4LcCo/3iaFnQy/XN/Y3yL2PnErtpdFbhqhSd1+o5+Sr1qaiwSSqosAuvg==", "f007b115-a403-4707-947b-98c0e69cc796" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "153480d3-b031-45dd-955f-3cd4c2aa2527", new DateTime(2024, 11, 12, 11, 15, 11, 795, DateTimeKind.Local).AddTicks(9898), "AQAAAAIAAYagAAAAEEqRstcHLrcgM7nrB14mdm6vD7Ej63qee4wKQbfkSge/JWSKGwI5lzdRSv1qK7VBEQ==", "00f3d65c-7df8-4faa-b835-c2601766cae6" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ad446dcf-7cc9-40d3-b201-cad7a8539d0b", new DateTime(2024, 11, 12, 11, 15, 11, 859, DateTimeKind.Local).AddTicks(5354), "AQAAAAIAAYagAAAAEHTOeQPfUGdwpIcXjRbf9Dql3yzm+KLOVOuKlYIawpLNOuG6VokD1NbRimuyezLKBQ==", "1bd3b0b2-bd3f-4258-926c-d8bf95f53d7f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "2b6a6132-e909-4860-a79f-30c96dcd8fc4", new DateTime(2024, 11, 12, 11, 15, 11, 921, DateTimeKind.Local).AddTicks(8914), "AQAAAAIAAYagAAAAEBMAgqZohIgIDaSGL7N6knxZCOwwB1aoE7PDMwRM4OCpImXUJcmlmi2MCaWr/2JF3w==", "3970ce9a-8e29-4444-8ddb-85a50bc9af4f" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "1a87cf6c-367a-4d5c-bca3-5a33d1d36d93", new DateTime(2024, 11, 12, 11, 15, 11, 991, DateTimeKind.Local).AddTicks(7246), "AQAAAAIAAYagAAAAEOi1q6Xpd4aINwCDLQx/JYr0ii+33MiwU9czDG2NCye68w2d+jFo0iOutSj20j0xsg==", "f550a20a-9d49-404c-a4f6-63f820c988b4" });

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsUsers_NotificationId",
                table: "NotificationsUsers",
                column: "NotificationId");

            migrationBuilder.CreateIndex(
                name: "IX_NotificationsUsers_UserId",
                table: "NotificationsUsers",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NotificationsUsers");

            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "3386a4c8-5bdd-4cf1-9ca2-39098bf5563d", new DateTime(2024, 11, 6, 11, 56, 4, 939, DateTimeKind.Local).AddTicks(8862) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "083ae4e3-5cfc-4f5a-99e3-03081e16d23b", new DateTime(2024, 11, 6, 11, 56, 4, 939, DateTimeKind.Local).AddTicks(8879) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "37b4e352-6f83-487a-9391-d780224edb5f", new DateTime(2024, 11, 6, 11, 56, 4, 939, DateTimeKind.Local).AddTicks(8891) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "bb0166e5-f586-401c-80f4-be21beb77f2c", new DateTime(2024, 11, 6, 11, 56, 4, 939, DateTimeKind.Local).AddTicks(8895) });

            migrationBuilder.UpdateData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "3512de1d-ad8a-4ce8-911b-05ac982b65e5", new DateTime(2024, 11, 6, 11, 56, 4, 939, DateTimeKind.Local).AddTicks(8899) });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "3cce1d0d-e661-436a-89a9-09279b078cca", new DateTime(2024, 11, 6, 11, 56, 5, 5, DateTimeKind.Local).AddTicks(5007), "AQAAAAIAAYagAAAAEKJv9W7Jm13F/5ZwjHdVuPWG0383YurQy+HVyJ4lzLMYPOyyzAExNvvwFv4/3BP2Nw==", "24955470-5e1c-4493-9fa5-ad5dce6f7728" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "62e972be-5ea9-4fa8-943b-ffc817c8bb2a", new DateTime(2024, 11, 6, 11, 56, 5, 71, DateTimeKind.Local).AddTicks(7290), "AQAAAAIAAYagAAAAENtuRGbQyuGlYBdrtU6l6Sav/RpaYUrxVectmgeWrskHfiVLo/GIw7GU5yJvr+jYJQ==", "e5951582-64f5-4308-98bf-fb61e977335c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ac1a6864-d0c5-4273-b3b3-c2c58996a3ac", new DateTime(2024, 11, 6, 11, 56, 5, 132, DateTimeKind.Local).AddTicks(1336), "AQAAAAIAAYagAAAAEC9wzzFa+VM79wHzh6OJ/LC1N/b643+/3xsCTuiMZbpjKHT270NF5vJdP8TfphTAyA==", "eb357490-ff10-4397-9ac6-8c9d9a227341" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "33300878-4cec-4fcf-acc5-7e017383af01", new DateTime(2024, 11, 6, 11, 56, 5, 192, DateTimeKind.Local).AddTicks(6959), "AQAAAAIAAYagAAAAEArXf5TvFbcOa1ygM2ekujj8zCImsi+gKJn8lFMgw8VcMcwsDk6WWn3rnXH/MkKMoA==", "8918c927-fd0b-49d5-9337-bb758be26d8c" });

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "PasswordHash", "SecurityStamp" },
                values: new object[] { "aa537a5e-978c-418b-b11f-fccb57a4c037", new DateTime(2024, 11, 6, 11, 56, 5, 251, DateTimeKind.Local).AddTicks(4487), "AQAAAAIAAYagAAAAEKIljaIV5odQBezYbVdGQOS3MG549OCshcPKKgkJ3NxE1WQgXnsaZxhK1ohLAiJrfA==", "b05d7eaa-dbf7-4266-ad38-4d2bf10ea3c6" });
        }
    }
}
