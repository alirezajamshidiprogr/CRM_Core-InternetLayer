using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class editReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_People_ClerkId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TBASServices_ServiceId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ClerkId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_ServiceId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ClerkId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "Reservation");

            migrationBuilder.AddColumn<string>(
                name: "SystemCode",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SystemCode",
                table: "Reservation");

            migrationBuilder.AddColumn<int>(
                name: "ClerkId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ClerkId",
                table: "Reservation",
                column: "ClerkId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_ServiceId",
                table: "Reservation",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_People_ClerkId",
                table: "Reservation",
                column: "ClerkId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TBASServices_ServiceId",
                table: "Reservation",
                column: "ServiceId",
                principalTable: "TBASServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
