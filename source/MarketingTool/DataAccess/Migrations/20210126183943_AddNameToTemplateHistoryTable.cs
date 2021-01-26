using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddNameToTemplateHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "TemplateHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TemplateHistory_Templates_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropIndex(
                name: "IX_TemplateHistory_TemplateId",
                table: "TemplateHistory");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "TemplateHistory");
        }
    }
}
