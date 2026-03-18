using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gis_Project.Migrations
{
    /// <inheritdoc />
    public partial class EditAccept : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "AspNetUsers");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
