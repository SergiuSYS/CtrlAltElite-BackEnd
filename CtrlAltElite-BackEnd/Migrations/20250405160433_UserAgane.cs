using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CtrlAltElite_BackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UserAgane : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "useRname",
                table: "AspNetUsers",
                newName: "userName");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userName",
                table: "AspNetUsers",
                newName: "useRname");
        }
    }
}
