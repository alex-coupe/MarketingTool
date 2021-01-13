using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddTimestepTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TemplateHistory",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "TimeSteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Hours = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TimeSteps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "TimeSteps",
                columns: new[] { "Id", "Hours", "Name" },
                values: new object[,]
                {
                    { 1, 0, "ASAP" },
                    { 2, 1, "Hourly" },
                    { 3, 24, "Daily" },
                    { 4, 168, "Weekly" },
                    { 5, 336, "Bi-Weekly" },
                    { 6, 672, "4 Weekly" },
                    { 7, 730, "Monthly" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TimeSteps");

            migrationBuilder.AlterColumn<string>(
                name: "Content",
                table: "TemplateHistory",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");
        }
    }
}
