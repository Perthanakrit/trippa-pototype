using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.database.Migrations
{
    /// <inheritdoc />
    public partial class DropRoleIdCol : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_UserRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_UserRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserRoleId",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetRoles");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserRoleId",
                table: "AspNetUsers",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetRoles",
                type: "character varying(13)",
                maxLength: 13,
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_UserRoleId",
                table: "AspNetUsers",
                column: "UserRoleId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_AspNetRoles_UserRoleId",
                table: "AspNetUsers",
                column: "UserRoleId",
                principalTable: "AspNetRoles",
                principalColumn: "Id");
        }
    }
}
