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
                name: "AppPriorities",
                columns: table => new
                {
                    App = table.Column<string>(type: "TEXT", nullable: false),
                    PriorityLevelId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppPriorities", x => x.App);
                    table.ForeignKey(
                        name: "FK_AppPriorities_DicPriorityLevels_PriorityLevelId",
                        column: x => x.PriorityLevelId,
                        principalTable: "DicPriorityLevels",
                        principalColumn: "PriorityLevelId",
                        onDelete: ReferentialAction.Cascade);
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

            migrationBuilder.InsertData(
                table: "AppPriorities",
                columns: new[] { "App", "PriorityLevelId" },
                values: new object[] { "Glob", 1 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "NullReferenceException", 3 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "ArgumentNullException", 3 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "OutOfMemoryException", 3 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "MissingFieldException", 1 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "MissingMemberException", 1 });

            migrationBuilder.InsertData(
                table: "ErrorPriorities",
                columns: new[] { "Error", "PriorityLevelId" },
                values: new object[] { "MissingMethodException", 1 });

            migrationBuilder.CreateIndex(
                name: "IX_AppPriorities_PriorityLevelId",
                table: "AppPriorities",
                column: "PriorityLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_ErrorPriorities_PriorityLevelId",
                table: "ErrorPriorities",
                column: "PriorityLevelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AppPriorities");

            migrationBuilder.DropTable(
                name: "ErrorPriorities");

            migrationBuilder.DropTable(
                name: "DicPriorityLevels");
        }
    }
}
