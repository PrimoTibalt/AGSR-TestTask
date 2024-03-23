using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PatientREST.Migrations
{
    /// <inheritdoc />
    public partial class AddGivenNameSeparateId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName");

            migrationBuilder.AddColumn<Guid>(
                name: "Id",
                table: "GivenName",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_GivenName_NameId",
                table: "GivenName",
                column: "NameId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName");

            migrationBuilder.DropIndex(
                name: "IX_GivenName_NameId",
                table: "GivenName");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "GivenName");

            migrationBuilder.AddPrimaryKey(
                name: "PK_GivenName",
                table: "GivenName",
                columns: new[] { "NameId", "GivenId" });
        }
    }
}
