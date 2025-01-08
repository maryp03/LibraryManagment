using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LibraryManagment.Migrations
{
    /// <inheritdoc />
    public partial class Borrowingupdate2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DelayDays",
                table: "Borrowing",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DelayDays",
                table: "Borrowing");
        }
    }
}
