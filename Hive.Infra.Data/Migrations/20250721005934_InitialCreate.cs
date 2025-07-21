using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90f50916-128e-4a6b-9920-6a9033ed9d57", "AQAAAAIAAYagAAAAEB0QBVMZlMPB7V4Bz5NiQHAh2SZVXYqwQrXVbNADKoGhWYNvrfKiLeLPJsx2M90+0A==", "976bfbd0-47a4-4508-90bf-4d24ff1773c6" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29891efa-2719-4148-b660-72887d064285", "AQAAAAIAAYagAAAAEL5xBGCdqMDMrC6J6CiWxo5shuHKX1khPA/xvbu7La9LXmABlQJlq6Al3UIFhhioOQ==", "a5688561-427b-4593-99e0-47f39ca21ee7" });
        }
    }
}
