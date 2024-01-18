using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class addedEntities : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_candidates_CandidateType_candidate_type_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTypesDocumentTypes_CandidateType_CandidateTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTypesDocumentTypes_DocumentType_DocumentTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                schema: "main",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_Document_reward_applications_RewardApplicationId",
                schema: "main",
                table: "Document");

            migrationBuilder.DropForeignKey(
                name: "FK_PinAbsenceReason_Members_MemberId",
                schema: "main",
                table: "PinAbsenceReason");

            migrationBuilder.DropForeignKey(
                name: "FK_reward_applications_CandidateType_candidate_type_name_en",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PinAbsenceReason",
                schema: "main",
                table: "PinAbsenceReason");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentType",
                schema: "main",
                table: "DocumentType");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Document",
                schema: "main",
                table: "Document");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateType",
                schema: "main",
                table: "CandidateType");

            migrationBuilder.RenameTable(
                name: "PinAbsenceReason",
                schema: "main",
                newName: "PinAbsenceReasons",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "DocumentType",
                schema: "main",
                newName: "DocumentTypes",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "Document",
                schema: "main",
                newName: "Documents",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CandidateType",
                schema: "main",
                newName: "CandidateTypes",
                newSchema: "main");

            migrationBuilder.RenameIndex(
                name: "IX_PinAbsenceReason_MemberId",
                schema: "main",
                table: "PinAbsenceReasons",
                newName: "IX_PinAbsenceReasons_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_RewardApplicationId",
                schema: "main",
                table: "Documents",
                newName: "IX_Documents_RewardApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Document_DocumentTypeId",
                schema: "main",
                table: "Documents",
                newName: "IX_Documents_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PinAbsenceReasons",
                schema: "main",
                table: "PinAbsenceReasons",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "main",
                table: "DocumentTypes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Documents",
                schema: "main",
                table: "Documents",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateTypes",
                schema: "main",
                table: "CandidateTypes",
                column: "NameEn");

            migrationBuilder.CreateTable(
                name: "IssuedRewards",
                schema: "main",
                columns: table => new
                {
                    RewardApplicationStatusId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssuedRewards", x => x.RewardApplicationStatusId);
                    table.ForeignKey(
                        name: "FK_IssuedRewards_RewardApplicationStatuses_RewardApplicationSt~",
                        column: x => x.RewardApplicationStatusId,
                        principalSchema: "main",
                        principalTable: "RewardApplicationStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MothersChildren",
                schema: "main",
                columns: table => new
                {
                    MotherId = table.Column<int>(type: "integer", nullable: false),
                    ChildId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MothersChildren", x => new { x.MotherId, x.ChildId });
                    table.ForeignKey(
                        name: "FK_MothersChildren_candidates_ChildId",
                        column: x => x.ChildId,
                        principalSchema: "main",
                        principalTable: "candidates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MothersChildren_candidates_MotherId",
                        column: x => x.MotherId,
                        principalSchema: "main",
                        principalTable: "candidates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MothersChildren_ChildId",
                schema: "main",
                table: "MothersChildren",
                column: "ChildId");

            migrationBuilder.AddForeignKey(
                name: "FK_candidates_CandidateTypes_candidate_type_id",
                schema: "main",
                table: "candidates",
                column: "candidate_type_id",
                principalSchema: "main",
                principalTable: "CandidateTypes",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTypesDocumentTypes_CandidateTypes_CandidateTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes",
                column: "CandidateTypeId",
                principalSchema: "main",
                principalTable: "CandidateTypes",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTypesDocumentTypes_DocumentTypes_DocumentTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes",
                column: "DocumentTypeId",
                principalSchema: "main",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                schema: "main",
                table: "Documents",
                column: "DocumentTypeId",
                principalSchema: "main",
                principalTable: "DocumentTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_reward_applications_RewardApplicationId",
                schema: "main",
                table: "Documents",
                column: "RewardApplicationId",
                principalSchema: "main",
                principalTable: "reward_applications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PinAbsenceReasons_Members_MemberId",
                schema: "main",
                table: "PinAbsenceReasons",
                column: "MemberId",
                principalSchema: "main",
                principalTable: "Members",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_reward_applications_CandidateTypes_candidate_type_name_en",
                schema: "main",
                table: "reward_applications",
                column: "candidate_type_name_en",
                principalSchema: "main",
                principalTable: "CandidateTypes",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_candidates_CandidateTypes_candidate_type_id",
                schema: "main",
                table: "candidates");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTypesDocumentTypes_CandidateTypes_CandidateTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_CandidateTypesDocumentTypes_DocumentTypes_DocumentTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_DocumentTypes_DocumentTypeId",
                schema: "main",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_Documents_reward_applications_RewardApplicationId",
                schema: "main",
                table: "Documents");

            migrationBuilder.DropForeignKey(
                name: "FK_PinAbsenceReasons_Members_MemberId",
                schema: "main",
                table: "PinAbsenceReasons");

            migrationBuilder.DropForeignKey(
                name: "FK_reward_applications_CandidateTypes_candidate_type_name_en",
                schema: "main",
                table: "reward_applications");

            migrationBuilder.DropTable(
                name: "IssuedRewards",
                schema: "main");

            migrationBuilder.DropTable(
                name: "MothersChildren",
                schema: "main");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PinAbsenceReasons",
                schema: "main",
                table: "PinAbsenceReasons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DocumentTypes",
                schema: "main",
                table: "DocumentTypes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Documents",
                schema: "main",
                table: "Documents");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CandidateTypes",
                schema: "main",
                table: "CandidateTypes");

            migrationBuilder.RenameTable(
                name: "PinAbsenceReasons",
                schema: "main",
                newName: "PinAbsenceReason",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "DocumentTypes",
                schema: "main",
                newName: "DocumentType",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "Documents",
                schema: "main",
                newName: "Document",
                newSchema: "main");

            migrationBuilder.RenameTable(
                name: "CandidateTypes",
                schema: "main",
                newName: "CandidateType",
                newSchema: "main");

            migrationBuilder.RenameIndex(
                name: "IX_PinAbsenceReasons_MemberId",
                schema: "main",
                table: "PinAbsenceReason",
                newName: "IX_PinAbsenceReason_MemberId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_RewardApplicationId",
                schema: "main",
                table: "Document",
                newName: "IX_Document_RewardApplicationId");

            migrationBuilder.RenameIndex(
                name: "IX_Documents_DocumentTypeId",
                schema: "main",
                table: "Document",
                newName: "IX_Document_DocumentTypeId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PinAbsenceReason",
                schema: "main",
                table: "PinAbsenceReason",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DocumentType",
                schema: "main",
                table: "DocumentType",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Document",
                schema: "main",
                table: "Document",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CandidateType",
                schema: "main",
                table: "CandidateType",
                column: "NameEn");

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
                name: "FK_CandidateTypesDocumentTypes_CandidateType_CandidateTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes",
                column: "CandidateTypeId",
                principalSchema: "main",
                principalTable: "CandidateType",
                principalColumn: "NameEn",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CandidateTypesDocumentTypes_DocumentType_DocumentTypeId",
                schema: "main",
                table: "CandidateTypesDocumentTypes",
                column: "DocumentTypeId",
                principalSchema: "main",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_DocumentType_DocumentTypeId",
                schema: "main",
                table: "Document",
                column: "DocumentTypeId",
                principalSchema: "main",
                principalTable: "DocumentType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Document_reward_applications_RewardApplicationId",
                schema: "main",
                table: "Document",
                column: "RewardApplicationId",
                principalSchema: "main",
                principalTable: "reward_applications",
                principalColumn: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_PinAbsenceReason_Members_MemberId",
                schema: "main",
                table: "PinAbsenceReason",
                column: "MemberId",
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
        }
    }
}
