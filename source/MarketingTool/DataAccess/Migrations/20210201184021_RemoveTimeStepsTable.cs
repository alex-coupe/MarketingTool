using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class RemoveTimeStepsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Timesteps");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Timesteps",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Hours = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Timesteps", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Timesteps",
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
    }
}
