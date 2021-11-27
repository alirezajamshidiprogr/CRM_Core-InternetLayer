using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CRM_Core.DataAccessLayer.Migrations
{
    public partial class UpdatePeopleDateTimeToDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "M_MariedDate",
                table: "People",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "M_Birthday",
                table: "People",
                type: "Date",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "M_MariedDate",
                table: "People",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "M_Birthday",
                table: "People",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "Date",
                oldNullable: true);
        }
    }
}
