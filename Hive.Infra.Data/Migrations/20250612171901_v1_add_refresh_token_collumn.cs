using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class v1_add_refresh_token_collumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "RefreshToken",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "RefreshTokenExpiresAtUtc",
                table: "AspNetUsers",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "RefreshToken", "RefreshTokenExpiresAtUtc", "SecurityStamp" },
                values: new object[] { "91cdbefb-93ef-4164-b969-38c20a489465", "AQAAAAIAAYagAAAAEIA1bSSeM9ZlzMCX0rAlRHtd1HAv/leAiIhfV3h4g9VScoqfeaTlmxpL1l2ZkEsPyQ==", null, null, "f2e008e4-f803-450e-a4d5-e67a72ab5d5f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RefreshToken",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RefreshTokenExpiresAtUtc",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "efb21f89-f8d9-41dc-8e80-f7cbcc12013b", "AQAAAAIAAYagAAAAEB7efJtFuhNO/zd65YIo8lx1v3kXLMAPcplh6B07iAAGa9yLTG2cjQJwKol6atH3EQ==", "c2239a5f-8638-446e-8788-212139fea18d" });
        }
    }
}
