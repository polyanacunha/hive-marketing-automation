using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_midiaProduction : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "OriginalPrompt",
                table: "MidiaProduction",
                newName: "UserPrompt");

            migrationBuilder.RenameColumn(
                name: "ContextualizedPrompt",
                table: "MidiaProduction",
                newName: "SystemPrompt");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "9b808646-aa24-4d24-8bde-5ae1b5a31207", "AQAAAAIAAYagAAAAEDSp0T+2xJ24tZU8YIexMrcXVcjKlLmeKKEnhpcFy/4IFWo2HD0gITwNbtA/TH4RiA==", "d101a027-fcc6-44ae-8df1-24504f23d59b" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserPrompt",
                table: "MidiaProduction",
                newName: "OriginalPrompt");

            migrationBuilder.RenameColumn(
                name: "SystemPrompt",
                table: "MidiaProduction",
                newName: "ContextualizedPrompt");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c506fd-a1fb-460f-ad7d-cddb3316d6b9", "AQAAAAIAAYagAAAAEN8rZmAeNFdvNDfYpmCmlgNGXCEbTdgrBNqOR1sJKRsoR+g/az4YI7ZS4qhYqjVXJA==", "8e80e40a-d349-4cf5-be03-faf1080de27f" });
        }
    }
}
