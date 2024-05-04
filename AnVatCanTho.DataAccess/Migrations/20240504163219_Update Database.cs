using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AnVatCanTho.DataAccess.Migrations
{
    public partial class UpdateDatabase : Migration
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
                table: "Administrators",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "Display_Name", "DOB", "Password", "Username" },
                values: new object[] { "Admin", new DateTime(2024, 5, 4, 23, 32, 18, 79, DateTimeKind.Local).AddTicks(6531), "$2a$11$UPoZ.Ztsf3CRUflzBtJjCevf/OyT6HMq0OD6qsc/h.9WtRSszadJ.", "admin" });

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
                values: new object[] { "user1.jpg", new DateTime(2024, 5, 4, 23, 32, 17, 894, DateTimeKind.Local).AddTicks(7859), "$2a$11$ejxPo.CKT/J8547sVYno1OpDw.7jPqyP2JrtAKqfShXROBInyeXx6" });

            migrationBuilder.UpdateData(
                table: "Customers",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Avatar", "DOB", "Password" },
                values: new object[] { "user2.jpg", new DateTime(2024, 5, 4, 23, 32, 18, 79, DateTimeKind.Local).AddTicks(5632), "$2a$11$Rz9EkH/5QMa9WFYcnwu1Uu12SPQ2kiN7O2jfu1cigqimLgt0AL6qy" });

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
                columns: new[] { "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "Quán ăn vặt cho sinh viên nổi tiếng bậc nhất Cần Thơ", "Ăn vật Sinh Viên", new DateTime(2024, 5, 4, 23, 32, 18, 477, DateTimeKind.Local).AddTicks(4922), "$2a$11$UuZVLnOEql45FgLyOhhb6ucvPRqSsvSwOyAuR60.zwK98wtMgs4mi", "0312345678" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "Quán ăn vặt hút khách ở Cần Thơ", "Quán Hồi Đó", new DateTime(2024, 5, 4, 23, 32, 18, 657, DateTimeKind.Local).AddTicks(2235), "$2a$11$81lUZ2mTt3JgH4MPdTqyWeDSt2MVLLYPFqhedmmhNpGmR0U2Y8kxu", "0312345678" });

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
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
                keyValue: "chan-ga-sot-thai.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "goi-cuon.jpg");

            migrationBuilder.DeleteData(
                table: "Product_Images",
                keyColumn: "Path_Name",
                keyValue: "MBGvAbfLEI-khoai-tay-chien-01.jpg");

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
                columns: new[] { "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "Description", "snackbar1", new DateTime(2024, 4, 19, 9, 55, 56, 344, DateTimeKind.Local).AddTicks(780), "$2a$11$WlXFQI6XzOhNAeN2bs3i6uc6aB8CsC4C8VI3c0sR/Jc/SGVEf0FBW", "NA" });

            migrationBuilder.UpdateData(
                table: "Snack_Bars",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "Description", "Display_Name", "DOB", "Password", "Tel" },
                values: new object[] { "Description", "snackbar2", new DateTime(2024, 4, 19, 9, 55, 56, 491, DateTimeKind.Local).AddTicks(3310), "$2a$11$EB42Qhd2fPkUTUY/jpDJ8OnDoI5pa5eZ3JXr38.wpViIwCJifLDYu", "NA" });
        }
    }
}
