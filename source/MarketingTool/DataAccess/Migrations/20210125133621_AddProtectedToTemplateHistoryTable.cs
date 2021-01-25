using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddProtectedToTemplateHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.RenameColumn(
                name: "EditedDate",
                table: "TemplateHistory",
                newName: "ModifiedDate");

            migrationBuilder.AddColumn<bool>(
                name: "Protected",
                table: "TemplateHistory",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Protected",
                table: "TemplateHistory");

            migrationBuilder.RenameColumn(
                name: "ModifiedDate",
                table: "TemplateHistory",
                newName: "EditedDate");

            migrationBuilder.CreateIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory",
                column: "TemplateId");

            migrationBuilder.AddForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory",
                column: "TemplateId",
                principalTable: "Templates",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
