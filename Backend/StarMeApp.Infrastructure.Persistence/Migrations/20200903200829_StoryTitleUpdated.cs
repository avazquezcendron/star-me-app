using Microsoft.EntityFrameworkCore.Migrations;

namespace StarMeApp.Infrastructure.Persistence.Migrations
{
    public partial class StoryTitleUpdated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "varchar(130)",
                maxLength: 130,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(30)",
                oldMaxLength: 30);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Title",
                table: "Stories",
                type: "varchar(30)",
                maxLength: 30,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(130)",
                oldMaxLength: 130);
        }
    }
}
