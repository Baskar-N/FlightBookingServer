using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApiService.Migrations
{
    public partial class InitialSetupBooking : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Booking",
                columns: table => new
                {
                    BookingRecId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ScheduleRecId = table.Column<int>(nullable: false),
                    EmailId = table.Column<string>(maxLength: 50, nullable: false),
                    Name = table.Column<string>(maxLength: 50, nullable: false),
                    NumberOfSeats = table.Column<int>(nullable: false),
                    IsBcs = table.Column<bool>(nullable: false),
                    TicketPnr = table.Column<string>(maxLength: 20, nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    CreatedDate = table.Column<DateTime>(nullable: false),
                    TicketCancelDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Booking", x => x.BookingRecId);
                    table.ForeignKey(
                        name: "FK_Booking_Schedule_ScheduleRecId",
                        column: x => x.ScheduleRecId,
                        principalTable: "Schedule",
                        principalColumn: "ScheduleRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Passenger",
                columns: table => new
                {
                    PassengerRecId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    BookingRecId = table.Column<int>(nullable: false),
                    Name = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Passenger", x => x.PassengerRecId);
                    table.ForeignKey(
                        name: "FK_Passenger_Booking_BookingRecId",
                        column: x => x.BookingRecId,
                        principalTable: "Booking",
                        principalColumn: "BookingRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Journey",
                columns: table => new
                {
                    JourneyRecId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerRecId = table.Column<int>(nullable: false),
                    MealTypeRecId = table.Column<int>(nullable: false),
                    SeatNunber = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Journey", x => x.JourneyRecId);
                    table.ForeignKey(
                        name: "FK_Journey_Passenger_PassengerRecId",
                        column: x => x.PassengerRecId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ReturnJourney",
                columns: table => new
                {
                    ReturnJourneyRecId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PassengerRecId = table.Column<int>(nullable: false),
                    MealTypeRecId = table.Column<int>(nullable: false),
                    SeatNunber = table.Column<int>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReturnJourney", x => x.ReturnJourneyRecId);
                    table.ForeignKey(
                        name: "FK_ReturnJourney_Passenger_PassengerRecId",
                        column: x => x.PassengerRecId,
                        principalTable: "Passenger",
                        principalColumn: "PassengerRecId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Booking_ScheduleRecId",
                table: "Booking",
                column: "ScheduleRecId");

            migrationBuilder.CreateIndex(
                name: "IX_Journey_PassengerRecId",
                table: "Journey",
                column: "PassengerRecId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Passenger_BookingRecId",
                table: "Passenger",
                column: "BookingRecId");

            migrationBuilder.CreateIndex(
                name: "IX_ReturnJourney_PassengerRecId",
                table: "ReturnJourney",
                column: "PassengerRecId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Journey");

            migrationBuilder.DropTable(
                name: "ReturnJourney");

            migrationBuilder.DropTable(
                name: "Passenger");

            migrationBuilder.DropTable(
                name: "Booking");
        }
    }
}
