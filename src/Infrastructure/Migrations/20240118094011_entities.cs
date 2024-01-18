using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class entities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "pin",
                schema: "main",
                table: "candidates");

            migrationBuilder.RenameColumn(
                name: "candidate_type",
                schema: "main",
                table: "candidates",
                newName: "candidate_type_id");

            migrationBuilder.AddColumn<int>(
                name: "OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PreviousStatusId",
                schema: "main",
                table: "RewardApplicationStatuses",
                type: "integer",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "candidate_type_id",
                schema: "main",
                table: "reward_applications",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "candidate_type_name_en",
                schema: "main",
                table: "reward_applications",
                type: "character varying(128)",
                maxLength: 128,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "member_id",
                schema: "main",
                table: "candidates",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "CandidateType",
                schema: "main",
                columns: table => new
                {
                    NameEn = table.Column<string>(type: "text", nullable: false),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKg = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateType", x => x.NameEn);
                });

            migrationBuilder.CreateTable(
                name: "DocumentType",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    NameRu = table.Column<string>(type: "text", nullable: false),
                    NameKg = table.Column<string>(type: "text", nullable: false),
                    Required = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DocumentType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Members",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Pin = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Members", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateTypesDocumentTypes",
                schema: "main",
                columns: table => new
                {
                    CandidateTypeId = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CandidateTypesDocumentTypes", x => new { x.CandidateTypeId, x.DocumentTypeId });
                    table.ForeignKey(
                        name: "FK_CandidateTypesDocumentTypes_CandidateType_CandidateTypeId",
                        column: x => x.CandidateTypeId,
                        principalSchema: "main",
                        principalTable: "CandidateType",
                        principalColumn: "NameEn",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CandidateTypesDocumentTypes_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "main",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Document",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Bytes = table.Column<byte[]>(type: "bytea", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Extension = table.Column<string>(type: "text", nullable: false),
                    DocumentTypeId = table.Column<int>(type: "integer", nullable: false),
                    RewardApplicationId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Document", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Document_DocumentType_DocumentTypeId",
                        column: x => x.DocumentTypeId,
                        principalSchema: "main",
                        principalTable: "DocumentType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Document_reward_applications_RewardApplicationId",
                        column: x => x.RewardApplicationId,
                        principalSchema: "main",
                        principalTable: "reward_applications",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "PinAbsenceReason",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Reason = table.Column<string>(type: "text", nullable: false),
                    MemberId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PinAbsenceReason", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PinAbsenceReason_Members_MemberId",
                        column: x => x.MemberId,
                        principalSchema: "main",
                        principalTable: "Members",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_RewardApplicationStatuses_OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses",
                column: "OfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_RewardApplicationStatuses_PreviousStatusId",
                schema: "main",
                table: "RewardApplicationStatuses",
                column: "PreviousStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_reward_applications_candidate_type_name_en",
                schema: "main",
                table: "reward_applications",
                column: "candidate_type_name_en");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_candidate_type_id",
                schema: "main",
                table: "candidates",
                column: "candidate_type_id");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_member_id",
                schema: "main",
                table: "candidates",
                column: "member_id");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateTypesDocumentTypes_DocumentTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_DocumentTypeId",
                schema: "main",
                table: "Document",
                column: "DocumentTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Document_RewardApplicationId",
                schema: "main",
                table: "Document",
                column: "RewardApplicationId");

            migrationBuilder.CreateIndex(
                name: "IX_Members_Pin",
                schema: "main",
                table: "Members",
                column: "Pin",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PinAbsenceReason_MemberId",
                schema: "main",
                table: "PinAbsenceReason",
                column: "MemberId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_candidates_CandidateType_candidate_type_id",
                schema: "main",
                table: "candidates",
                column: "candidate_type_id",
                principalSchema: "main",
                principalTable: "CandidateType",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_candidates_Members_member_id",
                schema: "main",
                table: "candidates",
                column: "member_id",
                principalSchema: "main",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reward_applications_CandidateType_candidate_type_name_en",
                schema: "main",
                table: "reward_applications",
                column: "candidate_type_name_en",
                principalSchema: "main",
                principalTable: "CandidateType",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RewardApplicationStatuses_RewardApplicationStatuses_Previou~",
                schema: "main",
                table: "RewardApplicationStatuses",
                column: "PreviousStatusId",
                principalSchema: "main",
                principalTable: "RewardApplicationStatuses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_RewardApplicationStatuses_offices_OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses",
                column: "OfficeId",
                principalSchema: "main",
                principalTable: "offices",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_candidates_CandidateType_candidate_type_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_candidates_Members_member_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_reward_applications_CandidateType_candidate_type_name_en",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropForeignKey(
                name: "FK_RewardApplicationStatuses_RewardApplicationStatuses_Previou~",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropForeignKey(
                name: "FK_RewardApplicationStatuses_offices_OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropTable(
                name: "CandidateTypesDocumentTypes",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Document",
                schema: "main");

            migrationBuilder.DropTable(
                name: "PinAbsenceReason",
                schema: "main");

            migrationBuilder.DropTable(
                name: "CandidateType",
                schema: "main");

            migrationBuilder.DropTable(
                name: "DocumentType",
                schema: "main");

            migrationBuilder.DropTable(
                name: "Members",
                schema: "main");

            migrationBuilder.DropIndex(
                name: "IX_RewardApplicationStatuses_OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_RewardApplicationStatuses_PreviousStatusId",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropIndex(
                name: "IX_reward_applications_candidate_type_name_en",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropIndex(
                name: "IX_candidates_candidate_type_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropIndex(
                name: "IX_candidates_member_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropColumn(
                name: "OfficeId",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "PreviousStatusId",
                schema: "main",
                table: "RewardApplicationStatuses");

            migrationBuilder.DropColumn(
                name: "candidate_type_id",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropColumn(
                name: "candidate_type_name_en",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropColumn(
                name: "member_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.RenameColumn(
                name: "candidate_type_id",
                schema: "main",
                table: "candidates",
                newName: "candidate_type");

            migrationBuilder.AddColumn<string>(
                name: "pin",
                schema: "main",
                table: "candidates",
                type: "character(14)",
                fixedLength: true,
                maxLength: 14,
                nullable: true);
        }
    }
}
