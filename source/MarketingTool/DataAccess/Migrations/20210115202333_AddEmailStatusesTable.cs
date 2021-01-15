using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddEmailStatusesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EmailStatuses",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmailStatuses", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "EmailStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Pending" });

            migrationBuilder.InsertData(
                table: "EmailStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 2, "Sent" });

            migrationBuilder.InsertData(
                table: "EmailStatuses",
                columns: new[] { "Id", "Name" },
                values: new object[] { 3, "Failed" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmailStatuses");
        }
    }
}
