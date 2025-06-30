using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hive.Infra.Data.Migrations
{
    /// <inheritdoc />
    public partial class create_tabel_midiaProducion_and_imageUrl : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobGeneration_ClientProfile_ClientProfileId",
                table: "JobGeneration");

            migrationBuilder.DropTable(
                name: "Campaing");

            migrationBuilder.DropTable(
                name: "Midia");

            migrationBuilder.DropTable(
                name: "Post");

            migrationBuilder.DropIndex(
                name: "IX_JobGeneration_ClientProfileId",
                table: "JobGeneration");

            migrationBuilder.DropColumn(
                name: "ClientProfileId",
                table: "JobGeneration");

            migrationBuilder.DropColumn(
                name: "QueuedAt",
                table: "JobGeneration");

            migrationBuilder.RenameColumn(
                name: "FinalVideoUrl",
                table: "JobGeneration",
                newName: "ExternalMediaUrl");

            migrationBuilder.AddColumn<int>(
                name: "MidiaProductionId",
                table: "JobGeneration",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ImageUrl",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Url = table.Column<string>(type: "text", nullable: false),
                    ClientProfileId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrl", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MidiaProduction",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    ClientProfileId = table.Column<Guid>(type: "uuid", nullable: false),
                    OriginalPrompt = table.Column<string>(type: "text", nullable: false),
                    ContextualizedPrompt = table.Column<string>(type: "text", nullable: false),
                    GeneratedScriptJson = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    FinalVideoUrl = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MidiaProduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MidiaProduction_ClientProfile_ClientProfileId",
                        column: x => x.ClientProfileId,
                        principalTable: "ClientProfile",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ImageUrlMidiaProduction",
                columns: table => new
                {
                    InputImageUrlId = table.Column<int>(type: "integer", nullable: false),
                    MidiaProductionsId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ImageUrlMidiaProduction", x => new { x.InputImageUrlId, x.MidiaProductionsId });
                    table.ForeignKey(
                        name: "FK_ImageUrlMidiaProduction_ImageUrl_InputImageUrlId",
                        column: x => x.InputImageUrlId,
                        principalTable: "ImageUrl",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ImageUrlMidiaProduction_MidiaProduction_MidiaProductionsId",
                        column: x => x.MidiaProductionsId,
                        principalTable: "MidiaProduction",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "a18be9c0-aa65-4af8-bd17-002f23242002",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f8c506fd-a1fb-460f-ad7d-cddb3316d6b9", "AQAAAAIAAYagAAAAEN8rZmAeNFdvNDfYpmCmlgNGXCEbTdgrBNqOR1sJKRsoR+g/az4YI7ZS4qhYqjVXJA==", "8e80e40a-d349-4cf5-be03-faf1080de27f" });

            migrationBuilder.CreateIndex(
                name: "IX_JobGeneration_MidiaProductionId",
                table: "JobGeneration",
                column: "MidiaProductionId");

            migrationBuilder.CreateIndex(
                name: "IX_ImageUrlMidiaProduction_MidiaProductionsId",
                table: "ImageUrlMidiaProduction",
                column: "MidiaProductionsId");

            migrationBuilder.CreateIndex(
                name: "IX_MidiaProduction_ClientProfileId",
                table: "MidiaProduction",
                column: "ClientProfileId");

            migrationBuilder.AddForeignKey(
                name: "FK_JobGeneration_MidiaProduction_MidiaProductionId",
                table: "JobGeneration",
                column: "MidiaProductionId",
                principalTable: "MidiaProduction",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobGeneration_MidiaProduction_MidiaProductionId",
                table: "JobGeneration");

            migrationBuilder.DropTable(
                name: "ImageUrlMidiaProduction");

            migrationBuilder.DropTable(
                name: "ImageUrl");

            migrationBuilder.DropTable(
                name: "MidiaProduction");

            migrationBuilder.DropIndex(
                name: "IX_JobGeneration_MidiaProductionId",
                table: "JobGeneration");

            migrationBuilder.DropColumn(
                name: "MidiaProductionId",
                table: "JobGeneration");

            migrationBuilder.RenameColumn(
                name: "ExternalMediaUrl",
                table: "JobGeneration",
                newName: "FinalVideoUrl");

            migrationBuilder.AddColumn<Guid>(
                name: "ClientProfileId",
                table: "JobGeneration",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<DateTime>(
                name: "QueuedAt",
                table: "JobGeneration",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Campaing",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Budget = table.Column<double>(type: "double precision", nullable: false),
                    CampaingStatus = table.Column<string>(type: "text", nullable: false),
                    CampaingType = table.Column<string>(type: "text", nullable: false),
                    FinalDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    InitialDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    Message = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    TargetPublic = table.Column<string>(type: "text", nullable: false),
                    UsuarioId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Campaing", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Midia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    AspectRatio = table.Column<string>(type: "text", nullable: false),
                    Duration = table.Column<string>(type: "text", nullable: false),
                    Format = table.Column<string>(type: "text", nullable: false),
                    Resolution = table.Column<string>(type: "text", nullable: false),
                    Size = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Midia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Post",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Legenda = table.Column<string>(type: "text", nullable: false),
                    Url = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Post", x => x.Id);
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

            migrationBuilder.AddForeignKey(
                name: "FK_JobGeneration_ClientProfile_ClientProfileId",
                table: "JobGeneration",
                column: "ClientProfileId",
                principalTable: "ClientProfile",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
