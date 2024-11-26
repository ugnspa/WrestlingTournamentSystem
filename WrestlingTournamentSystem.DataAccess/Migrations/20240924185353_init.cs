using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace WrestlingTournamentSystem.DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TournamentStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TournamentWeightCategoryStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentWeightCategoryStatuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "WrestlingStyles",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(2)", maxLength: 2, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrestlingStyles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Location = table.Column<string>(type: "nvarchar(255)", maxLength: 255, nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Tournaments_TournamentStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TournamentStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    PrimaryCategory = table.Column<bool>(type: "bit", nullable: false),
                    StyleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeightCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeightCategories_WrestlingStyles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "WrestlingStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Wrestlers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Country = table.Column<string>(type: "nvarchar(60)", maxLength: 60, nullable: false),
                    BirthDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PhotoUrl = table.Column<string>(type: "nvarchar(2048)", maxLength: 2048, nullable: true),
                    StyleId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wrestlers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Wrestlers_WrestlingStyles_StyleId",
                        column: x => x.StyleId,
                        principalTable: "WrestlingStyles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TournamentWeightCategories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    StatusId = table.Column<int>(type: "int", nullable: false),
                    fk_TournamentId = table.Column<int>(type: "int", nullable: false),
                    fk_WeightCategoryId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TournamentWeightCategories", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TournamentWeightCategories_TournamentWeightCategoryStatuses_StatusId",
                        column: x => x.StatusId,
                        principalTable: "TournamentWeightCategoryStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TournamentWeightCategories_Tournaments_fk_TournamentId",
                        column: x => x.fk_TournamentId,
                        principalTable: "Tournaments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TournamentWeightCategories_WeightCategories_fk_WeightCategoryId",
                        column: x => x.fk_WeightCategoryId,
                        principalTable: "WeightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "WrestlersTournamentsWeightCategories",
                columns: table => new
                {
                    fk_TournamentWeightCategoryId = table.Column<int>(type: "int", nullable: false),
                    fk_WrestlerId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WrestlersTournamentsWeightCategories", x => new { x.fk_TournamentWeightCategoryId, x.fk_WrestlerId });
                    table.ForeignKey(
                        name: "FK_WrestlersTournamentsWeightCategories_TournamentWeightCategories_fk_TournamentWeightCategoryId",
                        column: x => x.fk_TournamentWeightCategoryId,
                        principalTable: "TournamentWeightCategories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WrestlersTournamentsWeightCategories_Wrestlers_fk_WrestlerId",
                        column: x => x.fk_WrestlerId,
                        principalTable: "Wrestlers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Tournaments_StatusId",
                table: "Tournaments",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentWeightCategories_fk_TournamentId",
                table: "TournamentWeightCategories",
                column: "fk_TournamentId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentWeightCategories_fk_WeightCategoryId",
                table: "TournamentWeightCategories",
                column: "fk_WeightCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_TournamentWeightCategories_StatusId",
                table: "TournamentWeightCategories",
                column: "StatusId");

            migrationBuilder.CreateIndex(
                name: "IX_WeightCategories_StyleId",
                table: "WeightCategories",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_Wrestlers_StyleId",
                table: "Wrestlers",
                column: "StyleId");

            migrationBuilder.CreateIndex(
                name: "IX_WrestlersTournamentsWeightCategories_fk_WrestlerId",
                table: "WrestlersTournamentsWeightCategories",
                column: "fk_WrestlerId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WrestlersTournamentsWeightCategories");

            migrationBuilder.DropTable(
                name: "TournamentWeightCategories");

            migrationBuilder.DropTable(
                name: "Wrestlers");

            migrationBuilder.DropTable(
                name: "TournamentWeightCategoryStatuses");

            migrationBuilder.DropTable(
                name: "Tournaments");

            migrationBuilder.DropTable(
                name: "WeightCategories");

            migrationBuilder.DropTable(
                name: "TournamentStatuses");

            migrationBuilder.DropTable(
                name: "WrestlingStyles");
        }
    }
}
