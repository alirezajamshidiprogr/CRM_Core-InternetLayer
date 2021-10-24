using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class TbasSalonCostsTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "TBASSalonCosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CostName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CostTitle = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    TBASSalonCostId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TBASSalonCosts", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TBASSalonCosts_TBASSalonCosts_TBASSalonCostId",
                        column: x => x.TBASSalonCostId,
                        principalTable: "TBASSalonCosts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TBASSalonCosts_TBASSalonCostId",
                table: "TBASSalonCosts",
                column: "TBASSalonCostId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TBASSalonCosts");
        }
    }
}
