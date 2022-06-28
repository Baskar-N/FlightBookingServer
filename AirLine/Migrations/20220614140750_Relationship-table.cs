using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineApiService.Migrations
{
    public partial class Relationshiptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AirlineId1",
                table: "Airline",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Airline_AirlineId1",
                table: "Airline",
                column: "AirlineId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Airline_Airline_AirlineId1",
                table: "Airline",
                column: "AirlineId1",
                principalTable: "Airline",
                principalColumn: "AirlineId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Airline_Airline_AirlineId1",
                table: "Airline");

            migrationBuilder.DropIndex(
                name: "IX_Airline_AirlineId1",
                table: "Airline");

            migrationBuilder.DropColumn(
                name: "AirlineId1",
                table: "Airline");
        }
    }
}
