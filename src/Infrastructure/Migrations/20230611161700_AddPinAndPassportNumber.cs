using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddPinAndPassportNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "passport_number",
                schema: "main",
                table: "candidates",
                type: "character varying(10)",
                maxLength: 10,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "pin",
                schema: "main",
                table: "candidates",
                type: "character(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "passport_number",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropColumn(
                name: "pin",
                schema: "main",
                table: "candidates");
        }
    }
}
