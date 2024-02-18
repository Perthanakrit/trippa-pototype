using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.database.Migrations
{
    /// <inheritdoc />
    public partial class AddColAttendee : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAccepted",
                table: "TripAttendees",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAccepted",
                table: "TripAttendees");
        }
    }
}
