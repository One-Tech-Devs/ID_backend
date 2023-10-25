using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ID_repository.Migrations
{
    /// <inheritdoc />
    public partial class cascadeDeleteCompany : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataRequests_Companies_CompanyId",
                table: "DataRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_DataRequests_Companies_CompanyId",
                table: "DataRequests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DataRequests_Companies_CompanyId",
                table: "DataRequests");

            migrationBuilder.AddForeignKey(
                name: "FK_DataRequests_Companies_CompanyId",
                table: "DataRequests",
                column: "CompanyId",
                principalTable: "Companies",
                principalColumn: "Id");
        }
    }
}
