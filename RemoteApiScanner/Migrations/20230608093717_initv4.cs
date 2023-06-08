using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace RemoteApiScanner.Migrations
{
    /// <inheritdoc />
    public partial class initv4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "executionTime",
                table: "EsecuzioniKiteRunner",
                type: "TEXT",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "executionTime",
                table: "EsecuzioniKiteRunner");
        }
    }
}
