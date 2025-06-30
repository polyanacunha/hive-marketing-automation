using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_tabel_jobGeneration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "JobGeneration",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    Prompt = table.Column<string>(type: "text", nullable: false),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    ExternalJobId = table.Column<string>(type: "text", nullable: true),
                    FinalVideoUrl = table.Column<string>(type: "text", nullable: true),
                    AssetType = table.Column<int>(type: "integer", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    QueuedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CompletedAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobGeneration", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobGeneration_ClientProfile_ClientProfileId",
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
                values: new object[] { "5e638295-8bd2-454e-a7bd-d94e7337a83f", "AQAAAAIAAYagAAAAEGbeU1swHfoJv8ccr3lfgCafkv3ISJrfbyWLrxTWCPQaBMAtaVPY4S0b39Z+8F2FPQ==", "3981b832-0484-4a4a-948b-ed977eec509b" });

            migrationBuilder.CreateIndex(
                name: "IX_JobGeneration_ClientProfileId",
                table: "JobGeneration",
                column: "ClientProfileId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobGeneration");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "4c4bd88e-9c0b-4892-9bb8-7a72aa97f502", "AQAAAAIAAYagAAAAEKoyFc/7Vd9d/IQft/iBn5ivXlqdfoWPohqyHtDW3vWqdZspclSVXUwIHIn0f9+Gtw==", "f90ebbfa-eb46-4b05-ac8c-d01919a41a28" });
        }
    }
}
