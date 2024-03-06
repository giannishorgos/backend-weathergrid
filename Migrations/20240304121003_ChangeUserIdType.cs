using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace apiWeathergrid.Migrations
{
    /// <inheritdoc />
    public partial class ChangeUserIdType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "FavoriteLocation",
                columns: table => new
                {
                    Id = table
                        .Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FavoriteLocation", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    Id = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                }
            );

            migrationBuilder.CreateTable(
                name: "UserHasLocations",
                columns: table => new
                {
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    FavoriteLocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey(
                        "PK_UserHasLocations",
                        x => new { x.UserId, x.FavoriteLocationId }
                    );
                    table.ForeignKey(
                        name: "FK_UserHasLocations_FavoriteLocation_FavoriteLocationId",
                        column: x => x.FavoriteLocationId,
                        principalTable: "FavoriteLocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                    table.ForeignKey(
                        name: "FK_UserHasLocations_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade
                    );
                }
            );

            migrationBuilder.CreateIndex(
                name: "IX_UserHasLocations_FavoriteLocationId",
                table: "UserHasLocations",
                column: "FavoriteLocationId"
            );
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(name: "UserHasLocations");

            migrationBuilder.DropTable(name: "FavoriteLocation");

            migrationBuilder.DropTable(name: "User");
        }
    }
}
