using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineApiService.Migrations
{
    public partial class RemoveRelationshiptable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            
        }
    }
}
