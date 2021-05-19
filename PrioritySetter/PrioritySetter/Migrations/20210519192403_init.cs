using Microsoft.EntityFrameworkCore.Migrations;

namespace PrioritySetter.Migrations
{
    public partial class init : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DicPriorityLevels",
                columns: table => new
                {
                    PriorityLevelId = table.Column<int>(type: "INTEGER", nullable: false),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DicPriorityLevels", x => x.PriorityLevelId);
                });

            migrationBuilder.CreateTable(
                name: "ErrorPriorities",
                columns: table => new
                {
                    Error = table.Column<string>(type: "TEXT", nullable: false),
                    PriorityLevelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ErrorPriorities", x => x.Error);
                    table.ForeignKey(
                        name: "FK_ErrorPriorities_DicPriorityLevels_PriorityLevelId",
                        column: x => x.PriorityLevelId,
                        principalTable: "DicPriorityLevels",
                        principalColumn: "PriorityLevelId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "DicPriorityLevels",
                columns: new[] { "PriorityLevelId", "Description", "Name" },
                values: new object[] { 2, "Do it", "Normal" });

            migrationBuilder.InsertData(
                table: "DicPriorityLevels",
                columns: new[] { "PriorityLevelId", "Description", "Name" },
                values: new object[] { 3, "Do it now!", "High" });

            migrationBuilder.InsertData(
                table: "DicPriorityLevels",
                columns: new[] { "PriorityLevelId", "Description", "Name" },
                values: new object[] { 1, "Maybe later", "Low" });

            migrationBuilder.CreateIndex(
                name: "IX_ErrorPriorities_PriorityLevelId",
                table: "ErrorPriorities",
                column: "PriorityLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ErrorPriorities");

            migrationBuilder.DropTable(
                name: "DicPriorityLevels");
        }
    }
}
