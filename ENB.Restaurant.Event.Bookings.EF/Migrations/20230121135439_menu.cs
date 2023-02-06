using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class menu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Booked_Drug_Companies_MenuId",
                table: "Menu_Booked");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Drug_Companies_MenuId",
                table: "Menu_Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Drug_Companies",
                table: "Drug_Companies");

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

            migrationBuilder.RenameTable(
                name: "Drug_Companies",
                newName: "Menus");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menus",
                table: "Menus",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13027d03-4ca8-4483-8cd9-e6851d6aeb46", "f0437d9a-4021-413c-92e8-a4cc560bf16d", "Visitor", "VISITOR" },                    
                    { "f0728ebd-9af2-4a5f-91de-fd1c4d99f16f", "22be9f60-b706-4bbb-aeab-054333ce7794", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Booked_Menus_MenuId",
                table: "Menu_Booked",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Menus_MenuId",
                table: "Menu_Meal",
                column: "MenuId",
                principalTable: "Menus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Booked_Menus_MenuId",
                table: "Menu_Booked");

            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Menus_MenuId",
                table: "Menu_Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menus",
                table: "Menus");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "13027d03-4ca8-4483-8cd9-e6851d6aeb46");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "660bcc39-8d1f-4f08-951a-25be1179296d");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7e823bad-4724-45e9-8981-30fa9eb657ad");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "f0728ebd-9af2-4a5f-91de-fd1c4d99f16f");

            migrationBuilder.RenameTable(
                name: "Menus",
                newName: "Drug_Companies");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Drug_Companies",
                table: "Drug_Companies",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "a3104591-b71b-4a79-8eaa-45bedb49fe44", "d7e127ce-ac99-473f-9410-de82256b4023", "Visitor", "VISITOR" },
                    { "c4f13f34-5985-440d-9da3-b20a169ab033", "3186293f-541a-482d-8b8f-a4235bdae6e1", "Administrator", "ADMINISTRATOR" },
                    { "e6341c20-a083-421e-af6c-4eb4217e4f25", "4eb06aad-6467-481e-9005-6136ea49405e", "Administrator", "ADMINISTRATOR" },
                    { "eebd8c82-7961-41f4-b8d8-05babd7cd16c", "0ce0c5f5-2833-4660-9759-9093e92438d3", "Visitor", "VISITOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Booked_Drug_Companies_MenuId",
                table: "Menu_Booked",
                column: "MenuId",
                principalTable: "Drug_Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Drug_Companies_MenuId",
                table: "Menu_Meal",
                column: "MenuId",
                principalTable: "Drug_Companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
