using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class role : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6016ceda-73d3-45ef-824f-b4341bbeb899");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "74032699-fd92-40e7-aee0-a3c1ae466391");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {                    
                    { "d03256db-abe6-4309-bc1e-51d454f08e87", "202297eb-0ff1-48e2-8b68-71b73af9147d", "Visitor", "VISITOR" },
                    { "ed090eda-47b2-4d91-984d-e3a40c5b8dd9", "bda27243-33c7-4ce3-bdcb-c7ec41f001c1", "Administrator", "ADMINISTRATOR" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "5147207a-a852-407f-a741-90473f5ceb6c");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ac81d41e-5ef8-47f3-9654-fa85ec56be84");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "d03256db-abe6-4309-bc1e-51d454f08e87");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "ed090eda-47b2-4d91-984d-e3a40c5b8dd9");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6016ceda-73d3-45ef-824f-b4341bbeb899", "5912106f-1f1e-4b77-a471-14ab2ab77e73", "Visitor", "VISITOR" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "74032699-fd92-40e7-aee0-a3c1ae466391", "7020ab57-0129-4428-aaf1-10c653f4eb9f", "Administrator", "ADMINISTRATOR" });
        }
    }
}
