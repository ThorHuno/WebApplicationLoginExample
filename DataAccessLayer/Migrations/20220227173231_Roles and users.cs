using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DataAccessLayer.Migrations
{
    public partial class Rolesandusers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Discriminator", "NormalizedName", "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "c67e0991-32ec-42c8-8369-b11e8376bf43", "Admin", "ApplicationRole", "ADMIN", "f6e3a436-1d99-40e9-a0f8-d5f87753853c", new DateTime(2022, 2, 27, 17, 32, 29, 588, DateTimeKind.Utc).AddTicks(3715) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Discriminator", "NormalizedName", "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "c67e0991-32ec-42c8-8369-b11e8376bf44", "User", "ApplicationRole", "USER", "f6e3a436-1d99-40e9-a0f8-d5f87753853c", new DateTime(2022, 2, 27, 17, 32, 29, 588, DateTimeKind.Utc).AddTicks(3715) });

            migrationBuilder.InsertData(
                table: "Roles",
                columns: new[] { "Id", "Name", "Discriminator", "NormalizedName", "ConcurrencyStamp", "CreatedAt" },
                values: new object[] { "c67e0991-32ec-42c8-8369-b11e8376bf45", "Public", "ApplicationRole", "PUBLIC", "f6e3a436-1d99-40e9-a0f8-d5f87753853c", new DateTime(2022, 2, 27, 17, 32, 29, 588, DateTimeKind.Utc).AddTicks(3715) });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "UserName", "NormalizedUserName", "AccessFailedCount", "EmailConfirmed", "PhoneNumberConfirmed", "TwoFactorEnabled", "LockoutEnabled", "SecurityStamp", "ConcurrencyStamp", "CreatedAt", "PasswordHash" },
                values: new object[] { "7545aa39-44c6-4792-92c0-c2695fe37507", "admin", "ADMIN", 0, false, false, false, true, "3e21dcbc-ea5d-4b31-944f-2449ad4f0bbb", "3e21dcbc-ea5d-4b31-944f-2449ad4f0bbb", new DateTime(2022, 2, 27, 17, 32, 29, 619, DateTimeKind.Utc).AddTicks(7764), "AQAAAAEAACcQAAAAEPVuyOSCkuXHFgIl5LCo0XGGdIurJLLS0lHuFXJIlqcRYPb9SmRubvzsFfmG2uCbNw==" });

            migrationBuilder.InsertData(
                table: "UserRoles",
                columns: new[] { "UserId", "RoleId" },
                values: new object[] { "7545aa39-44c6-4792-92c0-c2695fe37507", "c67e0991-32ec-42c8-8369-b11e8376bf43" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c67e0991-32ec-42c8-8369-b11e8376bf43");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c67e0991-32ec-42c8-8369-b11e8376bf44");

            migrationBuilder.DeleteData(
                table: "Roles",
                keyColumn: "Id",
                keyValue: "c67e0991-32ec-42c8-8369-b11e8376bf45");

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: "7545aa39-44c6-4792-92c0-c2695fe37507");

            migrationBuilder.DeleteData(
                table: "UserRoles",
                keyColumns: new[] { "UserId", "RoleId" },
                keyValues: new[] { "7545aa39-44c6-4792-92c0-c2695fe37507", "c67e0991-32ec-42c8-8369-b11e8376bf43" });
        }
    }
}
