using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_tabel_clientProfile : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Post",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaing",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddColumn<Guid>(
                name: "ClientProfileId",
                table: "AspNetUsers",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MarketSegment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MarketSegment", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "TargetAudience",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Description = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TargetAudience", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ClientProfile",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    MarketSegmentId = table.Column<int>(type: "integer", nullable: false),
                    TargetAudienceId = table.Column<int>(type: "integer", nullable: false),
                    CompanyName = table.Column<string>(type: "text", nullable: false),
                    TaxId = table.Column<string>(type: "text", nullable: false),
                    WebSiteUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientProfile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ClientProfile_MarketSegment_MarketSegmentId",
                        column: x => x.MarketSegmentId,
                        principalTable: "MarketSegment",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ClientProfile_TargetAudience_TargetAudienceId",
                        column: x => x.TargetAudienceId,
                        principalTable: "TargetAudience",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ClientProfileId", "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { null, "4c4bd88e-9c0b-4892-9bb8-7a72aa97f502", "AQAAAAIAAYagAAAAEKoyFc/7Vd9d/IQft/iBn5ivXlqdfoWPohqyHtDW3vWqdZspclSVXUwIHIn0f9+Gtw==", "f90ebbfa-eb46-4b05-ac8c-d01919a41a28" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_ClientProfileId",
                table: "AspNetUsers",
                column: "ClientProfileId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfile_MarketSegmentId",
                table: "ClientProfile",
                column: "MarketSegmentId");

            migrationBuilder.CreateIndex(
                name: "IX_ClientProfile_TargetAudienceId",
                table: "ClientProfile",
                column: "TargetAudienceId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_ClientProfile_ClientProfileId",
                table: "AspNetUsers",
                column: "ClientProfileId",
                principalTable: "ClientProfile",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_ClientProfile_ClientProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropTable(
                name: "ClientProfile");

            migrationBuilder.DropTable(
                name: "MarketSegment");

            migrationBuilder.DropTable(
                name: "TargetAudience");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_ClientProfileId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "ClientProfileId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<string>(
                name: "Url",
                table: "Post",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Campaing",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "91cdbefb-93ef-4164-b969-38c20a489465", "AQAAAAIAAYagAAAAEIA1bSSeM9ZlzMCX0rAlRHtd1HAv/leAiIhfV3h4g9VScoqfeaTlmxpL1l2ZkEsPyQ==", "f2e008e4-f803-450e-a4d5-e67a72ab5d5f" });
        }
    }
}
