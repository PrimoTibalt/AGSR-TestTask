using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientREST.Migrations
{
    /// <inheritdoc />
    public partial class MakeGivenTextNotUnique : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropUniqueConstraint(
                name: "AK_Given_Text",
                table: "Given");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Given",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Given",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Given_Text",
                table: "Given",
                column: "Text");
        }
    }
}
