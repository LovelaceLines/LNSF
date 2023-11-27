using System;
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
            migrationBuilder.AddColumn<DateTime>(
                name: "CheckIn",
                table: "HostingsEscorts",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "CheckOut",
                table: "HostingsEscorts",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CheckIn",
                table: "HostingsEscorts");

            migrationBuilder.DropColumn(
                name: "CheckOut",
                table: "HostingsEscorts");
        }
    }
}
