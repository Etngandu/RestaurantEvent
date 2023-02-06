using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class BkgNote : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Note_Booking_BookingId",
                table: "Booking_Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "0746e0d6-d421-4b0c-94e4-9e961c6fdd7a");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "40d1ec0f-c56f-443b-9c6b-2d59c3e3af30");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "59d37d3b-5a5e-4dba-b547-f0383c6dd71e");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b675a6a0-d153-4650-87a4-daf02394cfb2");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Booking_Note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Booking_Note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {                    
                    { "cb73aa16-376d-4206-9b9b-e3e1a683026d", "1c29fa98-99b3-4dde-8d8a-522dc661d1f9", "Administrator", "ADMINISTRATOR" },
                    { "fef8c8ea-4948-423f-bc47-fb49e4576290", "595adb50-3b7e-405e-8fa3-d3bc4b7470e2", "Visitor", "VISITOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Note_Booking_BookingId",
                table: "Booking_Note",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Note_Booking_BookingId",
                table: "Booking_Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5d2f3a7d-6e39-414e-8e13-198db53fc128");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "79d31f5c-438e-49ae-a770-693c1ff662d3");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "cb73aa16-376d-4206-9b9b-e3e1a683026d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "fef8c8ea-4948-423f-bc47-fb49e4576290");

            migrationBuilder.AlterColumn<int>(
                name: "CustomerId",
                table: "Booking_Note",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "BookingId",
                table: "Booking_Note",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "0746e0d6-d421-4b0c-94e4-9e961c6fdd7a", "39560f8d-63a3-4e93-88ad-0c4192736c7e", "Administrator", "ADMINISTRATOR" },
                    { "40d1ec0f-c56f-443b-9c6b-2d59c3e3af30", "df4c8bd4-a9d5-4411-93cc-4f5f6191d221", "Visitor", "VISITOR" },
                    { "59d37d3b-5a5e-4dba-b547-f0383c6dd71e", "30c8c203-851c-4f44-b6c3-654564ea4bba", "Visitor", "VISITOR" },
                    { "b675a6a0-d153-4650-87a4-daf02394cfb2", "f44f2ec2-8f61-4455-ab34-ac380c97a86c", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Note_Booking_BookingId",
                table: "Booking_Note",
                column: "BookingId",
                principalTable: "Booking",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");
        }
    }
}
