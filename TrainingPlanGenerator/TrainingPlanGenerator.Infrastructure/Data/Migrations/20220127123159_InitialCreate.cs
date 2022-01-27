using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlanGenerator.Infrastructure.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Excersises",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Excersises", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TrainingPlans",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TrainingPlans", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ExcersiseTrainingPlan",
                columns: table => new
                {
                    ExcersisesId = table.Column<int>(type: "int", nullable: false),
                    TrainingPlansId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ExcersiseTrainingPlan", x => new { x.ExcersisesId, x.TrainingPlansId });
                    table.ForeignKey(
                        name: "FK_ExcersiseTrainingPlan_Excersises_ExcersisesId",
                        column: x => x.ExcersisesId,
                        principalTable: "Excersises",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ExcersiseTrainingPlan_TrainingPlans_TrainingPlansId",
                        column: x => x.TrainingPlansId,
                        principalTable: "TrainingPlans",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ExcersiseTrainingPlan_TrainingPlansId",
                table: "ExcersiseTrainingPlan",
                column: "TrainingPlansId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ExcersiseTrainingPlan");

            migrationBuilder.DropTable(
                name: "Excersises");

            migrationBuilder.DropTable(
                name: "TrainingPlans");
        }
    }
}
