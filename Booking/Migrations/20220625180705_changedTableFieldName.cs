using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApiService.Migrations
{
    public partial class changedTableFieldName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SeatNunber",
                table: "Journey");

            migrationBuilder.AlterColumn<string>(
                name: "SeatNumber",
                table: "ReturnJourney",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<string>(
                name: "SeatNumber",
                table: "Journey",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "DiscountRecId",
                table: "Booking",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking",
                column: "DiscountRecId",
                principalTable: "Discount",
                principalColumn: "DiscountRecId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "SeatNumber",
                table: "Journey");

            migrationBuilder.AlterColumn<int>(
                name: "SeatNumber",
                table: "ReturnJourney",
                type: "int",
                nullable: false,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "SeatNunber",
                table: "Journey",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "DiscountRecId",
                table: "Booking",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking",
                column: "DiscountRecId",
                principalTable: "Discount",
                principalColumn: "DiscountRecId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
