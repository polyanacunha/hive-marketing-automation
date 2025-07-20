using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class alter_table_ImageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Url",
                table: "ImageUrl",
                newName: "ImageKey");

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "ImageUrl",
                type: "timestamp with time zone",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dd10022-882b-46bb-9541-1a9d70d486aa", "AQAAAAIAAYagAAAAEGwHt4sMKuCHnHg4kRZKqwyiReBd9i6vnI+qiXSWPQ1Zbu0ILLahChyiVqZnYlJOvQ==", "1fb90f82-b341-41be-88ca-0a5887f10d8f" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "ImageUrl");

            migrationBuilder.RenameColumn(
                name: "ImageKey",
                table: "ImageUrl",
                newName: "Url");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7bfebbdc-eba2-4c98-be58-1cd4d0249145", "AQAAAAIAAYagAAAAEEwqos6ZHlq5zahHxKEgF2zCMM+BAVDoiz+S3BjYbeE8/bR6T9hOvQb1P6u9RCfJkA==", "fd830878-5653-47a6-a0eb-4abd7e75a2b2" });
        }
    }
}
