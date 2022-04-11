using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class AddTblsCheques : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "Salary",
                table: "ClerkServices",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateTable(
                name: "Cheque",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationId = table.Column<int>(type: "int", nullable: false),
                    TBASPayTypeId = table.Column<int>(type: "int", nullable: false),
                    SystemCode = table.Column<string>(type: "nvarchar(15)", maxLength: 15, nullable: true),
                    M_RegisterDate = table.Column<DateTime>(type: "Date", nullable: false),
                    P_RegisterDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    M_EditChequeDate = table.Column<DateTime>(type: "Date", nullable: true),
                    P_EditChequeDate = table.Column<string>(type: "nvarchar(10)", maxLength: 10, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cheque", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Cheque_Reservation_ReservationId",
                        column: x => x.ReservationId,
                        principalTable: "Reservation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Cheque_TBASPayType_TBASPayTypeId",
                        column: x => x.TBASPayTypeId,
                        principalTable: "TBASPayType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ChequeDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ChequeId = table.Column<int>(type: "int", nullable: false),
                    Discount = table.Column<int>(type: "int", nullable: false),
                    TotalPriceCheque = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    FinalPriceCheque = table.Column<decimal>(type: "decimal(18,2)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ChequeDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ChequeDetails_Cheque_ChequeId",
                        column: x => x.ChequeId,
                        principalTable: "Cheque",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Cheque_ReservationId",
                table: "Cheque",
                column: "ReservationId");

            migrationBuilder.CreateIndex(
                name: "IX_Cheque_TBASPayTypeId",
                table: "Cheque",
                column: "TBASPayTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ChequeDetails_ChequeId",
                table: "ChequeDetails",
                column: "ChequeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ChequeDetails");

            migrationBuilder.DropTable(
                name: "Cheque");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "ClerkServices");
        }
    }
}
