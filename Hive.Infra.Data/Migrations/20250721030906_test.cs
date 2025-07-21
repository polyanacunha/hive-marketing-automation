using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class test : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "ab1d4ffb-522e-4f74-8dd8-4ffd27275337", "AQAAAAIAAYagAAAAEHZ+IJpmzLliHfi9sNnoh2adTea/IxGycM/IhX6g9Wgq85addj7kjaBdsCEjgUjB1A==", "42083a3f-9219-4105-917f-81956e296519" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "90f50916-128e-4a6b-9920-6a9033ed9d57", "AQAAAAIAAYagAAAAEB0QBVMZlMPB7V4Bz5NiQHAh2SZVXYqwQrXVbNADKoGhWYNvrfKiLeLPJsx2M90+0A==", "976bfbd0-47a4-4508-90bf-4d24ff1773c6" });
        }
    }
}
