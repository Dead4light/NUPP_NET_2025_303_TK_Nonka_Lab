using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Zoo.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Zookeepers",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FullName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zookeepers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AnimalModel",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    ZookeeperId = table.Column<Guid>(type: "uniqueidentifier", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalModel", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnimalModel_Zookeepers_ZookeeperId",
                        column: x => x.ZookeeperId,
                        principalTable: "Zookeepers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Lions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    IsAlpha = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Lions_AnimalModel_Id",
                        column: x => x.Id,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parrots",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Phrase = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parrots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Parrots_AnimalModel_Id",
                        column: x => x.Id,
                        principalTable: "AnimalModel",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalModel_ZookeeperId",
                table: "AnimalModel",
                column: "ZookeeperId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lions");

            migrationBuilder.DropTable(
                name: "Parrots");

            migrationBuilder.DropTable(
                name: "AnimalModel");

            migrationBuilder.DropTable(
                name: "Zookeepers");
        }
    }
}
