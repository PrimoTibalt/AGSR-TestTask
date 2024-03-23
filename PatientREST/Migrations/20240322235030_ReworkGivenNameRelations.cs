using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientREST.Migrations
{
    /// <inheritdoc />
    public partial class ReworkGivenNameRelations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenName_Names_NamesId",
                table: "GivenName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName");

            migrationBuilder.DropIndex(
                name: "IX_GivenName_NamesId",
                table: "GivenName");

            migrationBuilder.RenameColumn(
                name: "NamesId",
                table: "GivenName",
                newName: "NameId");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Given",
                type: "TEXT",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName",
                columns: new[] { "NameId", "GivenId" });

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Given_Text",
                table: "Given",
                column: "Text");

            migrationBuilder.CreateIndex(
                name: "IX_GivenName_GivenId",
                table: "GivenName",
                column: "GivenId");

            migrationBuilder.AddForeignKey(
                name: "FK_GivenName_Names_NameId",
                table: "GivenName",
                column: "NameId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_GivenName_Names_NameId",
                table: "GivenName");

            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName");

            migrationBuilder.DropIndex(
                name: "IX_GivenName_GivenId",
                table: "GivenName");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Given_Text",
                table: "Given");

            migrationBuilder.RenameColumn(
                name: "NameId",
                table: "GivenName",
                newName: "NamesId");

            migrationBuilder.AlterColumn<string>(
                name: "Text",
                table: "Given",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "TEXT");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName",
                columns: new[] { "GivenId", "NamesId" });

            migrationBuilder.CreateIndex(
                name: "IX_GivenName_NamesId",
                table: "GivenName",
                column: "NamesId");

            migrationBuilder.AddForeignKey(
                name: "FK_GivenName_Names_NamesId",
                table: "GivenName",
                column: "NamesId",
                principalTable: "Names",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
