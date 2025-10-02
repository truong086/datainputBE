using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntrySystemDL.Migrations
{
    public partial class dbupdatetoi0210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_data_Rows_Eavs_tables_table_id",
                table: "data_Rows_Eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_Rows_Eavs_users_created_by",
                table: "data_Rows_Eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_Rows_Jsons_tables_table_id",
                table: "data_Rows_Jsons");

            migrationBuilder.DropForeignKey(
                name: "FK_data_Rows_Jsons_users_created_by",
                table: "data_Rows_Jsons");

            migrationBuilder.DropForeignKey(
                name: "FK_data_Values_Eavs_data_Rows_Eavs_row_id",
                table: "data_Values_Eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_Values_Eavs_FieldDefinitions_field_id",
                table: "data_Values_Eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_field_Permissions_FieldDefinitions_field_id",
                table: "field_Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_field_Permissions_roles_role_id",
                table: "field_Permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_FieldDefinitions_tables_table_id",
                table: "FieldDefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_row_Access_Rules_roles_role_id",
                table: "row_Access_Rules");

            migrationBuilder.DropForeignKey(
                name: "FK_row_Access_Rules_tables_table_id",
                table: "row_Access_Rules");

            migrationBuilder.DropForeignKey(
                name: "FK_row_Access_Rules_users_user_id",
                table: "row_Access_Rules");

            migrationBuilder.DropForeignKey(
                name: "FK_user_Roles_roles_role_id",
                table: "user_Roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_Roles_users_user_id",
                table: "user_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_Roles",
                table: "user_Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_row_Access_Rules",
                table: "row_Access_Rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FieldDefinitions",
                table: "FieldDefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_field_Permissions",
                table: "field_Permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_Values_Eavs",
                table: "data_Values_Eavs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_Rows_Jsons",
                table: "data_Rows_Jsons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_Rows_Eavs",
                table: "data_Rows_Eavs");

            migrationBuilder.RenameTable(
                name: "user_Roles",
                newName: "user_roles");

            migrationBuilder.RenameTable(
                name: "row_Access_Rules",
                newName: "row_access_rules");

            migrationBuilder.RenameTable(
                name: "FieldDefinitions",
                newName: "fielddefinitions");

            migrationBuilder.RenameTable(
                name: "field_Permissions",
                newName: "field_permissions");

            migrationBuilder.RenameTable(
                name: "data_Values_Eavs",
                newName: "data_values_eavs");

            migrationBuilder.RenameTable(
                name: "data_Rows_Jsons",
                newName: "data_rows_jsons");

            migrationBuilder.RenameTable(
                name: "data_Rows_Eavs",
                newName: "data_rows_eavs");

            migrationBuilder.RenameIndex(
                name: "IX_user_Roles_role_id",
                table: "user_roles",
                newName: "IX_user_roles_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_Access_Rules_user_id",
                table: "row_access_rules",
                newName: "IX_row_access_rules_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_Access_Rules_table_id",
                table: "row_access_rules",
                newName: "IX_row_access_rules_table_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_Access_Rules_role_id",
                table: "row_access_rules",
                newName: "IX_row_access_rules_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_field_Permissions_role_id",
                table: "field_permissions",
                newName: "IX_field_permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_data_Rows_Jsons_created_by",
                table: "data_rows_jsons",
                newName: "IX_data_rows_jsons_created_by");

            migrationBuilder.RenameIndex(
                name: "IX_data_Rows_Eavs_created_by",
                table: "data_rows_eavs",
                newName: "IX_data_rows_eavs_created_by");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_row_access_rules",
                table: "row_access_rules",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_fielddefinitions",
                table: "fielddefinitions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_field_permissions",
                table: "field_permissions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_values_eavs",
                table: "data_values_eavs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_rows_jsons",
                table: "data_rows_jsons",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_rows_eavs",
                table: "data_rows_eavs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_data_rows_eavs_tables_table_id",
                table: "data_rows_eavs",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_rows_eavs_users_created_by",
                table: "data_rows_eavs",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_rows_jsons_tables_table_id",
                table: "data_rows_jsons",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_rows_jsons_users_created_by",
                table: "data_rows_jsons",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_values_eavs_data_rows_eavs_row_id",
                table: "data_values_eavs",
                column: "row_id",
                principalTable: "data_rows_eavs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_values_eavs_fielddefinitions_field_id",
                table: "data_values_eavs",
                column: "field_id",
                principalTable: "fielddefinitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_field_permissions_fielddefinitions_field_id",
                table: "field_permissions",
                column: "field_id",
                principalTable: "fielddefinitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_field_permissions_roles_role_id",
                table: "field_permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_fielddefinitions_tables_table_id",
                table: "fielddefinitions",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_access_rules_roles_role_id",
                table: "row_access_rules",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_access_rules_tables_table_id",
                table: "row_access_rules",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_access_rules_users_user_id",
                table: "row_access_rules",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_roles_role_id",
                table: "user_roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_roles_users_user_id",
                table: "user_roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_data_rows_eavs_tables_table_id",
                table: "data_rows_eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_rows_eavs_users_created_by",
                table: "data_rows_eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_rows_jsons_tables_table_id",
                table: "data_rows_jsons");

            migrationBuilder.DropForeignKey(
                name: "FK_data_rows_jsons_users_created_by",
                table: "data_rows_jsons");

            migrationBuilder.DropForeignKey(
                name: "FK_data_values_eavs_data_rows_eavs_row_id",
                table: "data_values_eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_data_values_eavs_fielddefinitions_field_id",
                table: "data_values_eavs");

            migrationBuilder.DropForeignKey(
                name: "FK_field_permissions_fielddefinitions_field_id",
                table: "field_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_field_permissions_roles_role_id",
                table: "field_permissions");

            migrationBuilder.DropForeignKey(
                name: "FK_fielddefinitions_tables_table_id",
                table: "fielddefinitions");

            migrationBuilder.DropForeignKey(
                name: "FK_row_access_rules_roles_role_id",
                table: "row_access_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_row_access_rules_tables_table_id",
                table: "row_access_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_row_access_rules_users_user_id",
                table: "row_access_rules");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_roles_role_id",
                table: "user_roles");

            migrationBuilder.DropForeignKey(
                name: "FK_user_roles_users_user_id",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_user_roles",
                table: "user_roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_row_access_rules",
                table: "row_access_rules");

            migrationBuilder.DropPrimaryKey(
                name: "PK_fielddefinitions",
                table: "fielddefinitions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_field_permissions",
                table: "field_permissions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_values_eavs",
                table: "data_values_eavs");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_rows_jsons",
                table: "data_rows_jsons");

            migrationBuilder.DropPrimaryKey(
                name: "PK_data_rows_eavs",
                table: "data_rows_eavs");

            migrationBuilder.RenameTable(
                name: "user_roles",
                newName: "user_Roles");

            migrationBuilder.RenameTable(
                name: "row_access_rules",
                newName: "row_Access_Rules");

            migrationBuilder.RenameTable(
                name: "fielddefinitions",
                newName: "FieldDefinitions");

            migrationBuilder.RenameTable(
                name: "field_permissions",
                newName: "field_Permissions");

            migrationBuilder.RenameTable(
                name: "data_values_eavs",
                newName: "data_Values_Eavs");

            migrationBuilder.RenameTable(
                name: "data_rows_jsons",
                newName: "data_Rows_Jsons");

            migrationBuilder.RenameTable(
                name: "data_rows_eavs",
                newName: "data_Rows_Eavs");

            migrationBuilder.RenameIndex(
                name: "IX_user_roles_role_id",
                table: "user_Roles",
                newName: "IX_user_Roles_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_access_rules_user_id",
                table: "row_Access_Rules",
                newName: "IX_row_Access_Rules_user_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_access_rules_table_id",
                table: "row_Access_Rules",
                newName: "IX_row_Access_Rules_table_id");

            migrationBuilder.RenameIndex(
                name: "IX_row_access_rules_role_id",
                table: "row_Access_Rules",
                newName: "IX_row_Access_Rules_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_field_permissions_role_id",
                table: "field_Permissions",
                newName: "IX_field_Permissions_role_id");

            migrationBuilder.RenameIndex(
                name: "IX_data_rows_jsons_created_by",
                table: "data_Rows_Jsons",
                newName: "IX_data_Rows_Jsons_created_by");

            migrationBuilder.RenameIndex(
                name: "IX_data_rows_eavs_created_by",
                table: "data_Rows_Eavs",
                newName: "IX_data_Rows_Eavs_created_by");

            migrationBuilder.AddPrimaryKey(
                name: "PK_user_Roles",
                table: "user_Roles",
                columns: new[] { "user_id", "role_id" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_row_Access_Rules",
                table: "row_Access_Rules",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FieldDefinitions",
                table: "FieldDefinitions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_field_Permissions",
                table: "field_Permissions",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_Values_Eavs",
                table: "data_Values_Eavs",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_Rows_Jsons",
                table: "data_Rows_Jsons",
                column: "id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_data_Rows_Eavs",
                table: "data_Rows_Eavs",
                column: "id");

            migrationBuilder.AddForeignKey(
                name: "FK_data_Rows_Eavs_tables_table_id",
                table: "data_Rows_Eavs",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_Rows_Eavs_users_created_by",
                table: "data_Rows_Eavs",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_Rows_Jsons_tables_table_id",
                table: "data_Rows_Jsons",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_Rows_Jsons_users_created_by",
                table: "data_Rows_Jsons",
                column: "created_by",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_Values_Eavs_data_Rows_Eavs_row_id",
                table: "data_Values_Eavs",
                column: "row_id",
                principalTable: "data_Rows_Eavs",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_data_Values_Eavs_FieldDefinitions_field_id",
                table: "data_Values_Eavs",
                column: "field_id",
                principalTable: "FieldDefinitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_field_Permissions_FieldDefinitions_field_id",
                table: "field_Permissions",
                column: "field_id",
                principalTable: "FieldDefinitions",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_field_Permissions_roles_role_id",
                table: "field_Permissions",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_FieldDefinitions_tables_table_id",
                table: "FieldDefinitions",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_Access_Rules_roles_role_id",
                table: "row_Access_Rules",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_Access_Rules_tables_table_id",
                table: "row_Access_Rules",
                column: "table_id",
                principalTable: "tables",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_row_Access_Rules_users_user_id",
                table: "row_Access_Rules",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_Roles_roles_role_id",
                table: "user_Roles",
                column: "role_id",
                principalTable: "roles",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_user_Roles_users_user_id",
                table: "user_Roles",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
