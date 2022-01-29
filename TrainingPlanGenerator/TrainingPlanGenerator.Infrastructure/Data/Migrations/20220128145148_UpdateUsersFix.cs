using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TrainingPlanGenerator.Infrastructure.Data.Migrations
{
    public partial class UpdateUsersFix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_TrainingPlans_ActiveTrainingPlanId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ActiveTrainingPlanId",
                table: "AppUsers",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_TrainingPlans_ActiveTrainingPlanId",
                table: "AppUsers",
                column: "ActiveTrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AppUsers_TrainingPlans_ActiveTrainingPlanId",
                table: "AppUsers");

            migrationBuilder.AlterColumn<int>(
                name: "ActiveTrainingPlanId",
                table: "AppUsers",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AppUsers_TrainingPlans_ActiveTrainingPlanId",
                table: "AppUsers",
                column: "ActiveTrainingPlanId",
                principalTable: "TrainingPlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
