using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnVatCanTho.DataAccess.Migrations
{
    public partial class SeedingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "NA");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "NA2");

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Snack_Bars",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                defaultValue: "no-image.jpg",
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Ward_Name",
                value: "Tân Phú");

            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Display_Name", "DOB", "Password", "Username" },
                values: new object[] { "Admin", new DateTime(2024, 5, 5, 9, 27, 18, 993, DateTimeKind.Local).AddTicks(4400), "$2a$11$t0XnMZk58p6iLifYBvTKs.Y7ExZN9WbLpfZfInPqKbl1cczLFPZ1K", "admin" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "Ngon nha");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Content",
                value: "10 điểm");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "DOB", "Password" },
                values: new object[] { "user1.jpg", new DateTime(2024, 5, 5, 9, 27, 18, 807, DateTimeKind.Local).AddTicks(7987), "$2a$11$BFYqoq7CX6qpIBzlwgTljeCq/BWSvRphSzj5FP3srC4ySupWTWVMm" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "DOB", "Password" },
                values: new object[] { "user2.jpg", new DateTime(2024, 5, 5, 9, 27, 18, 993, DateTimeKind.Local).AddTicks(3938), "$2a$11$X6bVdxS1j.B0sr9Fs.vGweuIygqAtKtLBq1Vm2iK6.yIkS9tyq6Di" });

            migrationBuilder.InsertData(
                table: "Product_Images",
                columns: new[] { "Path_Name", "Product_Id", "SnackBar_Id" },
                values: new object[,]
                {
                    { "18Arx7CbQe-sinh-to-bo-01.jpg", 1, 1 },
                    { "MBGvAbfLEI-khoai-tay-chien-01.jpg", 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "Description", "UnitPrice" },
                values: new object[] { "Sinh tố bơ là một đồ uống mát lạnh và bổ dưỡng, được làm từ quả bơ chín mọng, sữa tươi hoặc sữa dừa, đường hoặc mật ong, và một chút đá xay nhuyễn. Đây là một lựa chọn tuyệt vời cho những người yêu thích hương vị độc đáo và chất dinh dưỡng của quả bơ. Sinh tố bơ có màu xanh đậm tự nhiên, với hương thơm béo ngậy của quả bơ kết hợp với sự ngọt ngào của sữa và đường.", 15 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "Description", "ProductCategory_Id", "UnitPrice" },
                values: new object[] { "Khoai tây chiên là một món ăn phổ biến trên khắp thế giới, được làm từ khoai tây cắt thành dạng que hoặc thanh, sau đó chiên giòn trong dầu. Khoai tây chiên có bề mặt giòn vàng, bên trong mềm mịn, và thường được ăn kèm với sốt cà chua, sốt mayonnaise, hoặc sốt bơ.", 3, 15 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "SnackBar_Id", "Description", "Ingredient", "Name", "ProductCategory_Id", "Stock", "UnitPrice" },
                values: new object[,]
                {
                    { 3, 1, "Gỏi cuốn, hay còn gọi là spring roll, là một món ăn truyền thống của Việt Nam, được biết đến với vị ngon, hương thơm, và sự tươi mát. Món này thường được làm từ các nguyên liệu tự nhiên và giàu dinh dưỡng như thịt gà hoặc tôm, rau sống, và các loại rau củ khác nhau, được cuốn trong một tờ bánh tráng mềm mịn và mỏng như giấy.", "NA", "Gỏi cuốn", 4, 1, 10 },
                    { 4, 1, "Món chân gà sốt Thái là một món ăn hấp dẫn và đậm đà, kết hợp giữa sự ngọt của sốt Thái và độ giòn của chân gà. Món này thường được chế biến bằng cách chiên hoặc nướng chân gà cho đến khi chúng giòn vàng, sau đó được tẩm sốt Thái thơm ngon.", "NA", "Chân gà sốt thái", 3, 1, 30 },
                    { 5, 1, "Trà đào cam sả là một đồ uống tươi mát và sảng khoái, kết hợp giữa hương vị ngọt ngào của đào, chua nhẹ của cam, và hương thơm dễ chịu của sả. Món này thường được làm từ trà đen hoặc trà xanh pha loãng kèm theo nước ép đào, nước cam tươi và một ít nước ép sả.", "NA", "Trà đào cam sả", 1, 1, 15 },
                    { 6, 2, "Gà rán là một món ăn phổ biến trên toàn thế giới, được biết đến với bề mặt ngoài giòn vàng và thịt gà mềm mịn bên trong. Món gà này thường được chế biến bằng cách ngâm trong một hỗn hợp gia vị, sau đó được chiên hoặc nướng cho đến khi thịt chín và bề mặt trở nên giòn và vàng ươm. Gà rán có hương vị đậm đà, béo ngậy từ dầu và gia vị, cùng với vị ngọt tự nhiên của thịt gà.", "NA", "Gà rán", 3, 1, 20 },
                    { 7, 2, "Trà chanh giã tay là một đồ uống tươi mát và sảng khoái, được làm từ trà đen hoặc trà xanh pha loãng kèm với chanh và đường, được giã tay hoặc lắc đều để hòa quện hương vị. Đây là một món uống phổ biến, đặc biệt vào mùa hè, khi hương vị chua cay và ngọt ngào của trà chanh giã tay mang lại cảm giác sảng khoái và giải khát.", "NA", "Trà chanh giã tay", 1, 1, 20 },
                    { 8, 2, "Trà mãng cầu là một loại đồ uống tươi mát và bổ dưỡng, được làm từ lá mãng cầu tươi hoặc lá mãng cầu khô, kết hợp với nước sôi và đường hoặc mật ong để tạo ra một hương vị độc đáo và thơm ngon. Món này thường được thưởng thức nóng hoặc lạnh, tùy thuộc vào sở thích cá nhân.", "NA", "Trà mãng cầu", 1, 1, 20 }
                });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "avt-quan-ghien.jpg", "Ghiền là quán ăn vặt Cần Thơ nổi tiếng với những món ăn như: Trà sữa, trà chanh, sinh tố, tàu hũ, khoai tây lắc, bánh tráng trộn… Đây cũng là quán ăn được nhiều du khách ghé thăm khi du lịch Cần Thơ. Sự đa dạng của thực đơn quán kèm với mức giá phải chăng sẽ giúp bạn thỏa sức ăn uống mà không phải lo về chi phí. ", "Quán Ghiền", new DateTime(2024, 5, 5, 9, 27, 19, 359, DateTimeKind.Local).AddTicks(1100), "$2a$11$zMON6JQtYubQ3k.9vHTHseaK54mbgk/qhMNAFfr3M0buQeOaNdbZC", "0312345678" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "avt-quan-hoi-do.jpg", "Một lựa chọn ăn vặt bến Ninh Kiều thú vị đó chính là quán Hồi Đó. Không gian quán ở đây được thiết kế theo phong cách thập niên 80 bởi vậy khi đến đây, bạn sẽ có một cảm giác quen thuộc. Ngoài ra, hầu hết đồ dùng, bàn ghế, chén đũa cũng được lựa chọn mang hơi hướng xưa cũ tạo cho du khách những cảm nhận khác lạ. ", "Quán Hồi Đó", new DateTime(2024, 5, 5, 9, 27, 19, 542, DateTimeKind.Local).AddTicks(2638), "$2a$11$889lS.q5CfbUlmptV9dR/.9Qbp39JUEQx7v.93Kx1lklfSZ3kF8HK", "0312345678" });

            migrationBuilder.InsertData(
                table: "Snack_Bars",
                columns: new[] { "Id", "Avatar", "Description", "Display_Name", "DOB", "Email", "Password", "Tel", "Username" },
                values: new object[,]
                {
                    { 3, "avt-quan-di-lan.jpg", "Da heo chiên giòn là một trong những món ăn vặt ở Cần Thơ được nhiều khách hàng yêu thích, đặc biệt là các bạn trẻ. Tại quán dì Lan, bạn không những được ăn món da heo đặc trưng mà còn có thể gọi thêm nhiều món khác như: Xôi chiên thập cẩm, dừa tắc… Điều làm nên sự khác biệt ở món da heo tại quán đó chính là hầu hết nguyên liệu đều được chủ quán tự làm, đảm bảo vệ sinh, mức giá thành cũng rất phù hợp với học sinh, sinh viên. ", "Da heo chiên giòn Dì Lan", new DateTime(2024, 5, 5, 9, 27, 19, 762, DateTimeKind.Local).AddTicks(7898), "snackbar3@site.com", "$2a$11$RGnhfaAFV97Guec2Dac5wO2H.2QNDF4KngLQj0K/OvYFDad0CzDuq", "0312345678", "snackbar3" },
                    { 4, "avt-quan-co-huong.jpg", "Bánh tráng là món ăn vặt quen thuộc của rất nhiều vùng miền khác nhau. Quán bánh tráng cô Hưng là quán ăn vặt Cần Thơ có tên tuổi lâu đời nhất, những món ăn tại đây cũng được chế biến theo hương vị rất đặc trưng. Một vài món ăn hấp dẫn bạn nên thử khi ghé thăm địa điểm này đó chính là: Bánh tráng cuộn sốt mayonnaise, bánh tráng trộn, các loại gỏi,… ", "Quán bánh tráng cô Hưng", new DateTime(2024, 5, 5, 9, 27, 19, 934, DateTimeKind.Local).AddTicks(8814), "snackbar4@site.com", "$2a$11$6A1YLp39SQZhyNOiVLYuRufu.rfDleJ9gRB6pIJ7gZOFW7EHOn0HW", "0312345678", "snackbar4" },
                    { 5, "avt-quan-gao-dua.jpg", "Nếu bạn đang tìm kiếm một quán ăn vặt ngon, rẻ ở Cần Thơ thì Gáo Dừa sẽ là một lựa chọn thú vị mà bạn nên tham khảo. Ưu điểm của quán này đó chính là không gian khá thoáng đãng, nhân viên phục vụ hết sức thân thiện. ", "Quán ăn vặt Gáo Dừa", new DateTime(2024, 5, 5, 9, 27, 20, 98, DateTimeKind.Local).AddTicks(6777), "snackbar5@site.com", "$2a$11$vVWMnVvvd9/1KOfGLwTvLufQNuI2J59DfcCyDqcpyQJaBz456GMUS", "0312345678", "snackbar5" }
                });

            migrationBuilder.InsertData(
                table: "Addresses",
                columns: new[] { "Id", "Customer_Id", "District_Name", "NoAndStreet", "SnackBar_Id", "Ward_Name" },
                values: new object[,]
                {
                    { 5, null, "Ninh Kiều", "87 Phạm Ngũ Lão", 3, "Xuân Khánh" },
                    { 6, null, "Ninh Kiều", "8 - 10, Nguyễn Cư Trinh", 4, "An Cư" },
                    { 7, null, "Ninh Kiều", "45 Lê Lợi", 5, "Xuân Khánh" }
                });

            migrationBuilder.InsertData(
                table: "Comments",
                columns: new[] { "Id", "Content", "Customer_Id", "Product_Id", "SnackBar_Id" },
                values: new object[,]
                {
                    { 3, "Hơi bị ngon luôn", 1, 3, 1 },
                    { 4, "Món này ăn hơi bị dính", 1, 4, 1 },
                    { 5, "Cũng ngon", 1, 5, 1 },
                    { 6, "Gà giòn rụm luôn", 2, 6, 2 },
                    { 7, "Tuyệt vời", 2, 7, 2 },
                    { 8, "Nice", 2, 8, 2 }
                });

            migrationBuilder.InsertData(
                table: "Product_Images",
                columns: new[] { "Path_Name", "Product_Id", "SnackBar_Id" },
                values: new object[,]
                {
                    { "chan-ga-sot-thai.jpg", 4, 1 },
                    { "goi-cuon.jpg", 3, 1 },
                    { "tra-chanh-gia-tay.jpg", 7, 2 },
                    { "tra-dao-cam-sa.jpg", 5, 1 },
                    { "Tra-Mang-Cau.png", 8, 2 },
                    { "Wneqo25aPU-ga-ran-01.jpg", 6, 2 }
                });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "SnackBar_Id", "Description", "Ingredient", "Name", "ProductCategory_Id", "Stock", "UnitPrice" },
                values: new object[,]
                {
                    { 9, 3, "Da heo chiên giòn là một món ăn phổ biến trong ẩm thực đường phố Việt Nam, được yêu thích bởi độ giòn rụm và hương vị đặc trưng. Đây là món ăn vặt hoặc có thể dùng làm món khai vị trong các bữa ăn.", "NA", "Da heo chiên giòn", 3, 1, 20 },
                    { 10, 3, "Dừa tắc là một món đồ uống đặc sắc và đầy hấp dẫn của Việt Nam, đặc biệt phổ biến ở miền Nam. Món này kết hợp hương vị béo ngậy của nước dừa tươi với vị chua ngọt của tắc (một loại quả giống quýt hay cam), tạo nên một hương vị độc đáo và sảng khoái.", "NA", "Dừa tắc", 1, 1, 15 },
                    { 11, 4, "Bánh tráng cuộn là một món ăn đường phố phổ biến ở Việt Nam, đặc biệt là ở các tỉnh miền Trung và miền Nam. Món này gồm những lá bánh tráng mỏng được cuộn với một hỗn hợp đa dạng của các nguyên liệu như thịt, rau sống, trứng, và đôi khi là các loại đồ ăn vặt khác như xúc xích, chả, hoặc thậm chí là bánh mì.", "NA", "Bánh tráng cuộn", 3, 1, 20 },
                    { 12, 4, "Bánh tráng trộn là một món ăn vặt rất phổ biến ở Việt Nam, đặc biệt là trong giới trẻ. Món này là sự kết hợp của bánh tráng (lá bánh dẻo mỏng làm từ bột gạo), cùng với một loạt các nguyên liệu đa dạng, mang lại hương vị đặc trưng và thú vị.", "NA", "Bánh tráng trộn", 3, 1, 20 },
                    { 13, 4, "Sinh tố xoài là một trong những loại sinh tố yêu thích ở các nước nhiệt đới, nơi xoài luôn tươi ngon và dồi dào. Món này kết hợp vị ngọt tự nhiên và béo ngậy của xoài chín với sự mát lạnh của đá xay, tạo nên một thức uống giải khát hoàn hảo cho những ngày hè nóng bức.", "NA", "Sinh tố xoài", 2, 1, 15 },
                    { 14, 5, "Gỏi đu đủ là một món salad truyền thống của Việt Nam, đặc biệt phổ biến ở miền Nam. Món này được làm từ đu đủ xanh bào sợi, kết hợp với các nguyên liệu tươi ngon khác như tôm, thịt heo, và rau thơm, rắc lên trên là lạc rang giòn. Gỏi đu đủ được nhiều người yêu thích không chỉ vì hương vị đặc trưng mà còn vì tính giải nhiệt, phù hợp với thời tiết nóng ẩm.", "NA", "Gỏi đu đủ", 3, 1, 20 },
                    { 15, 5, "Cá viên chiên là một món ăn vặt phổ biến và ngon miệng, thường được thưởng thức ở nhiều nơi trên thế giới. Đây là một món ăn dễ làm, với các viên cá được chế biến từ thịt cá tươi ngon, được trộn với gia vị và hình thành thành hình viên tròn hoặc hình que trước khi được chiên giòn.", "NA", "Cá viên chiên", 3, 1, 20 },
                    { 16, 5, "Nước mía là một thức uống truyền thống và rất phổ biến trong nhiều nước trên thế giới, đặc biệt là ở các quốc gia nhiệt đới. Món này được làm từ nước cốt mía tươi ngon, được ép từ cây mía và thường được phục vụ lạnh hoặc có đá.", "NA", "Nước mía", 1, 1, 15 }
                });

            migrationBuilder.InsertData(
                table: "Ratings",
                columns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id", "Star" },
                values: new object[,]
                {
                    { 1, 3, 1, 3.0 },
                    { 1, 4, 1, 5.0 },
                    { 1, 5, 1, 4.0 },
                    { 2, 6, 2, 4.0 },
                    { 2, 7, 2, 3.0 },
                    { 2, 8, 2, 5.0 }
                });

            migrationBuilder.InsertData(
                table: "Product_Images",
                columns: new[] { "Path_Name", "Product_Id", "SnackBar_Id" },
                values: new object[,]
                {
                    { "banh-trang-cuon.jpg", 11, 4 },
                    { "banh-trang-tron.jpg", 12, 4 },
                    { "ca-vien-chien.jpg", 15, 5 },
                    { "da-heo-chien-gion.jpg", 9, 3 },
                    { "dua-tac.jpg", 10, 3 },
                    { "goi-du-du.jpg", 14, 5 },
                    { "nuoc-mia.jpg", 16, 5 },
                    { "sinh-to-xoai.jpg", 13, 4 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "18Arx7CbQe-sinh-to-bo-01.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "banh-trang-cuon.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "banh-trang-tron.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "ca-vien-chien.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "chan-ga-sot-thai.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "da-heo-chien-gion.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "dua-tac.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "goi-cuon.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "goi-du-du.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "MBGvAbfLEI-khoai-tay-chien-01.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "nuoc-mia.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "sinh-to-xoai.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "tra-chanh-gia-tay.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "tra-dao-cam-sa.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "Tra-Mang-Cau.png");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "Wneqo25aPU-ga-ran-01.jpg");

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 1, 3, 1 });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 1, 4, 1 });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 1, 5, 1 });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 2, 6, 2 });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 2, 7, 2 });

            migrationBuilder.DeleteData(
                table: "Ratings",
                keyColumns: new[] { "Customer_Id", "Product_Id", "SnackBar_Id" },
                keyValues: new object[] { 2, 8, 2 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 3, 1 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 4, 1 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 5, 1 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 6, 2 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 7, 2 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 8, 2 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 9, 3 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 10, 3 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 11, 4 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 12, 4 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 13, 4 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 14, 5 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 15, 5 });

            migrationBuilder.DeleteData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 16, 5 });

            migrationBuilder.DeleteData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.AlterColumn<string>(
                name: "CoverImage",
                table: "Snack_Bars",
                type: "nvarchar(256)",
                maxLength: 256,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(256)",
                oldMaxLength: 256,
                oldNullable: true,
                oldDefaultValue: "no-image.jpg");

            migrationBuilder.UpdateData(
                table: "Addresses",
                keyColumn: "Id",
                keyValue: 4,
                column: "Ward_Name",
                value: "Lê Bình");

            migrationBuilder.UpdateData(
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Display_Name", "DOB", "Password", "Username" },
                values: new object[] { "NA", new DateTime(2024, 4, 19, 9, 55, 56, 41, DateTimeKind.Local).AddTicks(3881), "$2a$11$1piWTecWlRdRoyqDV.9s6.9CBLz0.BhHhTZM32NpzoUAa6KgvUWeK", "NA" });

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 1,
                column: "Content",
                value: "NA");

            migrationBuilder.UpdateData(
                table: "Comments",
                keyColumn: "Id",
                keyValue: 2,
                column: "Content",
                value: "NA");

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 4, 19, 9, 55, 55, 886, DateTimeKind.Local).AddTicks(6842), "$2a$11$mZ7lTjj1rKXLiylWysvY/e0nXLvQdqMa6RUlgo7YE/U7sdF5hY10." });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "DOB", "Password" },
                values: new object[] { null, new DateTime(2024, 4, 19, 9, 55, 56, 41, DateTimeKind.Local).AddTicks(3199), "$2a$11$LiQdX3yep8I7tCXDEsauoukYr8QjF5vFP9YgePrjdGlFYpARuVs1." });

            migrationBuilder.InsertData(
                table: "Product_Images",
                columns: new[] { "Path_Name", "Product_Id", "SnackBar_Id" },
                values: new object[,]
                {
                    { "NA", 1, 1 },
                    { "NA2", 2, 2 }
                });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 1, 1 },
                columns: new[] { "Description", "UnitPrice" },
                values: new object[] { "Description", 1 });

            migrationBuilder.UpdateData(
                table: "Products",
                keyColumns: new[] { "Id", "SnackBar_Id" },
                keyValues: new object[] { 2, 2 },
                columns: new[] { "Description", "ProductCategory_Id", "UnitPrice" },
                values: new object[] { "Description", 1, 1 });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Avatar", "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { null, "Description", "snackbar1", new DateTime(2024, 4, 19, 9, 55, 56, 344, DateTimeKind.Local).AddTicks(780), "$2a$11$WlXFQI6XzOhNAeN2bs3i6uc6aB8CsC4C8VI3c0sR/Jc/SGVEf0FBW", "NA" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { null, "Description", "snackbar2", new DateTime(2024, 4, 19, 9, 55, 56, 491, DateTimeKind.Local).AddTicks(3310), "$2a$11$EB42Qhd2fPkUTUY/jpDJ8OnDoI5pa5eZ3JXr38.wpViIwCJifLDYu", "NA" });
        }
    }
}
