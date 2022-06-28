using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingApiService.Migrations
{
    public partial class AddedReturnSheduleRecId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ReturnSheduleRecId",
                table: "Booking",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReturnSheduleRecId",
                table: "Booking");
        }
    }
}
