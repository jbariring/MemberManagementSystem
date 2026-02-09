using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MemberManagement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAppUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.InsertData(
                table: "AppUsers",
                columns: new[] { "UserID", "DateCreated", "IsActive", "PasswordHash", "Username" },
                values: new object[] { 1, new DateTime(2026, 2, 9, 16, 32, 41, 749, DateTimeKind.Local).AddTicks(5420), true, "AQAAAAIAAYagAAAAEEXvFXOUU9K32YqcpYOXVzGjf3B8fdbnUlTdTjmsISXiVA9b2aBL1GXoWw2YyNNSQQ==", "admin" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AppUsers",
                keyColumn: "UserID",
                keyValue: 1);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "AppUsers",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "DateCreated",
                table: "AppUsers",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");
        }
    }
}
