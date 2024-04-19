using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnVatCanTho.DataAccess.Migrations
{
    public partial class addCreatedAtFieldForCommentTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Snack_Bars",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "no-avatar.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "Star",
                table: "Ratings",
                type: "float",
                nullable: false,
                defaultValue: 0.0,
                oldClrType: typeof(double),
                oldType: "float");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Customers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "no-avatar.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LikeQuantity",
                table: "Comments",
                type: "int",
                nullable: true,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreatedAt",
                table: "Comments",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "getdate()");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Administrators",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "no-avatar.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 55, 56, 41, DateTimeKind.Local).AddTicks(3881), "$2a$11$1piWTecWlRdRoyqDV.9s6.9CBLz0.BhHhTZM32NpzoUAa6KgvUWeK" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 55, 55, 886, DateTimeKind.Local).AddTicks(6842), "$2a$11$mZ7lTjj1rKXLiylWysvY/e0nXLvQdqMa6RUlgo7YE/U7sdF5hY10." });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 55, 56, 41, DateTimeKind.Local).AddTicks(3199), "$2a$11$LiQdX3yep8I7tCXDEsauoukYr8QjF5vFP9YgePrjdGlFYpARuVs1." });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 55, 56, 344, DateTimeKind.Local).AddTicks(780), "$2a$11$WlXFQI6XzOhNAeN2bs3i6uc6aB8CsC4C8VI3c0sR/Jc/SGVEf0FBW" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 19, 9, 55, 56, 491, DateTimeKind.Local).AddTicks(3310), "$2a$11$EB42Qhd2fPkUTUY/jpDJ8OnDoI5pa5eZ3JXr38.wpViIwCJifLDYu" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreatedAt",
                table: "Comments");

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Snack_Bars",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "no-avatar.jpg");

            migrationBuilder.AlterColumn<double>(
                name: "Star",
                table: "Ratings",
                type: "float",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "float",
                oldDefaultValue: 0.0);

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Customers",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "no-avatar.jpg");

            migrationBuilder.AlterColumn<int>(
                name: "LikeQuantity",
                table: "Comments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldDefaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Avatar",
                table: "Administrators",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "no-avatar.jpg");

            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 3, 19, 55, 39, 271, DateTimeKind.Local).AddTicks(2567), "$2a$11$5TW40BYwYyBh1WFv157uXu8UsimlUlpzeCcLuT5UDtz50lLtBi8jW" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 3, 19, 55, 39, 128, DateTimeKind.Local).AddTicks(5312), "$2a$11$JPOHzRVNtb1SHnCCje9q9.cJF0iC4Dhp3LkVslth7Ndzv9nChoPli" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 3, 19, 55, 39, 271, DateTimeKind.Local).AddTicks(2135), "$2a$11$32PpsqyoZavoO1ziHjQeGePy2GObWuyxPkjwpkaROe12b/yg7G.42" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 3, 19, 55, 39, 586, DateTimeKind.Local).AddTicks(215), "$2a$11$v4fl.EFctO0bciAxkDeFIOOCOAYodqb6YzYthDg6/SGIUa34GFGQi" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 4, 3, 19, 55, 39, 726, DateTimeKind.Local).AddTicks(846), "$2a$11$iEaqDWDD3pfc6aVF.HbgRexoRI/kW5UQ12C3YotrLUMz6OE6NOKfq" });
        }
    }
}
