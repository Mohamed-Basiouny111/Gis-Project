using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gis_Project.Migrations
{
    /// <inheritdoc />
    public partial class editnull : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_ContractId",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "Assets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ContractId",
                table: "Assets",
                column: "ContractId",
                unique: true,
                filter: "[ContractId] IS NOT NULL");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Assets_ContractId",
                table: "Assets");

            migrationBuilder.AlterColumn<int>(
                name: "ContractId",
                table: "Assets",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Assets_ContractId",
                table: "Assets",
                column: "ContractId",
                unique: true);
        }
    }
}
