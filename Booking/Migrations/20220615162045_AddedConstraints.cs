using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApiService.Migrations
{
    public partial class AddedConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNunber",
                table: "ReturnJourney");

            migrationBuilder.AddColumn<int>(
                name: "SeatNumber",
                table: "ReturnJourney",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TicketCancelDate",
                table: "Booking",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "ReturnJourney");

            migrationBuilder.AddColumn<int>(
                name: "SeatNunber",
                table: "ReturnJourney",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<DateTime>(
                name: "TicketCancelDate",
                table: "Booking",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldNullable: true);
        }
    }
}
