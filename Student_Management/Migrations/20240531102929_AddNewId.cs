using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Student_Management.Migrations
{
    /// <inheritdoc />
    public partial class AddNewId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
           

            migrationBuilder.AddColumn<string>(
                name: "StudentId",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Students");

           
        }
    }
}
