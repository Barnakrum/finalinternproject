using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace finalinternshipproject.Migrations
{
    public partial class FixedMistakedInFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "value",
                table: "IntegerFieldsNames");

            migrationBuilder.DropColumn(
                name: "value",
                table: "DateTimeFieldsNames");

            migrationBuilder.DropColumn(
                name: "value",
                table: "BooleanFieldsNames");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "StringFieldsNames",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "StringField",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "MultilineFieldsNames",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "MultilineField",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "IntegerField",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "DateTimeField",
                newName: "Value");

            migrationBuilder.RenameColumn(
                name: "value",
                table: "BooleanField",
                newName: "Value");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "IntegerFieldsNames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "DateTimeFieldsNames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "BooleanFieldsNames",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "IntegerFieldsNames");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "DateTimeFieldsNames");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "BooleanFieldsNames");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "StringFieldsNames",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "StringField",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "MultilineFieldsNames",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "MultilineField",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "IntegerField",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "DateTimeField",
                newName: "value");

            migrationBuilder.RenameColumn(
                name: "Value",
                table: "BooleanField",
                newName: "value");

            migrationBuilder.AddColumn<int>(
                name: "value",
                table: "IntegerFieldsNames",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "value",
                table: "DateTimeFieldsNames",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<bool>(
                name: "value",
                table: "BooleanFieldsNames",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }
    }
}
