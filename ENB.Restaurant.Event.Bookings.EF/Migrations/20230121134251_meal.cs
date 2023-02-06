using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class meal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "MealId",
                table: "Menu_Meal",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MenuId",
                table: "Menu_Meal",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CustomerId",
                table: "Booking_Note",
                type: "int",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a3104591-b71b-4a79-8eaa-45bedb49fe44", "d7e127ce-ac99-473f-9410-de82256b4023", "Visitor", "VISITOR" },
                    { "c4f13f34-5985-440d-9da3-b20a169ab033", "3186293f-541a-482d-8b8f-a4235bdae6e1", "Administrator", "ADMINISTRATOR" },
                    
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Meal_MealId",
                table: "Menu_Meal",
                column: "MealId");

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Meal_MenuId",
                table: "Menu_Meal",
                column: "MenuId");

            migrationBuilder.CreateIndex(
                name: "IX_Booking_Note_CustomerId",
                table: "Booking_Note",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Drug_Companies_MenuId",
                table: "Menu_Meal",
                column: "MenuId",
                principalTable: "Drug_Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Booking_Note_Customers_CustomerId",
                table: "Booking_Note");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Drug_Companies_MenuId",
                table: "Menu_Meal");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal");

            migrationBuilder.DropIndex(
                name: "IX_Menu_Meal_MealId",
                table: "Menu_Meal");

            migrationBuilder.DropIndex(
                name: "IX_Menu_Meal_MenuId",
                table: "Menu_Meal");

            migrationBuilder.DropIndex(
                name: "IX_Booking_Note_CustomerId",
                table: "Booking_Note");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "a3104591-b71b-4a79-8eaa-45bedb49fe44");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "c4f13f34-5985-440d-9da3-b20a169ab033");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "e6341c20-a083-421e-af6c-4eb4217e4f25");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "eebd8c82-7961-41f4-b8d8-05babd7cd16c");

            migrationBuilder.DropColumn(
                name: "MealId",
                table: "Menu_Meal");

            migrationBuilder.DropColumn(
                name: "MenuId",
                table: "Menu_Meal");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                table: "Booking_Note");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "5147207a-a852-407f-a741-90473f5ceb6c", "7a47f6a5-bcfc-43bb-9c07-f21226532860", "Administrator", "ADMINISTRATOR" },
                    { "ac81d41e-5ef8-47f3-9654-fa85ec56be84", "400f9901-242e-4a4a-a9ef-225255de425a", "Visitor", "VISITOR" },
                    { "d03256db-abe6-4309-bc1e-51d454f08e87", "202297eb-0ff1-48e2-8b68-71b73af9147d", "Visitor", "VISITOR" },
                    { "ed090eda-47b2-4d91-984d-e3a40c5b8dd9", "bda27243-33c7-4ce3-bdcb-c7ec41f001c1", "Administrator", "ADMINISTRATOR" }
                });
        }
    }
}
