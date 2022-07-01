using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class SetRelationReservations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_ReservationDetails_ReservationDetailsId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ReservationDetailsId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ReservationDetailsId",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ReservationId",
                table: "ReservationDetails",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ReservationDetails_ReservationId",
                table: "ReservationDetails",
                column: "ReservationId");

            migrationBuilder.AddForeignKey(
                name: "FK_ReservationDetails_Reservation_ReservationId",
                table: "ReservationDetails",
                column: "ReservationId",
                principalTable: "Reservation",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ReservationDetails_Reservation_ReservationId",
                table: "ReservationDetails");

            migrationBuilder.DropIndex(
                name: "IX_ReservationDetails_ReservationId",
                table: "ReservationDetails");

            migrationBuilder.DropColumn(
                name: "ReservationId",
                table: "ReservationDetails");

            migrationBuilder.AddColumn<int>(
                name: "ReservationDetailsId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ReservationDetailsId",
                table: "Reservation",
                column: "ReservationDetailsId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_ReservationDetails_ReservationDetailsId",
                table: "Reservation",
                column: "ReservationDetailsId",
                principalTable: "ReservationDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
