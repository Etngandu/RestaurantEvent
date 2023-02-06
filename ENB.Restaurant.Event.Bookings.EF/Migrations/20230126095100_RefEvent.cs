using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class RefEvent : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "24a7eabb-243c-4dd9-9080-6a1efa463727");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "46cf25cb-4f24-4ab8-aeab-dfb358651450");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "537edadb-059d-4189-9dc6-f9023ae9b9ae");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ba65d5cc-09db-4828-a94a-dfd40df890a9");

            migrationBuilder.RenameColumn(
                name: "Ref_Menu_Status",
                table: "Booking",
                newName: "EventStatus");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {                    
                    { "59d37d3b-5a5e-4dba-b547-f0383c6dd71e", "30c8c203-851c-4f44-b6c3-654564ea4bba", "Visitor", "VISITOR" },
                    { "b675a6a0-d153-4650-87a4-daf02394cfb2", "f44f2ec2-8f61-4455-ab34-ac380c97a86c", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.RenameColumn(
                name: "EventStatus",
                table: "Booking",
                newName: "Ref_Menu_Status");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "24a7eabb-243c-4dd9-9080-6a1efa463727", "3de96241-b3c9-482b-84dd-4363caf18372", "Visitor", "VISITOR" },
                    { "46cf25cb-4f24-4ab8-aeab-dfb358651450", "07a1eb70-bd9c-43f6-89ae-f71e42128f82", "Administrator", "ADMINISTRATOR" },
                    { "537edadb-059d-4189-9dc6-f9023ae9b9ae", "2d2ce14a-309d-4131-bb29-c999283f1411", "Administrator", "ADMINISTRATOR" },
                    { "ba65d5cc-09db-4828-a94a-dfd40df890a9", "026500d1-f49c-4c63-90d3-739fca634bd4", "Visitor", "VISITOR" }
                });
        }
    }
}
