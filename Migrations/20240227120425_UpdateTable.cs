using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiWeathergrid.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserHasLocations",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false),
                    FavoriteLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserHasLocations", x => new { x.UserId, x.FavoriteLocationId });
                    table.ForeignKey(
                        name: "FK_UserHasLocations_FavoriteLocation_FavoriteLocationId",
                        column: x => x.FavoriteLocationId,
                        principalTable: "FavoriteLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserHasLocations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UserHasLocations_FavoriteLocationId",
                table: "UserHasLocations",
                column: "FavoriteLocationId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserHasLocations");
        }
    }
}
