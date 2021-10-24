using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class RelationTbasSlonCostBySalonCost : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TBASSalonCostId",
                table: "SalonCosts",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_SalonCosts_TBASSalonCostId",
                table: "SalonCosts",
                column: "TBASSalonCostId");

            migrationBuilder.AddForeignKey(
                name: "FK_SalonCosts_TBASSalonCosts_TBASSalonCostId",
                table: "SalonCosts",
                column: "TBASSalonCostId",
                principalTable: "TBASSalonCosts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SalonCosts_TBASSalonCosts_TBASSalonCostId",
                table: "SalonCosts");

            migrationBuilder.DropIndex(
                name: "IX_SalonCosts_TBASSalonCostId",
                table: "SalonCosts");

            migrationBuilder.DropColumn(
                name: "TBASSalonCostId",
                table: "SalonCosts");
        }
    }
}
