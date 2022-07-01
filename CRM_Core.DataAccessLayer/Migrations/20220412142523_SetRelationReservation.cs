using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class SetRelationReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TelPhones",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500);

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "TelPhones",
                type: "nvarchar(500)",
                maxLength: 500,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(500)",
                oldMaxLength: 500,
                oldNullable: true);

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
    }
}
