using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace CtrlAltElite_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class changes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "roomsMembers");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "rooms",
                type: "text",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "roomUserAssociation",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomUserAssociation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_roomUserAssociation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_roomUserAssociation_rooms_RoomId",
                        column: x => x.RoomId,
                        principalTable: "rooms",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_rooms_CreatorId",
                table: "rooms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_roomUserAssociation_RoomId",
                table: "roomUserAssociation",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_roomUserAssociation_UserId",
                table: "roomUserAssociation",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_rooms_RoomId",
                table: "AspNetUsers",
                column: "RoomId",
                principalTable: "rooms",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_rooms_AspNetUsers_CreatorId",
                table: "rooms",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_rooms_AspNetUsers_CreatorId",
                table: "rooms");

            migrationBuilder.DropTable(
                name: "roomUserAssociation");

            migrationBuilder.DropIndex(
                name: "IX_rooms_CreatorId",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "CreatorId",
                table: "rooms",
                type: "integer",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.CreateTable(
                name: "roomsMembers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoomId = table.Column<int>(type: "integer", nullable: false),
                    UserId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roomsMembers", x => x.Id);
                });
        }
    }
}
