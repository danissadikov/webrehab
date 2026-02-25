using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class initial_create : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "conditions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    body_region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    description = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_conditions", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    role = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "exercises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    body_region = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visibility = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    params_schema_json = table.Column<string>(type: "text", nullable: true, comment: "JSON Schema for exercise parameters"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercises", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercises_users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "patients",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    full_name = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    birth_date = table.Column<DateOnly>(type: "date", nullable: true),
                    sex = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: true),
                    notes = table.Column<string>(type: "text", nullable: true),
                    tags_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: custom tags/metadata"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patients", x => x.id);
                    table.ForeignKey(
                        name: "FK_patients_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rehab_protocols",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visibility = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rehab_protocols", x => x.id);
                    table.ForeignKey(
                        name: "FK_rehab_protocols_conditions_condition_id",
                        column: x => x.condition_id,
                        principalTable: "conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_rehab_protocols_users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "templates",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visibility = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_templates", x => x.id);
                    table.ForeignKey(
                        name: "FK_templates_users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_definitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "text", nullable: true),
                    instruction = table.Column<string>(type: "text", nullable: true),
                    result_schema_json = table.Column<string>(type: "text", nullable: true, comment: "JSON Schema for validation"),
                    owner_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    visibility = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_definitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_definitions_users_owner_user_id",
                        column: x => x.owner_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "media_assets",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    kind = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    url = table.Column<string>(type: "text", nullable: false),
                    uploaded_by_user_id = table.Column<Guid>(type: "uuid", nullable: true),
                    patient_id = table.Column<int>(type: "integer", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media_assets", x => x.id);
                    table.ForeignKey(
                        name: "FK_media_assets_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_media_assets_users_uploaded_by_user_id",
                        column: x => x.uploaded_by_user_id,
                        principalTable: "users",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "patient_assignments",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_assignments", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_assignments_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_assignments_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient_conditions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    condition_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    side = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: true),
                    onset_date = table.Column<DateOnly>(type: "date", nullable: true),
                    is_primary = table.Column<bool>(type: "boolean", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_conditions", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_conditions_conditions_condition_id",
                        column: x => x.condition_id,
                        principalTable: "conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_conditions_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_conditions_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "visit_notes",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    author_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    visit_date = table.Column<DateOnly>(type: "date", nullable: false),
                    diagnosis_text = table.Column<string>(type: "text", nullable: true),
                    subjective = table.Column<string>(type: "text", nullable: true),
                    objective = table.Column<string>(type: "text", nullable: true),
                    assessment = table.Column<string>(type: "text", nullable: true),
                    plan = table.Column<string>(type: "text", nullable: true),
                    extra_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: additional structured data"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_visit_notes", x => x.id);
                    table.ForeignKey(
                        name: "FK_visit_notes_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_visit_notes_users_author_user_id",
                        column: x => x.author_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "rehab_stages",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    protocol_id = table.Column<int>(type: "integer", nullable: false),
                    code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    goals = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_rehab_stages", x => x.id);
                    table.ForeignKey(
                        name: "FK_rehab_stages_rehab_protocols_protocol_id",
                        column: x => x.protocol_id,
                        principalTable: "rehab_protocols",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "template_items",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    template_id = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    exercise_id = table.Column<int>(type: "integer", nullable: true),
                    custom_exercise_title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    params_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: instance-specific parameters"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_template_items", x => x.id);
                    table.ForeignKey(
                        name: "FK_template_items_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_template_items_templates_template_id",
                        column: x => x.template_id,
                        principalTable: "templates",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workout_plans",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    source_template_id = table.Column<int>(type: "integer", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workout_plans", x => x.id);
                    table.ForeignKey(
                        name: "FK_workout_plans_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workout_plans_templates_source_template_id",
                        column: x => x.source_template_id,
                        principalTable: "templates",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_workout_plans_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "condition_test_recommendations",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    condition_id = table.Column<int>(type: "integer", nullable: false),
                    test_definition_id = table.Column<int>(type: "integer", nullable: false),
                    recommended_stage_code = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    priority = table.Column<int>(type: "integer", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_condition_test_recommendations", x => x.id);
                    table.ForeignKey(
                        name: "FK_condition_test_recommendations_conditions_condition_id",
                        column: x => x.condition_id,
                        principalTable: "conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_condition_test_recommendations_test_definitions_test_defini~",
                        column: x => x.test_definition_id,
                        principalTable: "test_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_media",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exercise_id = table.Column<int>(type: "integer", nullable: false),
                    media_asset_id = table.Column<int>(type: "integer", nullable: false),
                    purpose = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercise_media_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercise_media_media_assets_media_asset_id",
                        column: x => x.media_asset_id,
                        principalTable: "media_assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "patient_stage_status",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_condition_id = table.Column<int>(type: "integer", nullable: false),
                    protocol_id = table.Column<int>(type: "integer", nullable: false),
                    current_stage_id = table.Column<int>(type: "integer", nullable: false),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    changed_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    changed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_patient_stage_status", x => x.id);
                    table.ForeignKey(
                        name: "FK_patient_stage_status_patient_conditions_patient_condition_id",
                        column: x => x.patient_condition_id,
                        principalTable: "patient_conditions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_stage_status_rehab_protocols_protocol_id",
                        column: x => x.protocol_id,
                        principalTable: "rehab_protocols",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_stage_status_rehab_stages_current_stage_id",
                        column: x => x.current_stage_id,
                        principalTable: "rehab_stages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_patient_stage_status_users_changed_by_user_id",
                        column: x => x.changed_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "stage_criteria",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    stage_id = table.Column<int>(type: "integer", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    criteria_json = table.Column<string>(type: "text", nullable: false, comment: "JSON: rule definitions (pain/rom/tests)"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_stage_criteria", x => x.id);
                    table.ForeignKey(
                        name: "FK_stage_criteria_rehab_stages_stage_id",
                        column: x => x.stage_id,
                        principalTable: "rehab_stages",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "workout_sessions",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workout_plan_id = table.Column<int>(type: "integer", nullable: true),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    scheduled_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    completed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    status = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workout_sessions", x => x.id);
                    table.ForeignKey(
                        name: "FK_workout_sessions_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_workout_sessions_workout_plans_workout_plan_id",
                        column: x => x.workout_plan_id,
                        principalTable: "workout_plans",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "progress_entries",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    workout_session_id = table.Column<int>(type: "integer", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    recorded_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    pain_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: pain assessment per region"),
                    rom_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: ROM measurements"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_progress_entries", x => x.id);
                    table.ForeignKey(
                        name: "FK_progress_entries_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_progress_entries_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_progress_entries_workout_sessions_workout_session_id",
                        column: x => x.workout_session_id,
                        principalTable: "workout_sessions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "test_results",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    patient_condition_id = table.Column<int>(type: "integer", nullable: true),
                    test_definition_id = table.Column<int>(type: "integer", nullable: false),
                    workout_session_id = table.Column<int>(type: "integer", nullable: true),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    performed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    score_numeric = table.Column<decimal>(type: "numeric", nullable: true),
                    result_json = table.Column<string>(type: "text", nullable: false, comment: "JSON: structured test result"),
                    notes = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_test_results", x => x.id);
                    table.ForeignKey(
                        name: "FK_test_results_patient_conditions_patient_condition_id",
                        column: x => x.patient_condition_id,
                        principalTable: "patient_conditions",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_test_results_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_results_test_definitions_test_definition_id",
                        column: x => x.test_definition_id,
                        principalTable: "test_definitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_results_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_test_results_workout_sessions_workout_session_id",
                        column: x => x.workout_session_id,
                        principalTable: "workout_sessions",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "workout_exercises",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workout_session_id = table.Column<int>(type: "integer", nullable: false),
                    sort_order = table.Column<int>(type: "integer", nullable: false),
                    exercise_id = table.Column<int>(type: "integer", nullable: true),
                    custom_exercise_title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    instruction_media_id = table.Column<int>(type: "integer", nullable: true),
                    params_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: runtime parameters"),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_workout_exercises", x => x.id);
                    table.ForeignKey(
                        name: "FK_workout_exercises_exercises_exercise_id",
                        column: x => x.exercise_id,
                        principalTable: "exercises",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_workout_exercises_media_assets_instruction_media_id",
                        column: x => x.instruction_media_id,
                        principalTable: "media_assets",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_workout_exercises_workout_sessions_workout_session_id",
                        column: x => x.workout_session_id,
                        principalTable: "workout_sessions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_logs",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    workout_exercise_id = table.Column<int>(type: "integer", nullable: false),
                    patient_id = table.Column<int>(type: "integer", nullable: false),
                    created_by_user_id = table.Column<Guid>(type: "uuid", nullable: false),
                    performed_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    completed = table.Column<bool>(type: "boolean", nullable: false),
                    pain_int = table.Column<int>(type: "integer", nullable: true),
                    rom_value = table.Column<decimal>(type: "numeric", nullable: true),
                    metrics_json = table.Column<string>(type: "text", nullable: true, comment: "JSON: detailed metrics"),
                    patient_comment = table.Column<string>(type: "text", nullable: true),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_logs", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercise_logs_patients_patient_id",
                        column: x => x.patient_id,
                        principalTable: "patients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercise_logs_users_created_by_user_id",
                        column: x => x.created_by_user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercise_logs_workout_exercises_workout_exercise_id",
                        column: x => x.workout_exercise_id,
                        principalTable: "workout_exercises",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "exercise_log_media",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    exercise_log_id = table.Column<int>(type: "integer", nullable: false),
                    media_asset_id = table.Column<int>(type: "integer", nullable: false),
                    created_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_exercise_log_media", x => x.id);
                    table.ForeignKey(
                        name: "FK_exercise_log_media_exercise_logs_exercise_log_id",
                        column: x => x.exercise_log_id,
                        principalTable: "exercise_logs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_exercise_log_media_media_assets_media_asset_id",
                        column: x => x.media_asset_id,
                        principalTable: "media_assets",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_ctr_condition_id",
                table: "condition_test_recommendations",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_ctr_condition_priority",
                table: "condition_test_recommendations",
                columns: new[] { "condition_id", "priority" });

            migrationBuilder.CreateIndex(
                name: "ix_ctr_test_definition_id",
                table: "condition_test_recommendations",
                column: "test_definition_id");

            migrationBuilder.CreateIndex(
                name: "ux_ctr_condition_test",
                table: "condition_test_recommendations",
                columns: new[] { "condition_id", "test_definition_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_conditions_body_region",
                table: "conditions",
                column: "body_region");

            migrationBuilder.CreateIndex(
                name: "ix_conditions_code",
                table: "conditions",
                column: "code");

            migrationBuilder.CreateIndex(
                name: "ix_conditions_title",
                table: "conditions",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_log_media_exercise_log_id",
                table: "exercise_log_media",
                column: "exercise_log_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_log_media_media_asset_id",
                table: "exercise_log_media",
                column: "media_asset_id");

            migrationBuilder.CreateIndex(
                name: "ux_exercise_log_media_log_media",
                table: "exercise_log_media",
                columns: new[] { "exercise_log_id", "media_asset_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_completed",
                table: "exercise_logs",
                column: "completed");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_created_by_user_id",
                table: "exercise_logs",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_exercise_performed_at",
                table: "exercise_logs",
                columns: new[] { "workout_exercise_id", "performed_at" });

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_patient_id",
                table: "exercise_logs",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_patient_performed_at",
                table: "exercise_logs",
                columns: new[] { "patient_id", "performed_at" });

            migrationBuilder.CreateIndex(
                name: "ix_exercise_logs_workout_exercise_id",
                table: "exercise_logs",
                column: "workout_exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_media_exercise_id",
                table: "exercise_media",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercise_media_media_asset_id",
                table: "exercise_media",
                column: "media_asset_id");

            migrationBuilder.CreateIndex(
                name: "ux_exercise_media_exercise_media",
                table: "exercise_media",
                columns: new[] { "exercise_id", "media_asset_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_exercises_body_region",
                table: "exercises",
                column: "body_region");

            migrationBuilder.CreateIndex(
                name: "ix_exercises_owner_user_id",
                table: "exercises",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_exercises_title",
                table: "exercises",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_exercises_visibility",
                table: "exercises",
                column: "visibility");

            migrationBuilder.CreateIndex(
                name: "ix_media_assets_patient_id",
                table: "media_assets",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_media_assets_uploaded_by_user_id",
                table: "media_assets",
                column: "uploaded_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_assignments_patient_id",
                table: "patient_assignments",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_assignments_user_id",
                table: "patient_assignments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "ux_patient_assignments_patient_user",
                table: "patient_assignments",
                columns: new[] { "patient_id", "user_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_patient_conditions_condition_id",
                table: "patient_conditions",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_conditions_created_by_user_id",
                table: "patient_conditions",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_conditions_patient_id",
                table: "patient_conditions",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_conditions_patient_status",
                table: "patient_conditions",
                columns: new[] { "patient_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_patient_stage_status_changed_by_user_id",
                table: "patient_stage_status",
                column: "changed_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_stage_status_current_stage_id",
                table: "patient_stage_status",
                column: "current_stage_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_stage_status_patient_condition_id",
                table: "patient_stage_status",
                column: "patient_condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_patient_stage_status_patient_condition_status",
                table: "patient_stage_status",
                columns: new[] { "patient_condition_id", "status" });

            migrationBuilder.CreateIndex(
                name: "ix_patient_stage_status_protocol_id",
                table: "patient_stage_status",
                column: "protocol_id");

            migrationBuilder.CreateIndex(
                name: "ix_patients_full_name",
                table: "patients",
                column: "full_name");

            migrationBuilder.CreateIndex(
                name: "ux_patients_user_id",
                table: "patients",
                column: "user_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_progress_entries_created_by_user_id",
                table: "progress_entries",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_progress_entries_patient_id",
                table: "progress_entries",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_progress_entries_patient_recorded_at",
                table: "progress_entries",
                columns: new[] { "patient_id", "recorded_at" });

            migrationBuilder.CreateIndex(
                name: "ix_progress_entries_workout_session_id",
                table: "progress_entries",
                column: "workout_session_id");

            migrationBuilder.CreateIndex(
                name: "ix_rehab_protocols_condition_id",
                table: "rehab_protocols",
                column: "condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_rehab_protocols_owner_user_id",
                table: "rehab_protocols",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_rehab_protocols_visibility",
                table: "rehab_protocols",
                column: "visibility");

            migrationBuilder.CreateIndex(
                name: "ix_rehab_stages_protocol_id",
                table: "rehab_stages",
                column: "protocol_id");

            migrationBuilder.CreateIndex(
                name: "ix_rehab_stages_protocol_sort",
                table: "rehab_stages",
                columns: new[] { "protocol_id", "sort_order" });

            migrationBuilder.CreateIndex(
                name: "ux_rehab_stages_protocol_code",
                table: "rehab_stages",
                columns: new[] { "protocol_id", "code" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_stage_criteria_stage_id",
                table: "stage_criteria",
                column: "stage_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_items_exercise_id",
                table: "template_items",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_items_template_id",
                table: "template_items",
                column: "template_id");

            migrationBuilder.CreateIndex(
                name: "ix_template_items_template_sort",
                table: "template_items",
                columns: new[] { "template_id", "sort_order" });

            migrationBuilder.CreateIndex(
                name: "ix_templates_owner_user_id",
                table: "templates",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_templates_visibility",
                table: "templates",
                column: "visibility");

            migrationBuilder.CreateIndex(
                name: "ix_test_definitions_owner_user_id",
                table: "test_definitions",
                column: "owner_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_definitions_title",
                table: "test_definitions",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "ix_test_definitions_visibility",
                table: "test_definitions",
                column: "visibility");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_created_by_user_id",
                table: "test_results",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_patient_condition_id",
                table: "test_results",
                column: "patient_condition_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_patient_id",
                table: "test_results",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_patient_performed_at",
                table: "test_results",
                columns: new[] { "patient_id", "performed_at" });

            migrationBuilder.CreateIndex(
                name: "ix_test_results_test_definition_id",
                table: "test_results",
                column: "test_definition_id");

            migrationBuilder.CreateIndex(
                name: "ix_test_results_workout_session_id",
                table: "test_results",
                column: "workout_session_id");

            migrationBuilder.CreateIndex(
                name: "ix_users_role",
                table: "users",
                column: "role");

            migrationBuilder.CreateIndex(
                name: "ix_users_status",
                table: "users",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ux_users_email",
                table: "users",
                column: "email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_visit_notes_author_user_id",
                table: "visit_notes",
                column: "author_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_visit_notes_patient_date",
                table: "visit_notes",
                columns: new[] { "patient_id", "visit_date" });

            migrationBuilder.CreateIndex(
                name: "ix_visit_notes_patient_id",
                table: "visit_notes",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_exercise_id",
                table: "workout_exercises",
                column: "exercise_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_instruction_media_id",
                table: "workout_exercises",
                column: "instruction_media_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_session_sort",
                table: "workout_exercises",
                columns: new[] { "workout_session_id", "sort_order" });

            migrationBuilder.CreateIndex(
                name: "ix_workout_exercises_workout_session_id",
                table: "workout_exercises",
                column: "workout_session_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_plans_created_by_user_id",
                table: "workout_plans",
                column: "created_by_user_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_plans_patient_id",
                table: "workout_plans",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_plans_source_template_id",
                table: "workout_plans",
                column: "source_template_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_plans_status",
                table: "workout_plans",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_workout_sessions_patient_id",
                table: "workout_sessions",
                column: "patient_id");

            migrationBuilder.CreateIndex(
                name: "ix_workout_sessions_patient_scheduled_at",
                table: "workout_sessions",
                columns: new[] { "patient_id", "scheduled_at" });

            migrationBuilder.CreateIndex(
                name: "ix_workout_sessions_status",
                table: "workout_sessions",
                column: "status");

            migrationBuilder.CreateIndex(
                name: "ix_workout_sessions_workout_plan_id",
                table: "workout_sessions",
                column: "workout_plan_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "condition_test_recommendations");

            migrationBuilder.DropTable(
                name: "exercise_log_media");

            migrationBuilder.DropTable(
                name: "exercise_media");

            migrationBuilder.DropTable(
                name: "patient_assignments");

            migrationBuilder.DropTable(
                name: "patient_stage_status");

            migrationBuilder.DropTable(
                name: "progress_entries");

            migrationBuilder.DropTable(
                name: "stage_criteria");

            migrationBuilder.DropTable(
                name: "template_items");

            migrationBuilder.DropTable(
                name: "test_results");

            migrationBuilder.DropTable(
                name: "visit_notes");

            migrationBuilder.DropTable(
                name: "exercise_logs");

            migrationBuilder.DropTable(
                name: "rehab_stages");

            migrationBuilder.DropTable(
                name: "patient_conditions");

            migrationBuilder.DropTable(
                name: "test_definitions");

            migrationBuilder.DropTable(
                name: "workout_exercises");

            migrationBuilder.DropTable(
                name: "rehab_protocols");

            migrationBuilder.DropTable(
                name: "exercises");

            migrationBuilder.DropTable(
                name: "media_assets");

            migrationBuilder.DropTable(
                name: "workout_sessions");

            migrationBuilder.DropTable(
                name: "conditions");

            migrationBuilder.DropTable(
                name: "workout_plans");

            migrationBuilder.DropTable(
                name: "patients");

            migrationBuilder.DropTable(
                name: "templates");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
