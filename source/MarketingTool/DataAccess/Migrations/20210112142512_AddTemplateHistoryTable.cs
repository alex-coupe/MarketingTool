using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddTemplateHistoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TemplateHistory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TemplateId = table.Column<int>(type: "int", nullable: false),
                    Content = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CreatorId = table.Column<int>(type: "int", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Version = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TemplateHistory", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TemplateHistory");
        }
    }
}
