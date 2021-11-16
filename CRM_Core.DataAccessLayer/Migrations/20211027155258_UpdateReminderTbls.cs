using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class UpdateReminderTbls : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Reminder_ReminderDayDetails_ReminderDayDetailId",
                table: "Reminder");

            migrationBuilder.DropTable(
                name: "ReminderDayDetails");

            migrationBuilder.DropIndex(
                name: "IX_Reminder_ReminderDayDetailId",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "M_RegisterDate",
                table: "Reminder");

            migrationBuilder.DropColumn(
                name: "ReminderDayDetailId",
                table: "Reminder");

            migrationBuilder.RenameColumn(
                name: "F_RegisterDate",
                table: "Reminder",
                newName: "F_ReminderDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "M_EditDate",
                table: "Reminder",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AddColumn<DateTime>(
                name: "M_ReminderDate",
                table: "Reminder",
                type: "datetime2",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "M_ReminderDate",
                table: "Reminder");

            migrationBuilder.RenameColumn(
                name: "F_ReminderDate",
                table: "Reminder",
                newName: "F_RegisterDate");

            migrationBuilder.AlterColumn<DateTime>(
                name: "M_EditDate",
                table: "Reminder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "M_RegisterDate",
                table: "Reminder",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "ReminderDayDetailId",
                table: "Reminder",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "ReminderDayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderDayDetails", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_ReminderDayDetailId",
                table: "Reminder",
                column: "ReminderDayDetailId");

            migrationBuilder.AddForeignKey(
                name: "FK_Reminder_ReminderDayDetails_ReminderDayDetailId",
                table: "Reminder",
                column: "ReminderDayDetailId",
                principalTable: "ReminderDayDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
