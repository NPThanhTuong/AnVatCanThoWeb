using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnVatCanTho.DataAccess.Migrations
{
    public partial class addCoverImageForSnackBar : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 10, 33, 53, 554, DateTimeKind.Local).AddTicks(2687), "$2a$11$i5FTGp16NNRSeoxRXX0PtOekr7hmlR6sUkTNrxAYlByBYGbxPTq3u" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 10, 33, 53, 404, DateTimeKind.Local).AddTicks(9116), "$2a$11$BYzzc5mb5GJJaUyBJ0hUfOW1xtvdiIHPM1n.qmJhdNfEWZ36o9p1G" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 10, 33, 53, 554, DateTimeKind.Local).AddTicks(2228), "$2a$11$oDTUTE2FWLZCnkHGoxEXueZylq0W0HpowZd8GLSeSCPV5ZsSzk.Xa" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { "cover-quan-ghien.jpg", new DateTime(2024, 5, 5, 10, 33, 53, 840, DateTimeKind.Local).AddTicks(9115), "$2a$11$Ggy2rLjWnD0PJ2MjcLlqE.uxk2TEzWO3pklRj6zGJXpQIb2Okg9IK" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { "cover-hoi-do.jpg", new DateTime(2024, 5, 5, 10, 33, 53, 991, DateTimeKind.Local).AddTicks(8937), "$2a$11$zsq1ji45xLR8lFqXKPIgJ.TVDTADP7qlEsFHgOvzrLtIrKYrd6stS" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { "cover-di-lan.jpg", new DateTime(2024, 5, 5, 10, 33, 54, 186, DateTimeKind.Local).AddTicks(4434), "$2a$11$.4EOlLrJ4M0lp/VgAjM8wO9upZQoPMWzHV0cctnGF4erKe5.r/sMC" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { "cover-co-hung.jpg", new DateTime(2024, 5, 5, 10, 33, 54, 329, DateTimeKind.Local).AddTicks(3840), "$2a$11$O0deYTPm/MjiwoCGfpJe5OEgZ86wtkifhBQDPgD/pBl6iyBG/HrFu" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CoverImage", "Display_Name", "DOB", "Password" },
                values: new object[] { "cover-gao-dua.jpg", "Quán ăn vặt Gáo Dừa", new DateTime(2024, 5, 5, 10, 33, 54, 470, DateTimeKind.Local).AddTicks(4222), "$2a$11$rMDgHRrC1qenzUlXnFHqN.mgTy4j.frfhS2YaQINZR/do8o1EwLxa" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 9, 27, 18, 993, DateTimeKind.Local).AddTicks(4400), "$2a$11$t0XnMZk58p6iLifYBvTKs.Y7ExZN9WbLpfZfInPqKbl1cczLFPZ1K" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 9, 27, 18, 807, DateTimeKind.Local).AddTicks(7987), "$2a$11$BFYqoq7CX6qpIBzlwgTljeCq/BWSvRphSzj5FP3srC4ySupWTWVMm" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "DOB", "Password" },
                values: new object[] { new DateTime(2024, 5, 5, 9, 27, 18, 993, DateTimeKind.Local).AddTicks(3938), "$2a$11$X6bVdxS1j.B0sr9Fs.vGweuIygqAtKtLBq1Vm2iK6.yIkS9tyq6Di" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 5, 5, 9, 27, 19, 359, DateTimeKind.Local).AddTicks(1100), "$2a$11$zMON6JQtYubQ3k.9vHTHseaK54mbgk/qhMNAFfr3M0buQeOaNdbZC" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 5, 5, 9, 27, 19, 542, DateTimeKind.Local).AddTicks(2638), "$2a$11$889lS.q5CfbUlmptV9dR/.9Qbp39JUEQx7v.93Kx1lklfSZ3kF8HK" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 5, 5, 9, 27, 19, 762, DateTimeKind.Local).AddTicks(7898), "$2a$11$RGnhfaAFV97Guec2Dac5wO2H.2QNDF4KngLQj0K/OvYFDad0CzDuq" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "CoverImage", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 5, 5, 9, 27, 19, 934, DateTimeKind.Local).AddTicks(8814), "$2a$11$6A1YLp39SQZhyNOiVLYuRufu.rfDleJ9gRB6pIJ7gZOFW7EHOn0HW" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 5,
                columns: new[] { "CoverImage", "Display_Name", "DOB", "Password" },
                values: new object[] { null, "Quán ăn vặt Gáo Dừa Cần Thơ", new DateTime(2024, 5, 5, 9, 27, 20, 98, DateTimeKind.Local).AddTicks(6777), "$2a$11$vVWMnVvvd9/1KOfGLwTvLufQNuI2J59DfcCyDqcpyQJaBz456GMUS" });
        }
    }
}
