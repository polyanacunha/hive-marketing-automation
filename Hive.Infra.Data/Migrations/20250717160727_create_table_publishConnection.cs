using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_table_publishConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ProdutoDescription",
                table: "Campaigns",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "MidiaLinks",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Value = table.Column<string>(type: "text", nullable: false),
                    ClientProfileId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidiaLinks", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MidiaLinks_ClientProfile_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PublishConnections",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientProfileId = table.Column<string>(type: "text", nullable: false),
                    Platform = table.Column<int>(type: "integer", nullable: false),
                    Expires = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    AccessToken = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PublishConnections", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PublishConnections_ClientProfile_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "29891efa-2719-4148-b660-72887d064285", "AQAAAAIAAYagAAAAEL5xBGCdqMDMrC6J6CiWxo5shuHKX1khPA/xvbu7La9LXmABlQJlq6Al3UIFhhioOQ==", "a5688561-427b-4593-99e0-47f39ca21ee7" });

            migrationBuilder.CreateIndex(
                name: "IX_MidiaLinks_ClientProfileId",
                table: "MidiaLinks",
                column: "ClientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_PublishConnections_ClientProfileId",
                table: "PublishConnections",
                column: "ClientProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MidiaLinks");

            migrationBuilder.DropTable(
                name: "PublishConnections");

            migrationBuilder.DropColumn(
                name: "ProdutoDescription",
                table: "Campaigns");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7dd10022-882b-46bb-9541-1a9d70d486aa", "AQAAAAIAAYagAAAAEGwHt4sMKuCHnHg4kRZKqwyiReBd9i6vnI+qiXSWPQ1Zbu0ILLahChyiVqZnYlJOvQ==", "1fb90f82-b341-41be-88ca-0a5887f10d8f" });
        }
    }
}
