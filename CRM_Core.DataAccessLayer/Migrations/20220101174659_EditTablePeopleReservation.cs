using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditTablePeopleReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TBASPayType_TBASPayTypeId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TBASPayTypeId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Reservation");

            migrationBuilder.AlterColumn<string>(
                name: "SystemCode",
                table: "Reservation",
                type: "nvarchar(15)",
                maxLength: 15,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<DateTime>(
                name: "M_ReservationEditDate",
                table: "Reservation",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "M_ReservationInsertDate",
                table: "Reservation",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "M_EditDate",
                table: "People",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "M_InsertDate",
                table: "People",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "M_ReservationEditDate",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "M_ReservationInsertDate",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "M_EditDate",
                table: "People");

            migrationBuilder.DropColumn(
                name: "M_InsertDate",
                table: "People");

            migrationBuilder.AlterColumn<string>(
                name: "SystemCode",
                table: "Reservation",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(15)",
                oldMaxLength: 15);

            migrationBuilder.AddColumn<double>(
                name: "Price",
                table: "Reservation",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TBASPayTypeId",
                table: "Reservation",
                column: "TBASPayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TBASPayType_TBASPayTypeId",
                table: "Reservation",
                column: "TBASPayTypeId",
                principalTable: "TBASPayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
