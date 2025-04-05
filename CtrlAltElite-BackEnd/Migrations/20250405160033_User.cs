using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlAltElite_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class User : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_rooms_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropForeignKey(
                name: "FK_rooms_AspNetUsers_CreatorId",
                table: "rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_roomUserAssociation_AspNetUsers_UserId",
                table: "roomUserAssociation");

            migrationBuilder.DropIndex(
                name: "IX_roomUserAssociation_UserId",
                table: "roomUserAssociation");

            migrationBuilder.DropIndex(
                name: "IX_rooms_CreatorId",
                table: "rooms");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "UserName",
                table: "AspNetUsers",
                newName: "useRname");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "rooms",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AlterColumn<string>(
                name: "useRname",
                table: "AspNetUsers",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "character varying(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserName",
                table: "AspNetUsers");

            migrationBuilder.RenameColumn(
                name: "useRname",
                table: "AspNetUsers",
                newName: "UserName");

            migrationBuilder.AlterColumn<string>(
                name: "CreatorId",
                table: "rooms",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserName",
                table: "AspNetUsers",
                type: "character varying(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "AspNetUsers",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_roomUserAssociation_UserId",
                table: "roomUserAssociation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_rooms_CreatorId",
                table: "rooms",
                column: "CreatorId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_RoomId",
                table: "AspNetUsers",
                column: "RoomId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_roomUserAssociation_AspNetUsers_UserId",
                table: "roomUserAssociation",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
