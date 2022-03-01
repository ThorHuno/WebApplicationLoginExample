using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Menudata : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 1, "User Management", null, "#" },
                    { 4, "Menu Management", null, "#" }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[,]
                {
                    { 1, "c67e0991-32ec-42c8-8369-b11e8376bf43" },
                    { 4, "c67e0991-32ec-42c8-8369-b11e8376bf43" }
                });

            migrationBuilder.InsertData(
                table: "Menus",
                columns: new[] { "Id", "Name", "SubMenuId", "Url" },
                values: new object[,]
                {
                    { 2, "Users", 1, "/Auth/UserManagement/Index" },
                    { 3, "Create user", 1, "/Auth/UserManagement/Register" },
                    { 5, "Create menu item", 4, "/Menu/Menu/Create" }
                });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 2, "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 3, "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.InsertData(
                table: "MenuRoles",
                columns: new[] { "MenuId", "RoleId" },
                values: new object[] { 5, "c67e0991-32ec-42c8-8369-b11e8376bf43" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new[] { "1", "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new[] { "4", "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValues: new[] { "1", "4" });

            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new[] { "2", "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new[] { "3", "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.DeleteData(
                table: "MenuRoles",
                keyColumns: new[] { "MenuId", "RoleId" },
                keyValues: new[] { "5", "c67e0991-32ec-42c8-8369-b11e8376bf43" });

            migrationBuilder.DeleteData(
                table: "Menus",
                keyColumn: "Id",
                keyValues: new[] { "2", "3", "5" });
        }
    }
}
