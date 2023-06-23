using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteApiScanner.Migrations
{
    /// <inheritdoc />
    public partial class initv5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "executionDate",
                table: "EsecuzioniKiteRunner",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "executionDate",
                table: "EsecuzioniKiteRunner");
        }
    }
}
