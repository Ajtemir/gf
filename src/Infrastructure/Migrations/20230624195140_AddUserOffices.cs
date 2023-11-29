using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddUserOffices : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "user_offices",
                schema: "main",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    office_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_offices", x => new { x.user_id, x.office_id });
                    table.ForeignKey(
                        name: "FK_user_offices_AspNetUsers_user_id",
                        column: x => x.user_id,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_offices_offices_office_id",
                        column: x => x.office_id,
                        principalSchema: "main",
                        principalTable: "offices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_user_offices_office_id",
                schema: "main",
                table: "user_offices",
                column: "office_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "user_offices",
                schema: "main");
        }
    }
}
