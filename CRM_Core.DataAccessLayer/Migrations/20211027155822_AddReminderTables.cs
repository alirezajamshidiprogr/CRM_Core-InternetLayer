using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class AddReminderTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ReminderDayDetails",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Day = table.Column<int>(type: "int", nullable: false),
                    ReminderId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReminderDayDetails", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReminderDayDetails_Reminder_ReminderId",
                        column: x => x.ReminderId,
                        principalTable: "Reminder",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReminderDayDetails_ReminderId",
                table: "ReminderDayDetails",
                column: "ReminderId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReminderDayDetails");
        }
    }
}
