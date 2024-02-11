using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace life_assistant.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "PolarUser",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AccessToken = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolarUser", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PolarDailyActivity",
                schema: "dbo",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Calories = table.Column<int>(type: "int", nullable: false),
                    ActiveCalories = table.Column<int>(type: "int", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    ActiveSteps = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PolarDailyActivity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PolarDailyActivity_PolarUser_UserId",
                        column: x => x.UserId,
                        principalSchema: "dbo",
                        principalTable: "PolarUser",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PolarDailyActivity_UserId",
                schema: "dbo",
                table: "PolarDailyActivity",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PolarDailyActivity",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "PolarUser",
                schema: "dbo");
        }
    }
}
