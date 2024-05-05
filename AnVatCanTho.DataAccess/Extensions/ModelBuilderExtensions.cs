using AnVatCanTho.Models;
using Microsoft.EntityFrameworkCore;
using System.IO.Pipelines;

namespace AnVatCanTho.DataAccess.Extensions;

public static class ModelBuilderExtensions
{
    private const string NinhKieuDistrict = "Ninh Kiều";
    private const string NKWard1 = "Xuân Khánh";
    private const string NKWard2 = "An Cư";
    private const string CaiRangDistrict = "Cái Răng";
    private const string CRWard1 = "Lê Bình";
    private const string CRWard2 = "Tân Phú";

    // Seed it thoi ong seed nhieu sau sua met lam ma no con roi tui kho doc
    public static ModelBuilder SeedFoundationData(this ModelBuilder builder)
    {
        builder.SeedDistrictsAndWards()
            .SeedCustomerAccounts()
            .SeedAdminAccount()
            .SeedSnackbars()
            .SeedProductsAndRatingsAndComments();
        return builder;
    }

    private static ModelBuilder SeedAdminAccount(this ModelBuilder builder)
    {
        builder.Entity<Role>().HasData(new Role
        {
            Id = 1,
            Name = "Administrator",
            Permission = "all"
        });

        builder.Entity<Administrator>()
            .HasData(new Administrator
            {
                Id = 1,
                City = "NA",
                DisplayName = "Admin",
                Dob = DateTime.Now,
                Email = "admin@site.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                Tel = "NA",
                Username = "admin",
                RoleId = 1,
            });

        return builder;
    }
    private static ModelBuilder SeedCustomerAccounts(this ModelBuilder builder) // cho customer trong so do co address ay ong
    {
        builder.Entity<Customer>()
            .HasData(new Customer
            {
                Id = 1,
                Username = "Nguyễn Văn A",
                Tel = "0123456789",
                Email = "nva@gmail.com",
                Avatar = "user1.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("nva123"),
                Dob = DateTime.Now,
                DisplayName = "NVA"

            },
            new Customer
            {
                Id = 2,
                Username = "Nguyễn Văn B",
                Tel = "0223456789",
                Email = "nvb@gmail.com",
                Avatar = "user2.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("nvb123"),
                Dob = DateTime.Now,
                DisplayName = "NVB"
            });

        builder.Entity<Address>()
            .HasData(new Address
            {
                Id = 1,
                CustomerId = 1,
                DistrictName = NinhKieuDistrict,
                WardName = NKWard1,
                NoAndStreet = "3/2",
            },
            new Address
            {
                Id = 2,
                CustomerId = 2,
                DistrictName = NinhKieuDistrict,
                WardName = NKWard1,
                NoAndStreet = "3/2",
            }
            );

        return builder;
    }
    private static ModelBuilder SeedDistrictsAndWards(this ModelBuilder builder) // nhu cai dong nay tui se gom vao seedCustomer
    {
        var ninhKieuDistr = new District
        {
            Name = NinhKieuDistrict
        };
        var caiRangDistr = new District
        {
            Name = CaiRangDistrict
        };
        var xuanKhanhWard = new Ward
        {
            Name = NKWard1,
            DistrictName = NinhKieuDistrict
        };
        var anCuWard = new Ward
        {
            Name = NKWard2,
            DistrictName = NinhKieuDistrict
        };
        var leBinhWard = new Ward
        {
            Name = CRWard1,
            DistrictName = CaiRangDistrict
        };
        var tanPhuWard = new Ward
        {
            Name = CRWard2,
            DistrictName = CaiRangDistrict
        };
        builder.Entity<District>().HasData(ninhKieuDistr, caiRangDistr);
        builder.Entity<Ward>().HasData(xuanKhanhWard, anCuWard, leBinhWard, tanPhuWard);
        return builder;
    }
    private static ModelBuilder SeedProductsAndRatingsAndComments(this ModelBuilder builder)
    {
        var Drinktype = new ProductType
        {
            Id = 1,
            Name = "Thức uống"
        };
        var FoodType = new ProductType
        {
            Id = 2,
            Name = "Thức ăn"
        };
        builder.Entity<ProductType>().HasData(Drinktype, FoodType);
        var Tea = new ProductCategory
        {
            Id = 1,
            Name = "Trà",
            Description = "Description",
            ProductTypeId = 1
        };
        var Smoothie = new ProductCategory
        {
            Id = 2,
            Name = "Sinh tố",
            Description = "Description",
            ProductTypeId = 1
        };
        var JunkFood = new ProductCategory
        {
            Id = 3, // 
            Name = "Thức ăn nhanh",
            Description = "Description",
            ProductTypeId = 2
        };
        var HealthyFood = new ProductCategory
        {
            Id = 4,
            Name = "Thức ăn lành mạnh",
            Description = "Description",
            ProductTypeId = 2
        };
        builder.Entity<ProductCategory>().HasData(Smoothie, Tea, JunkFood, HealthyFood);
        var AvocadoSmoothie = new Product
        {
            Id = 1,
            Description = "Sinh tố bơ là một đồ uống mát lạnh và bổ dưỡng, được làm từ quả bơ chín mọng, sữa tươi hoặc sữa dừa, đường hoặc mật ong, và một chút đá xay nhuyễn. Đây là một lựa chọn tuyệt vời cho những người yêu thích hương vị độc đáo và chất dinh dưỡng của quả bơ. Sinh tố bơ có màu xanh đậm tự nhiên, với hương thơm béo ngậy của quả bơ kết hợp với sự ngọt ngào của sữa và đường.",
            Ingredient = "NA",
            Name = "Sinh tố bơ",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 1,
            ProductCategoryId = 2
        };
        var FrenchFries = new Product
        {
            Id = 2,
            Description = "Khoai tây chiên là một món ăn phổ biến trên khắp thế giới, được làm từ khoai tây cắt thành dạng que hoặc thanh, sau đó chiên giòn trong dầu. Khoai tây chiên có bề mặt giòn vàng, bên trong mềm mịn, và thường được ăn kèm với sốt cà chua, sốt mayonnaise, hoặc sốt bơ.",
            Ingredient = "NA",
            Name = "Khoai tây chiên",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 2,
            ProductCategoryId = 3
        };
        var SpringRoll = new Product
        {
            Id = 3,
            Description = "Gỏi cuốn, hay còn gọi là spring roll, là một món ăn truyền thống của Việt Nam, được biết đến với vị ngon, hương thơm, và sự tươi mát. Món này thường được làm từ các nguyên liệu tự nhiên và giàu dinh dưỡng như thịt gà hoặc tôm, rau sống, và các loại rau củ khác nhau, được cuốn trong một tờ bánh tráng mềm mịn và mỏng như giấy.",
            Ingredient = "NA",
            Name = "Gỏi cuốn",
            Stock = 1,
            UnitPrice = 10,
            SnackBarId = 1,
            ProductCategoryId = 4
        };
        var ChickenFeetWithThaiSauce = new Product
        {
            Id = 4,
            Description = "Món chân gà sốt Thái là một món ăn hấp dẫn và đậm đà, kết hợp giữa sự ngọt của sốt Thái và độ giòn của chân gà. Món này thường được chế biến bằng cách chiên hoặc nướng chân gà cho đến khi chúng giòn vàng, sau đó được tẩm sốt Thái thơm ngon.",
            Ingredient = "NA",
            Name = "Chân gà sốt thái",
            Stock = 1,
            UnitPrice = 30,
            SnackBarId = 1,
            ProductCategoryId = 3
        };
        var LemongrassOrangePeachTea = new Product
        {
            Id = 5,
            Description = "Trà đào cam sả là một đồ uống tươi mát và sảng khoái, kết hợp giữa hương vị ngọt ngào của đào, chua nhẹ của cam, và hương thơm dễ chịu của sả. Món này thường được làm từ trà đen hoặc trà xanh pha loãng kèm theo nước ép đào, nước cam tươi và một ít nước ép sả.",
            Ingredient = "NA",
            Name = "Trà đào cam sả",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 1,
            ProductCategoryId = 1
        };
        var FriedChicken = new Product
        {
            Id = 6,
            Description = "Gà rán là một món ăn phổ biến trên toàn thế giới, được biết đến với bề mặt ngoài giòn vàng và thịt gà mềm mịn bên trong. Món gà này thường được chế biến bằng cách ngâm trong một hỗn hợp gia vị, sau đó được chiên hoặc nướng cho đến khi thịt chín và bề mặt trở nên giòn và vàng ươm. Gà rán có hương vị đậm đà, béo ngậy từ dầu và gia vị, cùng với vị ngọt tự nhiên của thịt gà.",
            Ingredient = "NA",
            Name = "Gà rán",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 2,
            ProductCategoryId = 3
        };
        var HandPoundedLemonTea = new Product
        {
            Id = 7,
            Description = "Trà chanh giã tay là một đồ uống tươi mát và sảng khoái, được làm từ trà đen hoặc trà xanh pha loãng kèm với chanh và đường, được giã tay hoặc lắc đều để hòa quện hương vị. Đây là một món uống phổ biến, đặc biệt vào mùa hè, khi hương vị chua cay và ngọt ngào của trà chanh giã tay mang lại cảm giác sảng khoái và giải khát.",
            Ingredient = "NA",
            Name = "Trà chanh giã tay",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 2,
            ProductCategoryId = 1
        };
        var SoursopTea = new Product
        {
            Id = 8,
            Description = "Trà mãng cầu là một loại đồ uống tươi mát và bổ dưỡng, được làm từ lá mãng cầu tươi hoặc lá mãng cầu khô, kết hợp với nước sôi và đường hoặc mật ong để tạo ra một hương vị độc đáo và thơm ngon. Món này thường được thưởng thức nóng hoặc lạnh, tùy thuộc vào sở thích cá nhân.",
            Ingredient = "NA",
            Name = "Trà mãng cầu",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 2,
            ProductCategoryId = 1
        };
        var FriedPorkSkin = new Product
        {
            Id = 9,
            Description = "Da heo chiên giòn là một món ăn phổ biến trong ẩm thực đường phố Việt Nam, được yêu thích bởi độ giòn rụm và hương vị đặc trưng. Đây là món ăn vặt hoặc có thể dùng làm món khai vị trong các bữa ăn.",
            Ingredient = "NA",
            Name = "Da heo chiên giòn",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 3,
            ProductCategoryId = 3
        };
        var CoconutKumquat = new Product
        {
            Id = 10,
            Description = "Dừa tắc là một món đồ uống đặc sắc và đầy hấp dẫn của Việt Nam, đặc biệt phổ biến ở miền Nam. Món này kết hợp hương vị béo ngậy của nước dừa tươi với vị chua ngọt của tắc (một loại quả giống quýt hay cam), tạo nên một hương vị độc đáo và sảng khoái.",
            Ingredient = "NA",
            Name = "Dừa tắc",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 3,
            ProductCategoryId = 1
        };
        var RicePaperScroll = new Product
        {
            Id = 11,
            Description = "Bánh tráng cuộn là một món ăn đường phố phổ biến ở Việt Nam, đặc biệt là ở các tỉnh miền Trung và miền Nam. Món này gồm những lá bánh tráng mỏng được cuộn với một hỗn hợp đa dạng của các nguyên liệu như thịt, rau sống, trứng, và đôi khi là các loại đồ ăn vặt khác như xúc xích, chả, hoặc thậm chí là bánh mì.",
            Ingredient = "NA",
            Name = "Bánh tráng cuộn",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 4,
            ProductCategoryId = 3
        };
        var RicePaperMixed = new Product
        {
            Id = 12,
            Description = "Bánh tráng trộn là một món ăn vặt rất phổ biến ở Việt Nam, đặc biệt là trong giới trẻ. Món này là sự kết hợp của bánh tráng (lá bánh dẻo mỏng làm từ bột gạo), cùng với một loạt các nguyên liệu đa dạng, mang lại hương vị đặc trưng và thú vị.",
            Ingredient = "NA",
            Name = "Bánh tráng trộn",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 4,
            ProductCategoryId = 3
        };
        var MangoBlended = new Product
        {
            Id = 13,
            Description = "Sinh tố xoài là một trong những loại sinh tố yêu thích ở các nước nhiệt đới, nơi xoài luôn tươi ngon và dồi dào. Món này kết hợp vị ngọt tự nhiên và béo ngậy của xoài chín với sự mát lạnh của đá xay, tạo nên một thức uống giải khát hoàn hảo cho những ngày hè nóng bức.",
            Ingredient = "NA",
            Name = "Sinh tố xoài",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 4,
            ProductCategoryId = 2
        };
        var PapayaSalad = new Product
        {
            Id = 14,
            Description = "Gỏi đu đủ là một món salad truyền thống của Việt Nam, đặc biệt phổ biến ở miền Nam. Món này được làm từ đu đủ xanh bào sợi, kết hợp với các nguyên liệu tươi ngon khác như tôm, thịt heo, và rau thơm, rắc lên trên là lạc rang giòn. Gỏi đu đủ được nhiều người yêu thích không chỉ vì hương vị đặc trưng mà còn vì tính giải nhiệt, phù hợp với thời tiết nóng ẩm.",
            Ingredient = "NA",
            Name = "Gỏi đu đủ",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 5,
            ProductCategoryId = 3
        };
        var FriedFishBall = new Product
        {
            Id = 15,
            Description = "Cá viên chiên là một món ăn vặt phổ biến và ngon miệng, thường được thưởng thức ở nhiều nơi trên thế giới. Đây là một món ăn dễ làm, với các viên cá được chế biến từ thịt cá tươi ngon, được trộn với gia vị và hình thành thành hình viên tròn hoặc hình que trước khi được chiên giòn.",
            Ingredient = "NA",
            Name = "Cá viên chiên",
            Stock = 1,
            UnitPrice = 20,
            SnackBarId = 5,
            ProductCategoryId = 3
        };
        var SugarcaneJuice = new Product
        {
            Id = 16,
            Description = "Nước mía là một thức uống truyền thống và rất phổ biến trong nhiều nước trên thế giới, đặc biệt là ở các quốc gia nhiệt đới. Món này được làm từ nước cốt mía tươi ngon, được ép từ cây mía và thường được phục vụ lạnh hoặc có đá.",
            Ingredient = "NA",
            Name = "Nước mía",
            Stock = 1,
            UnitPrice = 15,
            SnackBarId = 5,
            ProductCategoryId = 1
        };
        builder.Entity<Product>().HasData(FrenchFries, AvocadoSmoothie, FriedChicken, LemongrassOrangePeachTea,
            SpringRoll, ChickenFeetWithThaiSauce, HandPoundedLemonTea, SoursopTea,
            FriedPorkSkin, CoconutKumquat, RicePaperScroll, RicePaperMixed,
            MangoBlended, PapayaSalad, FriedFishBall, SugarcaneJuice);
        var AvocadoSmoothieImg = new ProductImage
        {
            PathName = "18Arx7CbQe-sinh-to-bo-01.jpg",
            SnackBarId = 1,
            ProductId = 1
        };
        var FrenchFriesImg = new ProductImage
        {
            PathName = "MBGvAbfLEI-khoai-tay-chien-01.jpg",
            SnackBarId = 2,
            ProductId = 2
        };
        var SpringRollImg = new ProductImage
        {
            PathName = "goi-cuon.jpg",
            SnackBarId = 1,
            ProductId = 3
        };
        var ChickenFeetWithThaiSauceImg = new ProductImage
        {
            PathName = "chan-ga-sot-thai.jpg",
            SnackBarId = 1,
            ProductId = 4
        };

        var LemongrassOrangePeachTeaImg = new ProductImage
        {
            PathName = "tra-dao-cam-sa.jpg",
            SnackBarId = 1,
            ProductId = 5
        };
        var FriedChickenImg = new ProductImage
        {
            PathName = "Wneqo25aPU-ga-ran-01.jpg",
            SnackBarId = 2,
            ProductId = 6
        };
        var HandPoundedLemonTeaImg = new ProductImage
        {
            PathName = "tra-chanh-gia-tay.jpg",
            SnackBarId = 2,
            ProductId = 7
        };
        var SoursopTeaImg = new ProductImage
        {
            PathName = "Tra-Mang-Cau.png",
            SnackBarId = 2,
            ProductId = 8
        };
        var FriedPorkSkinImg = new ProductImage
        {
            PathName = "da-heo-chien-gion.jpg",
            SnackBarId = 3,
            ProductId = 9
        };
        var CoconutKumquatImg = new ProductImage
        {
            PathName = "dua-tac.jpg",
            SnackBarId = 3,
            ProductId = 10
        };
        var RicePaperScrollImg = new ProductImage
        {
            PathName = "banh-trang-cuon.jpg",
            SnackBarId = 4,
            ProductId = 11
        };
        var RicePaperMixedImg = new ProductImage
        {
            PathName = "banh-trang-tron.jpg",
            SnackBarId = 4,
            ProductId = 12
        };
        var MangoBlendedImg = new ProductImage
        {
            PathName = "sinh-to-xoai.jpg",
            SnackBarId = 4,
            ProductId = 13
        };
        var PapayaSaladImg = new ProductImage
        {
            PathName = "goi-du-du.jpg",
            SnackBarId = 5,
            ProductId = 14
        };
        var FriedFishBallImg = new ProductImage
        {
            PathName = "ca-vien-chien.jpg",
            SnackBarId = 5,
            ProductId = 15
        };
        var SugarcaneJuiceImg = new ProductImage
        {
            PathName = "nuoc-mia.jpg",
            SnackBarId = 5,
            ProductId = 16
        };
        builder.Entity<ProductImage>().HasData(FrenchFriesImg, AvocadoSmoothieImg, LemongrassOrangePeachTeaImg, SpringRollImg,
            ChickenFeetWithThaiSauceImg, FriedChickenImg, HandPoundedLemonTeaImg, SoursopTeaImg,
            FriedPorkSkinImg, CoconutKumquatImg, RicePaperScrollImg, RicePaperMixedImg,
            MangoBlendedImg, PapayaSaladImg, FriedFishBallImg, SugarcaneJuiceImg);
        var FrenchFriesRating = new Rating
        {
            SnackBarId = 2,
            ProductId = 2,
            CustomerId = 2,
            Star = 4
        };
        var AvocadoSmoothieRating = new Rating
        {
            SnackBarId = 1,
            ProductId = 1,
            CustomerId = 1,
            Star = 3
        };
        var SpringRollRating = new Rating
        {
            SnackBarId = 1,
            ProductId = 3,
            CustomerId = 1,
            Star = 3
        };
        var ChickenFeetWithThaiSauceRating = new Rating
        {
            SnackBarId = 1,
            ProductId = 4,
            CustomerId = 1,
            Star = 5
        };
        var LemongrassOrangePeachTeaRating = new Rating
        {
            SnackBarId = 1,
            ProductId = 5,
            CustomerId = 1,
            Star = 4
        };
        var FriedChickenRating = new Rating
        {
            SnackBarId = 2,
            ProductId = 6,
            CustomerId = 2,
            Star = 4
        };
        var HandPoundedLemonTeaRating = new Rating
        {
            SnackBarId = 2,
            ProductId = 7,
            CustomerId = 2,
            Star = 3
        };
        var SoursopTeaRating = new Rating
        {
            SnackBarId = 2,
            ProductId = 8,
            CustomerId = 2,
            Star = 5
        };
        builder.Entity<Rating>().HasData(AvocadoSmoothieRating, FrenchFriesRating, LemongrassOrangePeachTeaRating, SpringRollRating,
            ChickenFeetWithThaiSauceRating, FriedChickenRating, HandPoundedLemonTeaRating, SoursopTeaRating);
        var AvocadoSmoothieCmt = new Comment
        {
            Id = 1,
            CustomerId = 1,
            SnackBarId = 1,
            ProductId = 1,
            Content = "Ngon nha",
        };
        var FrenchFriesCmt = new Comment
        {
            Id = 2,
            CustomerId = 2,
            SnackBarId = 2,
            ProductId = 2,
            Content = "10 điểm",
        };
        var SpringRollCmt = new Comment
        {
            Id = 3,
            CustomerId = 1,
            SnackBarId = 1,
            ProductId = 3,
            Content = "Hơi bị ngon luôn",
        };
        var ChickenFeetWithThaiSauceCmt = new Comment
        {
            Id = 4,
            CustomerId = 1,
            SnackBarId = 1,
            ProductId = 4,
            Content = "Món này ăn hơi bị dính",
        };
        var LemongrassOrangePeachTeaCmt = new Comment
        {
            Id = 5,
            CustomerId = 1,
            SnackBarId = 1,
            ProductId = 5,
            Content = "Cũng ngon",
        };
        var FriedChickenCmt = new Comment
        {
            Id = 6,
            CustomerId = 2,
            SnackBarId = 2,
            ProductId = 6,
            Content = "Gà giòn rụm luôn",
        };
        var HandPoundedLemonTeaCmt = new Comment
        {
            Id = 7,
            CustomerId = 2,
            SnackBarId = 2,
            ProductId = 7,
            Content = "Tuyệt vời",
        };
        var SoursopTeaCmt = new Comment
        {
            Id = 8,
            CustomerId = 2,
            SnackBarId = 2,
            ProductId = 8,
            Content = "Nice",
        };
        builder.Entity<Comment>().HasData(AvocadoSmoothieCmt, FrenchFriesCmt, LemongrassOrangePeachTeaCmt, SpringRollCmt,
            ChickenFeetWithThaiSauceCmt, FriedChickenCmt, HandPoundedLemonTeaCmt, SoursopTeaCmt);
        return builder;
    }
    private static ModelBuilder SeedSnackbars(this ModelBuilder builder)
    {
        builder.Entity<SnackBar>()
            .HasData(new SnackBar
            {
                Id = 1,
                Username = "snackbar1",
                Tel = "0312345678",
                Email = "snackbar1@site.com",
                Description = "Ghiền là quán ăn vặt Cần Thơ nổi tiếng với những món ăn như: Trà sữa, trà chanh, sinh tố, tàu hũ, khoai tây lắc, bánh tráng trộn… Đây cũng là quán ăn được nhiều du khách ghé thăm khi du lịch Cần Thơ. Sự đa dạng của thực đơn quán kèm với mức giá phải chăng sẽ giúp bạn thỏa sức ăn uống mà không phải lo về chi phí. ",
                Avatar = "avt-quan-ghien.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar1"),
                Dob = DateTime.Now,
                DisplayName = "Quán Ghiền"
            },
            new SnackBar
            {
                Id = 2,
                Username = "snackbar2",
                Tel = "0312345678",
                Email = "snackbar2@site.com",
                Description = "Một lựa chọn ăn vặt bến Ninh Kiều thú vị đó chính là quán Hồi Đó. Không gian quán ở đây được thiết kế theo phong cách thập niên 80 bởi vậy khi đến đây, bạn sẽ có một cảm giác quen thuộc. Ngoài ra, hầu hết đồ dùng, bàn ghế, chén đũa cũng được lựa chọn mang hơi hướng xưa cũ tạo cho du khách những cảm nhận khác lạ. ",
                Avatar = "avt-quan-hoi-do.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar2"),
                Dob = DateTime.Now,
                DisplayName = "Quán Hồi Đó"
            },
            new SnackBar
            {
                Id = 3,
                Username = "snackbar3",
                Tel = "0312345678",
                Email = "snackbar3@site.com",
                Description = "Da heo chiên giòn là một trong những món ăn vặt ở Cần Thơ được nhiều khách hàng yêu thích, đặc biệt là các bạn trẻ. Tại quán dì Lan, bạn không những được ăn món da heo đặc trưng mà còn có thể gọi thêm nhiều món khác như: Xôi chiên thập cẩm, dừa tắc… Điều làm nên sự khác biệt ở món da heo tại quán đó chính là hầu hết nguyên liệu đều được chủ quán tự làm, đảm bảo vệ sinh, mức giá thành cũng rất phù hợp với học sinh, sinh viên. ",
                Avatar = "avt-quan-di-lan.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar3"),
                Dob = DateTime.Now,
                DisplayName = "Da heo chiên giòn Dì Lan"
            },
            new SnackBar
            {
                Id = 4,
                Username = "snackbar4",
                Tel = "0312345678",
                Email = "snackbar4@site.com",
                Description = "Bánh tráng là món ăn vặt quen thuộc của rất nhiều vùng miền khác nhau. Quán bánh tráng cô Hưng là quán ăn vặt Cần Thơ có tên tuổi lâu đời nhất, những món ăn tại đây cũng được chế biến theo hương vị rất đặc trưng. Một vài món ăn hấp dẫn bạn nên thử khi ghé thăm địa điểm này đó chính là: Bánh tráng cuộn sốt mayonnaise, bánh tráng trộn, các loại gỏi,… ",
                Avatar = "avt-quan-co-huong.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar4"),
                Dob = DateTime.Now,
                DisplayName = "Quán bánh tráng cô Hưng"
            },
            new SnackBar
            {
                Id = 5,
                Username = "snackbar5",
                Tel = "0312345678",
                Email = "snackbar5@site.com",
                Description = "Nếu bạn đang tìm kiếm một quán ăn vặt ngon, rẻ ở Cần Thơ thì Gáo Dừa sẽ là một lựa chọn thú vị mà bạn nên tham khảo. Ưu điểm của quán này đó chính là không gian khá thoáng đãng, nhân viên phục vụ hết sức thân thiện. ",
                Avatar = "avt-quan-gao-dua.jpg",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar5"),
                Dob = DateTime.Now,
                DisplayName = "Quán ăn vặt Gáo Dừa"
            }
            );
        builder.Entity<Address>()
            .HasData(new Address
            {
                Id = 3,
                SnackBarId = 1,
                DistrictName = CaiRangDistrict,
                WardName = CRWard1,
                NoAndStreet = "Lý Thái Tổ",
            },
            new Address
            {
                Id = 4,
                SnackBarId = 2,
                DistrictName = CaiRangDistrict,
                WardName = CRWard2,
                NoAndStreet = "Trần Văn Trà"
            },
            new Address
            {
                Id = 5,
                SnackBarId = 3,
                DistrictName = NinhKieuDistrict,
                WardName = NKWard1,
                NoAndStreet = "87 Phạm Ngũ Lão"
            },
            new Address
            {
                Id = 6,
                SnackBarId = 4,
                DistrictName = NinhKieuDistrict,
                WardName = NKWard2,
                NoAndStreet = "8 - 10, Nguyễn Cư Trinh"
            },
            new Address
            {
                Id = 7,
                SnackBarId = 5,
                DistrictName = NinhKieuDistrict,
                WardName = NKWard1,
                NoAndStreet = "45 Lê Lợi"
            }
            );
        return builder;
    }
    private static ModelBuilder SeedOrdersAndBills(this ModelBuilder builder)
    {
        var Order1 = new Order
        {
            Id = 1,
            CustomerId = 1,
            Address = "NA",
            Total = 0,
            Status = true
        };
        var Order2 = new Order
        {
            Id = 2,
            CustomerId = 2,
            Address = "NA",
            Total = 0,
            Status = false
        };
        var OrderDetail1 = new OrderDetail
        {
            SnackBarId = 1,
            ProductId = 1,
            OrderId = 1,
            Quantity = 1,
            Price = 1
        };
        var OrderDetail2 = new OrderDetail
        {
            SnackBarId = 2,
            ProductId = 2,
            OrderId = 2,
            Quantity = 1,
            Price = 1
        };
        var Bill1 = new Bill
        {
            Id = 1,
            OrderId = 1,
            Total = 0,
            Address = "NA"
        };
        var Bill2 = new Bill
        {
            Id = 2,
            OrderId = 2,
            Total = 0,
            Address = "NA"
        };
        builder.Entity<Bill>().HasData(Bill1, Bill2);
        builder.Entity<OrderDetail>().HasData(OrderDetail1, OrderDetail2);
        builder.Entity<Order>().HasData(Order1, Order2);
        return builder;
    }
}