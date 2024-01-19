using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class dataSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "main",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(64)",
                oldMaxLength: 64,
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoleId1",
                schema: "main",
                table: "AspNetUserRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId1",
                schema: "main",
                table: "AspNetUserRoles",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                schema: "main",
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName", "Note" },
                values: new object[] { 1, "a9fd8470-98b2-426c-925e-6d225b114c71", "Administrator", "ADMINISTRATOR", "Администратор" });

            migrationBuilder.InsertData(
                schema: "main",
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "CreatedAt", "CreatedBy", "Email", "EmailConfirmed", "FirstName", "Image", "IsDeleted", "LastName", "LockoutEnabled", "LockoutEnd", "ModifiedAt", "ModifiedBy", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PatronymicName", "PhoneNumber", "PhoneNumberConfirmed", "Pin", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { 1, 0, "320f2064-5e0a-4659-8af0-eb2934123cf1", new DateTime(2024, 1, 19, 10, 41, 36, 782, DateTimeKind.Utc).AddTicks(4546), null, "Admin@test.ru", false, "Админ", null, false, "Админов", false, null, new DateTime(2024, 1, 19, 10, 41, 36, 782, DateTimeKind.Utc).AddTicks(4577), null, null, null, "AQAAAAIAAYagAAAAEIpwrFAM+yCqiFvw9wuooZ8gvUgqWG/IaNY/q3qtE0YUx7q6sflJyL6FwrMaTe07Dw==", null, "996111222333", false, null, null, false, "Admin" });

            migrationBuilder.InsertData(
                schema: "main",
                table: "CandidateTypes",
                columns: new[] { "NameEn", "NameKg", "NameRu" },
                values: new object[] { "Mother", "Эне", "Мать" });

            migrationBuilder.InsertData(
                schema: "main",
                table: "offices",
                columns: new[] { "id", "created_at", "created_by", "modified_at", "modified_by", "name_kg", "name_ru" },
                values: new object[] { 51617, new DateTime(2024, 1, 19, 10, 41, 36, 839, DateTimeKind.Utc).AddTicks(4820), 1, new DateTime(2024, 1, 19, 10, 41, 36, 839, DateTimeKind.Utc).AddTicks(4844), 1, "Мамлекеттик сыйлыктардын секретариаты", "Секретариат по государственным наградам" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                schema: "main",
                table: "AspNetUserRoles",
                column: "RoleId1");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_UserId1",
                schema: "main",
                table: "AspNetUserRoles",
                column: "UserId1");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                schema: "main",
                table: "AspNetUserRoles",
                column: "RoleId1",
                principalSchema: "main",
                principalTable: "AspNetRoles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                schema: "main",
                table: "AspNetUserRoles",
                column: "UserId1",
                principalSchema: "main",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetRoles_RoleId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUserRoles_AspNetUsers_UserId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_RoleId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUserRoles_UserId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.DeleteData(
                schema: "main",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                schema: "main",
                table: "CandidateTypes",
                keyColumn: "NameEn",
                keyValue: "Mother");

            migrationBuilder.DeleteData(
                schema: "main",
                table: "offices",
                keyColumn: "id",
                keyValue: 51617);

            migrationBuilder.DeleteData(
                schema: "main",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DropColumn(
                name: "RoleId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "UserId1",
                schema: "main",
                table: "AspNetUserRoles");

            migrationBuilder.AlterColumn<string>(
                name: "Email",
                schema: "main",
                table: "AspNetUsers",
                type: "character varying(64)",
                maxLength: 64,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);
        }
    }
}
