using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteApiScanner.Migrations
{
    /// <inheritdoc />
    public partial class initv3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "path",
                table: "EsecuzioniKiteRunner",
                newName: "statusCode");

            migrationBuilder.RenameColumn(
                name: "command",
                table: "EsecuzioniKiteRunner",
                newName: "routes");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "statusCode",
                table: "EsecuzioniKiteRunner",
                newName: "path");

            migrationBuilder.RenameColumn(
                name: "routes",
                table: "EsecuzioniKiteRunner",
                newName: "command");
        }
    }
}
