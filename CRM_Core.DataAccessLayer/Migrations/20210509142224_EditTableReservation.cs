using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class EditTableReservation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TBASPayType_PayTypeId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_PayTypeId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "PayTypeId",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "PeopleId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "TBASPayTypeId",
                table: "Reservation",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_TBASPayTypeId",
                table: "Reservation",
                column: "TBASPayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TBASPayType_TBASPayTypeId",
                table: "Reservation",
                column: "TBASPayTypeId",
                principalTable: "TBASPayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation");

            migrationBuilder.DropForeignKey(
                name: "FK_Reservation_TBASPayType_TBASPayTypeId",
                table: "Reservation");

            migrationBuilder.DropIndex(
                name: "IX_Reservation_TBASPayTypeId",
                table: "Reservation");

            migrationBuilder.DropColumn(
                name: "TBASPayTypeId",
                table: "Reservation");

            migrationBuilder.AlterColumn<int>(
                name: "PeopleId",
                table: "Reservation",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "PayTypeId",
                table: "Reservation",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Reservation_PayTypeId",
                table: "Reservation",
                column: "PayTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_People_PeopleId",
                table: "Reservation",
                column: "PeopleId",
                principalTable: "People",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate : ReferentialAction.NoAction);

            migrationBuilder.AddForeignKey(
                name: "FK_Reservation_TBASPayType_PayTypeId",
                table: "Reservation",
                column: "PayTypeId",
                principalTable: "TBASPayType",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction,
                onUpdate: ReferentialAction.NoAction);
        }
    }
}
