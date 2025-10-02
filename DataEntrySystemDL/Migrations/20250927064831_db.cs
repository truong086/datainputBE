using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntrySystemDL.Migrations
{
    public partial class db : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "roles",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    username = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    password = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    email = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    display_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "projects",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    owner_id = table.Column<int>(type: "int", nullable: true),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_projects", x => x.id);
                    table.ForeignKey(
                        name: "FK_projects_users_owner_id",
                        column: x => x.owner_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "user_Roles",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "int", nullable: false),
                    role_id = table.Column<int>(type: "int", nullable: false),
                    id = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_Roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_user_Roles_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_Roles_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "tables",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    project_id = table.Column<int>(type: "int", nullable: true),
                    name = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    description = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tables", x => x.id);
                    table.ForeignKey(
                        name: "FK_tables_projects_project_id",
                        column: x => x.project_id,
                        principalTable: "projects",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "data_Rows_Eavs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    table_id = table.Column<int>(type: "int", nullable: true),
                    row_uuid = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_Rows_Eavs", x => x.id);
                    table.ForeignKey(
                        name: "FK_data_Rows_Eavs_tables_table_id",
                        column: x => x.table_id,
                        principalTable: "tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_Rows_Eavs_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "data_Rows_Jsons",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    table_id = table.Column<int>(type: "int", nullable: true),
                    row_uuid = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    dynamic_data = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_by = table.Column<int>(type: "int", nullable: true),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_Rows_Jsons", x => x.id);
                    table.ForeignKey(
                        name: "FK_data_Rows_Jsons_tables_table_id",
                        column: x => x.table_id,
                        principalTable: "tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_Rows_Jsons_users_created_by",
                        column: x => x.created_by,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FieldDefinitions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    table_id = table.Column<int>(type: "int", nullable: true),
                    field_key = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    field_name = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    fieldtype = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    field_options = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    is_required = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    sort_order = table.Column<int>(type: "int", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldDefinitions", x => x.id);
                    table.ForeignKey(
                        name: "FK_FieldDefinitions_tables_table_id",
                        column: x => x.table_id,
                        principalTable: "tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "row_Access_Rules",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    table_id = table.Column<int>(type: "int", nullable: true),
                    user_id = table.Column<int>(type: "int", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    filter_conditions = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    access_type = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_row_Access_Rules", x => x.id);
                    table.CheckConstraint("CK_RowAccessRules_UserOrRole", "user_id IS NOT NULL OR role_id IS NOT NULL");
                    table.ForeignKey(
                        name: "FK_row_Access_Rules_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_row_Access_Rules_tables_table_id",
                        column: x => x.table_id,
                        principalTable: "tables",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_row_Access_Rules_users_user_id",
                        column: x => x.user_id,
                        principalTable: "users",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "data_Values_Eavs",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    row_id = table.Column<int>(type: "int", nullable: true),
                    field_id = table.Column<int>(type: "int", nullable: true),
                    field_key = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_text = table.Column<string>(type: "varchar(255)", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    value_number = table.Column<decimal>(type: "decimal(65,30)", nullable: true),
                    value_date = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    value_datetime = table.Column<DateTime>(type: "datetime(6)", nullable: true),
                    ValueBoolean = table.Column<bool>(type: "tinyint(1)", nullable: true),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_data_Values_Eavs", x => x.id);
                    table.ForeignKey(
                        name: "FK_data_Values_Eavs_data_Rows_Eavs_row_id",
                        column: x => x.row_id,
                        principalTable: "data_Rows_Eavs",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_data_Values_Eavs_FieldDefinitions_field_id",
                        column: x => x.field_id,
                        principalTable: "FieldDefinitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "field_Permissions",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    field_id = table.Column<int>(type: "int", nullable: true),
                    role_id = table.Column<int>(type: "int", nullable: true),
                    can_read = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    can_write = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    deleted = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    cretoredit = table.Column<string>(type: "longtext", nullable: true)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    created_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true),
                    updated_at = table.Column<DateTimeOffset>(type: "datetime(6)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_field_Permissions", x => x.id);
                    table.ForeignKey(
                        name: "FK_field_Permissions_FieldDefinitions_field_id",
                        column: x => x.field_id,
                        principalTable: "FieldDefinitions",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_field_Permissions_roles_role_id",
                        column: x => x.role_id,
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "idx_created_at",
                table: "data_Rows_Eavs",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_table_row",
                table: "data_Rows_Eavs",
                columns: new[] { "table_id", "row_uuid" });

            migrationBuilder.CreateIndex(
                name: "IX_data_Rows_Eavs_created_by",
                table: "data_Rows_Eavs",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "idx_created_at",
                table: "data_Rows_Jsons",
                column: "created_at");

            migrationBuilder.CreateIndex(
                name: "idx_table_row",
                table: "data_Rows_Jsons",
                columns: new[] { "table_id", "row_uuid" });

            migrationBuilder.CreateIndex(
                name: "IX_data_Rows_Jsons_created_by",
                table: "data_Rows_Jsons",
                column: "created_by");

            migrationBuilder.CreateIndex(
                name: "idx_field_value_date",
                table: "data_Values_Eavs",
                columns: new[] { "field_id", "value_date" });

            migrationBuilder.CreateIndex(
                name: "idx_field_value_number",
                table: "data_Values_Eavs",
                columns: new[] { "field_id", "value_number" });

            migrationBuilder.CreateIndex(
                name: "idx_field_value_text",
                table: "data_Values_Eavs",
                columns: new[] { "field_id", "value_text" });

            migrationBuilder.CreateIndex(
                name: "unique_row_field",
                table: "data_Values_Eavs",
                columns: new[] { "row_id", "field_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_field_Permissions_role_id",
                table: "field_Permissions",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "unique_field_role",
                table: "field_Permissions",
                columns: new[] { "field_id", "role_id" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "unique_field_key",
                table: "FieldDefinitions",
                columns: new[] { "table_id", "field_key" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_projects_owner_id",
                table: "projects",
                column: "owner_id");

            migrationBuilder.CreateIndex(
                name: "IX_row_Access_Rules_role_id",
                table: "row_Access_Rules",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_row_Access_Rules_table_id",
                table: "row_Access_Rules",
                column: "table_id");

            migrationBuilder.CreateIndex(
                name: "IX_row_Access_Rules_user_id",
                table: "row_Access_Rules",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "unique_table_name",
                table: "tables",
                columns: new[] { "project_id", "name" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_Roles_role_id",
                table: "user_Roles",
                column: "role_id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "data_Rows_Jsons");

            migrationBuilder.DropTable(
                name: "data_Values_Eavs");

            migrationBuilder.DropTable(
                name: "field_Permissions");

            migrationBuilder.DropTable(
                name: "row_Access_Rules");

            migrationBuilder.DropTable(
                name: "user_Roles");

            migrationBuilder.DropTable(
                name: "data_Rows_Eavs");

            migrationBuilder.DropTable(
                name: "FieldDefinitions");

            migrationBuilder.DropTable(
                name: "roles");

            migrationBuilder.DropTable(
                name: "tables");

            migrationBuilder.DropTable(
                name: "projects");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
