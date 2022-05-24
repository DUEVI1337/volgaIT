using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace VolgaIT.Migrations
{
    public partial class addManyToManyUserApps2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UsersApps",
                columns: table => new
                {
                    UsersId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    AppsId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersApps", x => new { x.UsersId, x.AppsId });
                    table.ForeignKey(
                        name: "FK_UsersApps_Apps_AppsId",
                        column: x => x.AppsId,
                        principalTable: "Apps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsersApps_AspNetUsers_UsersId",
                        column: x => x.UsersId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_UsersApps_AppsId",
                table: "UsersApps",
                column: "AppsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UsersApps");
        }
    }
}
