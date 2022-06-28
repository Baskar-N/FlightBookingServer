using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApiService.Migrations
{
    public partial class addconstraintdiscount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DiscountRecId",
                table: "Booking",
                nullable: true,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Booking_DiscountRecId",
                table: "Booking",
                column: "DiscountRecId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking",
                column: "DiscountRecId",
                principalTable: "Discount",
                principalColumn: "DiscountRecId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Discount_DiscountRecId",
                table: "Booking");

            migrationBuilder.DropIndex(
                name: "IX_Booking_DiscountRecId",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "DiscountRecId",
                table: "Booking");
        }
    }
}
