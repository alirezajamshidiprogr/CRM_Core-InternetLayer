using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class UpdateTableReminder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.CreateTable(
                name: "Reminder",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReminderTitle = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    F_RegisterDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_RegisterDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    F_EditDate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    M_EditDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    IsRepeatReminder = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    ToPersonelId = table.Column<int>(type: "int", nullable: true),
                    ReminderDayDetailId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reminder", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reminder_People_ToPersonelId",
                        column: x => x.ToPersonelId,
                        principalTable: "People",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Reminder_ReminderDayDetails_ReminderDayDetailId",
                        column: x => x.ReminderDayDetailId,
                        principalTable: "ReminderDayDetails",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_ReminderDayDetailId",
                table: "Reminder",
                column: "ReminderDayDetailId");

            migrationBuilder.CreateIndex(
                name: "IX_Reminder_ToPersonelId",
                table: "Reminder",
                column: "ToPersonelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reminder");

            migrationBuilder.DropTable(
                name: "ReminderDayDetails");
        }
    }
}
