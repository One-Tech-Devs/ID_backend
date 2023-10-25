using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ID_repository.Migrations
{
    /// <inheritdoc />
    public partial class KeyIv : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Iv",
                table: "Companies",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Key",
                table: "Companies",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Iv",
                table: "Clients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);

            migrationBuilder.AddColumn<byte[]>(
                name: "Key",
                table: "Clients",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Iv",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Companies");

            migrationBuilder.DropColumn(
                name: "Iv",
                table: "Clients");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Clients");
        }
    }
}
