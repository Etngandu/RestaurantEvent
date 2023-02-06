using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ENB.Restaurant.Event.Bookings.EF.Migrations
{
    public partial class menumeal : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu_Meal",
                table: "Menu_Meal");

            migrationBuilder.DropIndex(
                name: "IX_Menu_Meal_MealId",
                table: "Menu_Meal");

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

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Menu_Meal");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Menu_Meal");

            migrationBuilder.DropColumn(
                name: "DateModified",
                table: "Menu_Meal");

            migrationBuilder.DropColumn(
                name: "Other_Menu_Meal_detail",
                table: "Menu_Meal");

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "Menu_Meal",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu_Meal",
                table: "Menu_Meal",
                columns: new[] { "MealId", "MenuId" });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "537edadb-059d-4189-9dc6-f9023ae9b9ae", "2d2ce14a-309d-4131-bb29-c999283f1411", "Administrator", "ADMINISTRATOR" },
                    { "ba65d5cc-09db-4828-a94a-dfd40df890a9", "026500d1-f49c-4c63-90d3-739fca634bd4", "Visitor", "VISITOR" }
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Menu_Meal",
                table: "Menu_Meal");

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

            migrationBuilder.AlterColumn<int>(
                name: "MealId",
                table: "Menu_Meal",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Menu_Meal",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Menu_Meal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateModified",
                table: "Menu_Meal",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Other_Menu_Meal_detail",
                table: "Menu_Meal",
                type: "nvarchar(350)",
                maxLength: 350,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Menu_Meal",
                table: "Menu_Meal",
                column: "Id");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "13027d03-4ca8-4483-8cd9-e6851d6aeb46", "f0437d9a-4021-413c-92e8-a4cc560bf16d", "Visitor", "VISITOR" },
                    { "660bcc39-8d1f-4f08-951a-25be1179296d", "682bcbcb-97ce-4132-b936-a36fafcdd89d", "Visitor", "VISITOR" },
                    { "7e823bad-4724-45e9-8981-30fa9eb657ad", "7de6da51-c48b-4f0f-b959-5c007cc9a34f", "Administrator", "ADMINISTRATOR" },
                    { "f0728ebd-9af2-4a5f-91de-fd1c4d99f16f", "22be9f60-b706-4bbb-aeab-054333ce7794", "Administrator", "ADMINISTRATOR" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Menu_Meal_MealId",
                table: "Menu_Meal",
                column: "MealId");

            migrationBuilder.AddForeignKey(
                name: "FK_Menu_Meal_Meal_MealId",
                table: "Menu_Meal",
                column: "MealId",
                principalTable: "Meal",
                principalColumn: "Id");
        }
    }
}
