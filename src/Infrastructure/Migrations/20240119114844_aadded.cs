using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class aadded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                schema: "main",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "8425e404-2f2b-467e-b852-77b7762fe0d6");

            migrationBuilder.UpdateData(
                schema: "main",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "ModifiedAt", "PasswordHash" },
                values: new object[] { "c1939d9a-d0c3-4866-81cd-92b82524f704", new DateTime(2024, 1, 19, 11, 48, 44, 32, DateTimeKind.Utc).AddTicks(1505), new DateTime(2024, 1, 19, 11, 48, 44, 32, DateTimeKind.Utc).AddTicks(1533), "AQAAAAIAAYagAAAAEFRYi9O3UGt48Cl+maK5i9no5y4KHGT7TsjHXEQfbLqfFXHaswD7KaQevshBOxHaXQ==" });

            migrationBuilder.InsertData(
                schema: "main",
                table: "CandidateTypes",
                columns: new[] { "NameEn", "NameKg", "NameRu" },
                values: new object[,]
                {
                    { "Citizen", "Жараан", "гражданин" },
                    { "Entity", "Коом", "Организация" },
                    { "Foreigner", "Чет өлкөлүк жараан", "Иностранец" }
                });

            migrationBuilder.UpdateData(
                schema: "main",
                table: "offices",
                keyColumn: "id",
                keyValue: 51617,
                columns: new[] { "created_at", "modified_at" },
                values: new object[] { new DateTime(2024, 1, 19, 11, 48, 44, 90, DateTimeKind.Utc).AddTicks(1798), new DateTime(2024, 1, 19, 11, 48, 44, 90, DateTimeKind.Utc).AddTicks(1821) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "main",
                table: "CandidateTypes",
                keyColumn: "NameEn",
                keyValue: "Citizen");

            migrationBuilder.DeleteData(
                schema: "main",
                table: "CandidateTypes",
                keyColumn: "NameEn",
                keyValue: "Entity");

            migrationBuilder.DeleteData(
                schema: "main",
                table: "CandidateTypes",
                keyColumn: "NameEn",
                keyValue: "Foreigner");

            migrationBuilder.UpdateData(
                schema: "main",
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: 1,
                column: "ConcurrencyStamp",
                value: "a9fd8470-98b2-426c-925e-6d225b114c71");

            migrationBuilder.UpdateData(
                schema: "main",
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ConcurrencyStamp", "CreatedAt", "ModifiedAt", "PasswordHash" },
                values: new object[] { "320f2064-5e0a-4659-8af0-eb2934123cf1", new DateTime(2024, 1, 19, 10, 41, 36, 782, DateTimeKind.Utc).AddTicks(4546), new DateTime(2024, 1, 19, 10, 41, 36, 782, DateTimeKind.Utc).AddTicks(4577), "AQAAAAIAAYagAAAAEIpwrFAM+yCqiFvw9wuooZ8gvUgqWG/IaNY/q3qtE0YUx7q6sflJyL6FwrMaTe07Dw==" });

            migrationBuilder.UpdateData(
                schema: "main",
                table: "offices",
                keyColumn: "id",
                keyValue: 51617,
                columns: new[] { "created_at", "modified_at" },
                values: new object[] { new DateTime(2024, 1, 19, 10, 41, 36, 839, DateTimeKind.Utc).AddTicks(4820), new DateTime(2024, 1, 19, 10, 41, 36, 839, DateTimeKind.Utc).AddTicks(4844) });
        }
    }
}
