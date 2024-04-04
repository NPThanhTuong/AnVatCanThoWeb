using AnVatCanTho.Models;
using Microsoft.EntityFrameworkCore;

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
                DisplayName = "NA",
                Dob = DateTime.Now,
                Email = "admin@site.com",
                Password = BCrypt.Net.BCrypt.HashPassword("Admin123"),
                Tel = "NA",
                Username = "NA",
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
            Description = "Description",
            Ingredient = "NA",
            Name = "Sinh tố bơ",
            Stock = 1,
            UnitPrice = 1,
            SnackBarId = 1,
            ProductCategoryId = 2
        };
        var FrenchFries = new Product
        {
            Id = 2,
            Description = "Description",
            Ingredient = "NA",
            Name = "Khoai tây chiên",
            Stock = 1,
            UnitPrice = 1,
            SnackBarId = 2,
            ProductCategoryId = 1
        };
        builder.Entity<Product>().HasData(FrenchFries, AvocadoSmoothie);
        var AvocadoSmoothieImg = new ProductImage
        {
            PathName = "NA",
            SnackBarId = 1,
            ProductId = 1
        };
        var FrenchFriesImg = new ProductImage
        {
            PathName = "NA2",
            SnackBarId = 2,
            ProductId = 2
        };
        builder.Entity<ProductImage>().HasData(FrenchFriesImg, AvocadoSmoothieImg);
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
        builder.Entity<Rating>().HasData(AvocadoSmoothieRating, FrenchFriesRating);
        var AvocadoSmoothieCmt = new Comment
        {
            Id = 1,
            CustomerId = 1,
            SnackBarId = 1,
            ProductId = 1,
            Content = "NA",
        };
        var FrenchFriesCmt = new Comment
        {
            Id = 2,
            CustomerId = 2,
            SnackBarId = 2,
            ProductId = 2,
            Content = "NA",
        };
        builder.Entity<Comment>().HasData(AvocadoSmoothieCmt, FrenchFriesCmt);
        return builder;
    }
    private static ModelBuilder SeedSnackbars(this ModelBuilder builder)
    {
        builder.Entity<SnackBar>()
            .HasData( new SnackBar
            {
                Id = 1,
                Username = "snackbar1",
                Tel = "NA",
                Email = "snackbar1@site.com",
                Description = "Description",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar1"),
                Dob = DateTime.Now,
                DisplayName = "snackbar1"
            }, 
            new SnackBar
            {
                Id = 2,
                Username = "snackbar2",
                Tel = "NA",
                Email = "snackbar2@site.com",
                Description = "Description",
                Password = BCrypt.Net.BCrypt.HashPassword("snackbar2"),
                Dob = DateTime.Now,
                DisplayName = "snackbar2"
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
                WardName = CRWard1,
                NoAndStreet = "Trần Văn Trà"
            }
            );
        return builder;
    }
    private static ModelBuilder SeedOrdersAndBills (this ModelBuilder builder)
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