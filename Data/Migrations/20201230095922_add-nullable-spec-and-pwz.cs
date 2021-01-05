using Microsoft.EntityFrameworkCore.Migrations;

namespace HMS.Data.Migrations
{
    public partial class addnullablespecandpwz : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Employee",
                type: "varchar(50)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(50)");

            migrationBuilder.AlterColumn<string>(
                name: "PwzNumber",
                table: "Employee",
                type: "varchar(7)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(7)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Specialization",
                table: "Employee",
                type: "varchar(50)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PwzNumber",
                table: "Employee",
                type: "varchar(7)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(7)",
                oldNullable: true);
        }
    }
}
