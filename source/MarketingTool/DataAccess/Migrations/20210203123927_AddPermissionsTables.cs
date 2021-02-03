using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccess.Migrations
{
    public partial class AddPermissionsTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Templates",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "Archived",
                table: "Lists",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Permissions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Permissions", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Permissions",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[,]
                {
                    { 1, "All permissions are switched on", "Global" },
                    { 2, "Allow user to import new recipients", "Import Recipients" },
                    { 3, "Allow user to edit the recipient schema", "Edit Schema" },
                    { 4, "User can create new campaigns", "Add Campaigns" },
                    { 5, "User can edit existing campaigns", "Edit Campaigns" },
                    { 6, "User can create new templates", "Add Templates" },
                    { 7, "User can edit existing templates", "Edit Templates" },
                    { 8, "User can create new lists", "Add Lists" },
                    { 9, "User can edit existing lists", "Edit Lists" },
                    { 10, "User can add new recipients", "Add Recipients" },
                    { 11, "User can edit existing recipients", "Edit Recipients" },
                    { 12, "User can create new template synonyms", "Add Template Synonyms" },
                    { 13, "User can edit existing template synonyms", "Edit Template Synonyms" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Permissions");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Templates");

            migrationBuilder.DropColumn(
                name: "Archived",
                table: "Lists");
        }
    }
}
