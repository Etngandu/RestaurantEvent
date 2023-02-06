using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class @event : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d2b1e2e3-c6ec-412a-887b-c6197dd8f2b2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "da8748cf-c663-4390-b3a3-1cc5f46d11a1");

            migrationBuilder.RenameColumn(
                name: "Date_of_event",
                table: "Booking",
                newName: "Start");

            migrationBuilder.AddColumn<bool>(
                name: "AllDay",
                table: "Booking",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Color",
                table: "Booking",
                type: "nvarchar(150)",
                maxLength: 150,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "End",
                table: "Booking",
                type: "datetime2",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6016ceda-73d3-45ef-824f-b4341bbeb899", "5912106f-1f1e-4b77-a471-14ab2ab77e73", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74032699-fd92-40e7-aee0-a3c1ae466391", "7020ab57-0129-4428-aaf1-10c653f4eb9f", "Administrator", "ADMINISTRATOR" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6016ceda-73d3-45ef-824f-b4341bbeb899");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74032699-fd92-40e7-aee0-a3c1ae466391");

            migrationBuilder.DropColumn(
                name: "AllDay",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "Color",
                table: "Booking");

            migrationBuilder.DropColumn(
                name: "End",
                table: "Booking");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Booking",
                newName: "Date_of_event");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "d2b1e2e3-c6ec-412a-887b-c6197dd8f2b2", "21ebc5e0-db58-4feb-b502-1edc02787168", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "da8748cf-c663-4390-b3a3-1cc5f46d11a1", "b04668fa-e314-492f-8ae6-b27028532ce8", "Administrator", "ADMINISTRATOR" });
        }
    }
}
