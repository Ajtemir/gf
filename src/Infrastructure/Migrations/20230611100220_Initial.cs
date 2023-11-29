using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "main");

            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:PostgresExtension:uuid-ossp", ",,");

            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Note = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    LastName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    FirstName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: false),
                    PatronymicName = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    Pin = table.Column<string>(type: "character(14)", fixedLength: true, maxLength: 14, nullable: true),
                    CreatedBy = table.Column<int>(type: "integer", nullable: true),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ModifiedBy = table.Column<int>(type: "integer", nullable: true),
                    ModifiedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Image = table.Column<byte[]>(type: "bytea", maxLength: 4194304, nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(64)", maxLength: 64, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_CreatedBy",
                        column: x => x.CreatedBy,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_AspNetUsers_AspNetUsers_ModifiedBy",
                        column: x => x.ModifiedBy,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "citizenships",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_ru = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    name_kg = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_citizenships", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "educations",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_ru = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    name_kg = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_educations", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "positions",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_ru = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    name_kg = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_positions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "main",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                schema: "main",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                schema: "main",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                schema: "main",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    RoleId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "main",
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                schema: "main",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "integer", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "offices",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false),
                    name_ru = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    name_kg = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<int>(type: "integer", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_offices", x => x.id);
                    table.ForeignKey(
                        name: "FK_offices_AspNetUsers_created_by",
                        column: x => x.created_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_offices_AspNetUsers_modified_by",
                        column: x => x.modified_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rewards",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name_ru = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    name_kg = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    image_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: false),
                    image = table.Column<byte[]>(type: "bytea", maxLength: 4194304, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<int>(type: "integer", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rewards", x => x.id);
                    table.ForeignKey(
                        name: "FK_rewards_AspNetUsers_created_by",
                        column: x => x.created_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rewards_AspNetUsers_modified_by",
                        column: x => x.modified_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "candidates",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    candidate_type = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    image = table.Column<byte[]>(type: "bytea", maxLength: 4194304, nullable: true),
                    image_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    name_ru = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    name_kg = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    last_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    first_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    patronymic_name = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: true),
                    gender = table.Column<string>(type: "text", nullable: true),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    death_date = table.Column<DateOnly>(type: "date", nullable: true),
                    registered_address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    actual_address = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    education_id = table.Column<int>(type: "integer", nullable: true),
                    science_degree = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    years_of_work_total = table.Column<int>(type: "integer", nullable: true),
                    years_of_work_in_industry = table.Column<int>(type: "integer", nullable: true),
                    years_of_work_in_collective = table.Column<int>(type: "integer", nullable: true),
                    citizenship_id = table.Column<int>(type: "integer", nullable: true),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<int>(type: "integer", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_candidates", x => x.id);
                    table.ForeignKey(
                        name: "FK_candidates_AspNetUsers_created_by",
                        column: x => x.created_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidates_AspNetUsers_modified_by",
                        column: x => x.modified_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidates_citizenships_citizenship_id",
                        column: x => x.citizenship_id,
                        principalSchema: "main",
                        principalTable: "citizenships",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_candidates_educations_education_id",
                        column: x => x.education_id,
                        principalSchema: "main",
                        principalTable: "educations",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "office_relationships",
                schema: "main",
                columns: table => new
                {
                    ChildOfficeId = table.Column<int>(type: "integer", nullable: false),
                    ParentOfficeId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_office_relationships", x => new { x.ChildOfficeId, x.ParentOfficeId });
                    table.ForeignKey(
                        name: "FK_office_relationships_offices_ChildOfficeId",
                        column: x => x.ChildOfficeId,
                        principalSchema: "main",
                        principalTable: "offices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_office_relationships_offices_ParentOfficeId",
                        column: x => x.ParentOfficeId,
                        principalSchema: "main",
                        principalTable: "offices",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "reward_applications",
                schema: "main",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    reward_id = table.Column<int>(type: "integer", nullable: false),
                    reward_candidate_id = table.Column<int>(type: "integer", nullable: false),
                    region = table.Column<string>(type: "text", nullable: false),
                    special_achievements = table.Column<string>(type: "character varying(128)", maxLength: 128, nullable: false),
                    created_by = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    modified_by = table.Column<int>(type: "integer", nullable: false),
                    modified_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_reward_applications", x => x.id);
                    table.ForeignKey(
                        name: "FK_reward_applications_AspNetUsers_created_by",
                        column: x => x.created_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reward_applications_AspNetUsers_modified_by",
                        column: x => x.modified_by,
                        principalSchema: "main",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reward_applications_candidates_reward_candidate_id",
                        column: x => x.reward_candidate_id,
                        principalSchema: "main",
                        principalTable: "candidates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_reward_applications_rewards_reward_id",
                        column: x => x.reward_id,
                        principalSchema: "main",
                        principalTable: "rewards",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "citizenships",
                columns: new[] { "id", "name_kg", "name_ru" },
                values: new object[,]
                {
                    { 1, "Австралия", "Австралия" },
                    { 2, "Австрия", "Австрия" },
                    { 3, "Азербайджан", "Азербайджан" },
                    { 4, "Албания", "Албания" },
                    { 5, "Алжир", "Алжир" },
                    { 6, "Ангола", "Ангола" },
                    { 7, "Андорра", "Андорра" },
                    { 8, "Антигуа жана Барбуда", "Антигуа и Барбуда" },
                    { 9, "Аргентина", "Аргентина" },
                    { 10, "Армения", "Армения" },
                    { 11, "Ооганстан", "Афганистан" },
                    { 12, "Багам Аралдары", "Багамы" },
                    { 13, "Бангладеш", "Бангладеш" },
                    { 14, "Барбадос", "Барбадос" },
                    { 15, "Бахрейн", "Бахрейн" },
                    { 16, "Беларусь", "Беларусь" },
                    { 17, "Белиз", "Белиз" },
                    { 18, "Бельгия", "Бельгия" },
                    { 19, "Бенин", "Бенин" },
                    { 20, "Болгария", "Болгария" },
                    { 21, "Боливия", "Боливия" },
                    { 22, "Босния жана Герцеговина", "Босния и Герцеговина" },
                    { 23, "Ботсвана", "Ботсвана" },
                    { 24, "Бразилия", "Бразилия" },
                    { 25, "Бруней", "Бруней" },
                    { 26, "Буркина-Фасо", "Буркина-Фасо" },
                    { 27, "Бурунди", "Бурунди" },
                    { 28, "Бутан", "Бутан" },
                    { 29, "Вануату", "Вануату" },
                    { 30, "Улуу Британия", "Великобритания" },
                    { 31, "Венгрия", "Венгрия" },
                    { 32, "Венесуэла", "Венесуэла" },
                    { 33, "Чыгыш Тимор", "Восточный Тимор" },
                    { 34, "Вьетнам", "Вьетнам" },
                    { 35, "Габон", "Габон" },
                    { 36, "Гаити", "Гаити" },
                    { 37, "Гайана", "Гайана" },
                    { 38, "Гамбия", "Гамбия" },
                    { 39, "Гана", "Гана" },
                    { 40, "Гватемала", "Гватемала" },
                    { 41, "Гвинея", "Гвинея" },
                    { 42, "Гвинея-Бисау", "Гвинея-Бисау" },
                    { 43, "Германия", "Германия" },
                    { 44, "Гондурас", "Гондурас" },
                    { 45, "Гренада", "Гренада" },
                    { 46, "Греция", "Греция" },
                    { 47, "Грузия", "Грузия" },
                    { 48, "Дания", "Дания" },
                    { 49, "Джибути", "Джибути" },
                    { 50, "Доминика", "Доминика" },
                    { 51, "Доминикана", "Доминикана" },
                    { 52, "Египет", "Египет" },
                    { 53, "Замбия", "Замбия" },
                    { 54, "Зимбабве", "Зимбабве" },
                    { 55, "Израиль", "Израиль" },
                    { 56, "Индия", "Индия" },
                    { 57, "Индонезия", "Индонезия" },
                    { 58, "Иордания", "Иордания" },
                    { 59, "Ирак", "Ирак" },
                    { 60, "Иран", "Иран" },
                    { 61, "Ирландия", "Ирландия" },
                    { 62, "Исландия", "Исландия" },
                    { 63, "Испания", "Испания" },
                    { 64, "Италия", "Италия" },
                    { 65, "Йемен", "Йемен" },
                    { 66, "Кабо-Верде", "Кабо-Верде" },
                    { 67, "Казакстан", "Казахстан" },
                    { 68, "Камбоджа", "Камбоджа" },
                    { 69, "Камерун", "Камерун" },
                    { 70, "Канада", "Канада" },
                    { 71, "Катар", "Катар" },
                    { 72, "Кения", "Кения" },
                    { 73, "Кипр", "Кипр" },
                    { 74, "Кирибати", "Кирибати" },
                    { 75, "Кытай", "Китай" },
                    { 76, "Колумбия", "Колумбия" },
                    { 77, "Комор Аралдары", "Коморы" },
                    { 78, "Конго", "Конго" },
                    { 79, "Конго Демократиялык Республикасы", "ДР Конго" },
                    { 80, "Корея Элдик Демократиялык Республикасы", "КНДР" },
                    { 81, "Корея", "Корея" },
                    { 82, "Коста-Рика", "Коста-Рика" },
                    { 83, "Кот-д’Ивуар", "Кот-д’Ивуар" },
                    { 84, "Куба", "Куба" },
                    { 85, "Кувейт", "Кувейт" },
                    { 86, "Кыргызстан", "Кыргызстан" },
                    { 87, "Лаос", "Лаос" },
                    { 88, "Латвия", "Латвия" },
                    { 89, "Лесото", "Лесото" },
                    { 90, "Либерия", "Либерия" },
                    { 91, "Ливан", "Ливан" },
                    { 92, "Ливия", "Ливия" },
                    { 93, "Литва", "Литва" },
                    { 94, "Лихтенштейн", "Лихтенштейн" },
                    { 95, "Люксембург", "Люксембург" },
                    { 96, "Маврикий", "Маврикий" },
                    { 97, "Мавритания", "Мавритания" },
                    { 98, "Мадагаскар", "Мадагаскар" },
                    { 99, "Малави", "Малави" },
                    { 100, "Малайзия", "Малайзия" },
                    { 101, "Мали", "Мали" },
                    { 102, "Мальдив Аралдары", "Мальдивы" },
                    { 103, "Мальта", "Мальта" },
                    { 104, "Марокко", "Марокко" },
                    { 105, "Маршалл Аралдары", "Маршалловы Острова" },
                    { 106, "Мексика", "Мексика" },
                    { 107, "Микронезия", "Микронезия" },
                    { 108, "Мозамбик", "Мозамбик" },
                    { 109, "Молдавия", "Молдавия" },
                    { 110, "Монако", "Монако" },
                    { 111, "Монголия", "Монголия" },
                    { 112, "Мьянма", "Мьянма" },
                    { 113, "Намибия", "Намибия" },
                    { 114, "Науру", "Науру" },
                    { 115, "Непал", "Непал" },
                    { 116, "Нигер", "Нигер" },
                    { 117, "Нигерия", "Нигерия" },
                    { 118, "Нидерланд", "Нидерланды" },
                    { 119, "Никарагуа", "Никарагуа" },
                    { 120, "Жаңы Зеландия", "Новая Зеландия" },
                    { 121, "Норвегия", "Норвегия" },
                    { 122, "Бириккен Араб Эмираттары", "ОАЭ" },
                    { 123, "Оман", "Оман" },
                    { 124, "Пакистан", "Пакистан" },
                    { 125, "Палау", "Палау" },
                    { 126, "Панама", "Панама" },
                    { 127, "Папуа Жаңы Гвинея", "Папуа — Новая Гвинея" },
                    { 128, "Парагвай", "Парагвай" },
                    { 129, "Перу", "Перу" },
                    { 130, "Польша", "Польша" },
                    { 131, "Португалия", "Португалия" },
                    { 132, "Орусия", "Россия" },
                    { 133, "Руанда", "Руанда" },
                    { 134, "Румыния", "Румыния" },
                    { 135, "Сальвадор", "Сальвадор" },
                    { 136, "Самоа", "Самоа" },
                    { 137, "Сан-Марино", "Сан-Марино" },
                    { 138, "Сан Томе жана Принсипи", "Сан-Томе и Принсипи" },
                    { 139, "Сауд Арабия", "Саудовская Аравия" },
                    { 140, "Түндүк Македония", "Северная Македония" },
                    { 141, "Сейшель Аралдар", "Сейшелы" },
                    { 142, "Сенегал", "Сенегал" },
                    { 143, "Сент-Винсент жана Гренадиндер", "Сент-Винсент и Гренадины" },
                    { 144, "Сент-Китс жана Невис", "Сент-Китс и Невис" },
                    { 145, "Сент-Люсия", "Сент-Люсия	" },
                    { 146, "Сербия", "Сербия" },
                    { 147, "Сингапур", "Сингапур" },
                    { 148, "Сирия", "Сирия" },
                    { 149, "Словакия", "Словакия" },
                    { 150, "Словения", "Словения" },
                    { 151, "Америка Кошмо Штаттары", "США" },
                    { 152, "Соломон Аралдары", "Соломоновы Острова" },
                    { 153, "Сомали", "Сомали" },
                    { 154, "Судан", "Судан" },
                    { 155, "Суринам", "Суринам" },
                    { 156, "Сьерра-Леоне", "Сьерра-Леоне	" },
                    { 157, "Тажикстан", "Таджикистан" },
                    { 158, "Таиланд", "Таиланд" },
                    { 159, "Танзания", "Танзания" },
                    { 160, "Того", "Того" },
                    { 161, "Тонга", "Тонга" },
                    { 162, "Тринидад жана Тобаго", "Тринидад и Тобаго	" },
                    { 163, "Тувалу", "Тувалу" },
                    { 164, "Тунис", "Тунис" },
                    { 165, "Түркмөнстан", "Туркмения" },
                    { 166, "Түркия", "Турция" },
                    { 167, "Уганда", "Уганда" },
                    { 168, "Өзбекстан", "Узбекистан" },
                    { 169, "Украина", "Украина" },
                    { 170, "Уругвай", "Уругвай" },
                    { 171, "Фиджи", "Фиджи" },
                    { 172, "Филиппин", "Филиппины" },
                    { 173, "Финляндия", "Финляндия" },
                    { 174, "Франция", "Франция" },
                    { 175, "Хорватия", "Хорватия" },
                    { 176, "Борбордук Африка Республикасы", "ЦАР" },
                    { 177, "Чад", "Чад" },
                    { 178, "Черногория", "Черногория" },
                    { 179, "Чехия", "Чехия" },
                    { 180, "Чили", "Чили" },
                    { 181, "Швейцария", "Швейцария" },
                    { 182, "Швеция", "Швеция" },
                    { 183, "Шри-Ланка", "Шри-Ланка	" },
                    { 184, "Эквадор", "Эквадор" },
                    { 185, "Экваториалдык Гвинея", "Экваториальная Гвинея" },
                    { 186, "Эритрея", "Эритрея" },
                    { 187, "Эсватини", "Эсватини" },
                    { 188, "Эстония", "Эстония" },
                    { 189, "Эфиопия", "Эфиопия" },
                    { 190, "Түштүк Африка Республикасы", "ЮАР" },
                    { 191, "Түштүк Судан", "Южный Судан" },
                    { 192, "Ямайка", "Ямайка" },
                    { 193, "Жапония", "Япония" }
                });

            migrationBuilder.InsertData(
                schema: "main",
                table: "educations",
                columns: new[] { "id", "name_kg", "name_ru" },
                values: new object[,]
                {
                    { 1, "Начальное и ниже", "Начальное и ниже" },
                    { 2, "Неполное среднее", "Неполное среднее" },
                    { 3, "Среднее общее", "Среднее общее" },
                    { 4, "Среднее специальное", "Среднее специальное" },
                    { 5, "Незаконченное высшее", "Незаконченное высшее" },
                    { 6, "Высшее", "Высшее" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                schema: "main",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "main",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                schema: "main",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                schema: "main",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                schema: "main",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "main",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_CreatedBy",
                schema: "main",
                table: "AspNetUsers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ModifiedBy",
                schema: "main",
                table: "AspNetUsers",
                column: "ModifiedBy");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "main",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_candidates_citizenship_id",
                schema: "main",
                table: "candidates",
                column: "citizenship_id");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_created_by",
                schema: "main",
                table: "candidates",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_education_id",
                schema: "main",
                table: "candidates",
                column: "education_id");

            migrationBuilder.CreateIndex(
                name: "IX_candidates_modified_by",
                schema: "main",
                table: "candidates",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_office_relationships_ParentOfficeId",
                schema: "main",
                table: "office_relationships",
                column: "ParentOfficeId");

            migrationBuilder.CreateIndex(
                name: "IX_offices_created_by",
                schema: "main",
                table: "offices",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_offices_modified_by",
                schema: "main",
                table: "offices",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_reward_applications_created_by",
                schema: "main",
                table: "reward_applications",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_reward_applications_modified_by",
                schema: "main",
                table: "reward_applications",
                column: "modified_by");

            migrationBuilder.CreateIndex(
                name: "IX_reward_applications_reward_candidate_id",
                schema: "main",
                table: "reward_applications",
                column: "reward_candidate_id");

            migrationBuilder.CreateIndex(
                name: "IX_reward_applications_reward_id",
                schema: "main",
                table: "reward_applications",
                column: "reward_id");

            migrationBuilder.CreateIndex(
                name: "IX_rewards_created_by",
                schema: "main",
                table: "rewards",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "IX_rewards_modified_by",
                schema: "main",
                table: "rewards",
                column: "modified_by");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens",
                schema: "main");

            migrationBuilder.DropTable(
                name: "office_relationships",
                schema: "main");

            migrationBuilder.DropTable(
                name: "positions",
                schema: "main");

            migrationBuilder.DropTable(
                name: "reward_applications",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetRoles",
                schema: "main");

            migrationBuilder.DropTable(
                name: "offices",
                schema: "main");

            migrationBuilder.DropTable(
                name: "candidates",
                schema: "main");

            migrationBuilder.DropTable(
                name: "rewards",
                schema: "main");

            migrationBuilder.DropTable(
                name: "citizenships",
                schema: "main");

            migrationBuilder.DropTable(
                name: "educations",
                schema: "main");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "main");
        }
    }
}
