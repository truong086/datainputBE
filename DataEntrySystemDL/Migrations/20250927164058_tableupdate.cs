using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataEntrySystemDL.Migrations
{
    public partial class tableupdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "user_id",
                table: "tables",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_tables_user_id",
                table: "tables",
                column: "user_id");

            migrationBuilder.AddForeignKey(
                name: "FK_tables_users_user_id",
                table: "tables",
                column: "user_id",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tables_users_user_id",
                table: "tables");

            migrationBuilder.DropIndex(
                name: "IX_tables_user_id",
                table: "tables");

            migrationBuilder.DropColumn(
                name: "user_id",
                table: "tables");
        }
    }
}
