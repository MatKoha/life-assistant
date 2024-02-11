using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_assistant.Server.Migrations
{
    /// <inheritdoc />
    public partial class PolarMemberId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MemberId",
                schema: "dbo",
                table: "PolarUser",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MemberId",
                schema: "dbo",
                table: "PolarUser");
        }
    }
}
