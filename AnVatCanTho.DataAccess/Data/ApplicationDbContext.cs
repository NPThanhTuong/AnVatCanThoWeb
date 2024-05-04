using AnVatCanTho.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using AnVatCanTho.DataAccess.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace AnVatCanTho.DataAccess.Data
{
    public partial class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        public virtual DbSet<Address> Addresses { get; set; } = null!;
        public virtual DbSet<Bill> Bills { get; set; } = null!;
        public virtual DbSet<Comment> Comments { get; set; } = null!;
        public virtual DbSet<Customer> Customers { get; set; } = null!;
        public virtual DbSet<District> Districts { get; set; } = null!;
        public virtual DbSet<ProductImage> ProductImages { get; set; } = null!;
        public virtual DbSet<Order> Orders { get; set; } = null!;
        public virtual DbSet<OrderDetail> OrderDetails { get; set; } = null!;
        public virtual DbSet<Product> Products { get; set; } = null!;
        public virtual DbSet<ProductCategory> ProductCategories { get; set; } = null!;
        public virtual DbSet<ProductType> ProductTypes { get; set; } = null!;
        public virtual DbSet<Rating> Ratings { get; set; } = null!;
        public virtual DbSet<SnackBar> SnackBars { get; set; } = null!;
        public virtual DbSet<Ward> Wards { get; set; } = null!;
        public virtual DbSet<Administrator> Administrators { get; set; } = null!;
        public virtual DbSet<Role> Roles { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Address>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "ADDRESS_PK")
                    .IsUnique();
                entity.HasIndex(e => new { e.DistrictName, e.WardName }, "AddressBelongToWard_FK");
                entity.HasIndex(e => e.CustomerId, "CustomerHasAddress_FK");
                entity.HasIndex(e => e.SnackBarId, "SnackBarHasAddress_FK");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.NoAndStreet)
                    .HasMaxLength(128)
                    .HasColumnName("NoAndStreet");
                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .HasColumnName("District_Name");
                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");

                entity.Property(e => e.WardName)
                    .HasMaxLength(50)
                    .HasColumnName("Ward_Name");
            });

            modelBuilder.Entity<Bill>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "BILL_PK")
                    .IsUnique();

                entity.HasIndex(e => e.OrderId, "BillOfOrder_FK");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .HasColumnName("Address");

                entity.Property(e => e.Total).HasColumnName("Total");

                entity.Property(e => e.OrderId).HasColumnName("Order_Id");
            });

            modelBuilder.Entity<Comment>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "COMMENT_PK")
                    .IsUnique();

                entity.HasIndex(e => new { e.SnackBarId, e.ProductId }, "CommentOfProduct_FK");

                entity.HasIndex(e => e.CustomerId, "CustomerHasComment_FK");

                entity.Property(e => e.Id).HasColumnName("Id");

                entity.Property(e => e.Content)
                    .HasMaxLength(1024)
                    .HasColumnName("Content");

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("getdate()")
                    .HasColumnName("CreatedAt");

                entity.Property(e => e.LikeQuantity)
                    .HasDefaultValue(0)
                    .HasColumnName("LikeQuantity");

                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");

                entity.Property(e => e.ProductId).HasColumnName("Product_Id");

                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "CUSTOMER_PK")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Avatar)
                    .HasMaxLength(256)
                    .HasDefaultValue("no-avatar.jpg")
                    .IsRequired(false)
                    .HasColumnName("Avatar");
                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("Email");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("Username");
                entity.Property(e => e.Password)
                    .HasMaxLength(512)
                    .HasColumnName("Password");
                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");
                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tel")
                    .IsFixedLength();
            });

            // Relation: one Customer has one or many Address
            modelBuilder.Entity<Customer>().HasMany(e => e.Addresses)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId);
            // Relation: one Customer has zero or many Orders
            modelBuilder.Entity<Customer>().HasMany(e => e.Orders)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired(false);
            // Relation: one Customer has zero or many Comments
            modelBuilder.Entity<Customer>().HasMany(e => e.Comments)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired(false);
            // Relation: one Customer has zero or many Ratings
            modelBuilder.Entity<Customer>().HasMany(e => e.Ratings)
                .WithOne(e => e.Customer)
                .HasForeignKey(e => e.CustomerId)
                .IsRequired(false);

            modelBuilder.Entity<District>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.HasIndex(e => e.Name, "DISTRICT_PK")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("Name");
            });

            // Relation: one District has one or many Ward
            modelBuilder.Entity<District>().HasMany(e => e.Wards)
                    .WithOne(e => e.District)
                    .HasForeignKey(e => e.DistrictName);

            modelBuilder.Entity<ProductImage>(entity =>
            {
                entity.HasKey(e => e.PathName);

                entity.ToTable("Product_Images");

                entity.HasIndex(e => e.PathName, "PRODUCT_IMAGE_PK")
                    .IsUnique();
                entity.HasIndex(e => new { e.SnackBarId, e.ProductId }, "ProductHasImage_FK");

                entity.Property(e => e.PathName)
                    .HasMaxLength(64)
                    .HasColumnName("Path_Name");
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");
                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");
            });

            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.CustomerId, "CustomerHasOrders_FK");
                entity.HasIndex(e => e.Id, "ORDER_PK")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
                entity.Property(e => e.Address)
                    .HasMaxLength(256)
                    .HasColumnName("Address");
                entity.Property(e => e.Status).HasColumnName("Status");
                entity.Property(e => e.Total).HasColumnName("Total");
            });

            // Relation: one Order has one or many Bills (one-to-one)
            modelBuilder.Entity<Order>().HasMany(e => e.Bills)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId);
            // Relation: one Order has one or many OrderDetails
            modelBuilder.Entity<Order>().HasMany(e => e.OrderDetails)
                .WithOne(e => e.Order)
                .HasForeignKey(e => e.OrderId);

            modelBuilder.Entity<OrderDetail>(entity =>
            {
                entity.HasKey(e => new { e.SnackBarId, e.ProductId, e.OrderId });

                entity.ToTable("Order_Details");

                entity.HasIndex(e => e.OrderId, "OrderDetailOfOrder_FK");
                entity.HasIndex(e => new { e.SnackBarId, e.ProductId }, "OrderDetailOfProduct_FK");

                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");
                entity.Property(e => e.OrderId).HasColumnName("Order_Id");
                entity.Property(e => e.Price).HasColumnName("Price");
                entity.Property(e => e.Quantity).HasColumnName("Quantity");
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => new { e.SnackBarId, e.Id });

                entity.HasIndex(e => new { e.SnackBarId, e.Id }, "PRODUCT_PK")
                    .IsUnique();
                entity.HasIndex(e => e.ProductCategoryId, "ProductOfProductCategory_FK");

                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id");
                entity.Property(e => e.ProductCategoryId).HasColumnName("ProductCategory_Id");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("Description");
                entity.Property(e => e.Ingredient)
                    .HasMaxLength(1024)
                    .HasColumnName("Ingredient");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("Name");
                entity.Property(e => e.Stock).HasColumnName("Stock");
                entity.Property(e => e.UnitPrice).HasColumnName("UnitPrice");
            });

            // Relation: one Product has one or many ProductImages
            modelBuilder.Entity<Product>().HasMany(e => e.ProductImages)
                .WithOne(e => e.Product)
                .HasPrincipalKey(e => new { e.Id, e.SnackBarId })
                .HasForeignKey(e => new { e.ProductId, e.SnackBarId })
                .OnDelete(DeleteBehavior.Restrict);
            // Relation: one Product has zero or many OrderDetails
            modelBuilder.Entity<Product>().HasMany(e => e.OrderDetails)
                .WithOne(e => e.Product)
                .HasPrincipalKey(e => new { e.Id, e.SnackBarId })
                .HasForeignKey(e => new { e.ProductId, e.SnackBarId })
                .IsRequired(false);
            // Relation: one Product has zero or many Ratings
            modelBuilder.Entity<Product>().HasMany(e => e.Ratings)
                .WithOne(e => e.Product)
                .HasPrincipalKey(e => new { e.Id, e.SnackBarId })
                .HasForeignKey(e => new { e.ProductId, e.SnackBarId })
                .IsRequired(false);
            // Relation: one Product has zero or many Comments
            modelBuilder.Entity<Product>().HasMany(e => e.Comments)
                .WithOne(e => e.Product)
                .HasPrincipalKey(e => new { e.Id, e.SnackBarId })
                .HasForeignKey(e => new { e.ProductId, e.SnackBarId })
                .IsRequired(false);

            modelBuilder.Entity<ProductCategory>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Product_Categories");

                entity.HasIndex(e => e.Id, "PRODUCT_CATEGORY_PK")
                    .IsUnique();
                entity.HasIndex(e => e.ProductTypeId, "ProductCategoryOfProductType_FK");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("Description");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("Name");
                entity.Property(e => e.ProductTypeId).HasColumnName("ProductType_Id");
            });

            // Relation: one ProductCategory has one or many Product
            modelBuilder.Entity<ProductCategory>().HasMany(e => e.Products)
                .WithOne(e => e.ProductCategory)
                .HasForeignKey(e => e.ProductCategoryId);

            modelBuilder.Entity<ProductType>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Product_Types");

                entity.HasIndex(e => e.Id, "PRODUCT_TYPE_PK")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("Name");
            });

            // Relation: one ProductType has one or many ProductCaterogy
            modelBuilder.Entity<ProductType>().HasMany(e => e.ProductCategories)
                .WithOne(e => e.ProductType)
                .HasForeignKey(e => e.ProductTypeId);

            modelBuilder.Entity<Rating>(entity =>
            {
                entity.HasKey(e => new { e.SnackBarId, e.ProductId, e.CustomerId });

                entity.HasIndex(e => e.CustomerId, "DetailRatingOfCustomer_FK");
                entity.HasIndex(e => new { e.ProductId, e.SnackBarId }, "DetailRatingofProduct_FK");

                entity.Property(e => e.SnackBarId).HasColumnName("SnackBar_Id");
                entity.Property(e => e.ProductId).HasColumnName("Product_Id");
                entity.Property(e => e.CustomerId).HasColumnName("Customer_Id");
                entity.Property(e => e.Star)
                    .HasDefaultValue(0.0)
                    .HasColumnName("Star");
            });

            modelBuilder.Entity<SnackBar>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("Snack_Bars");

                entity.HasIndex(e => e.Id, "SNACK_BAR_PK")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Avatar)
                    .HasMaxLength(256)
                    .HasDefaultValue("no-avatar.jpg")
                    .IsRequired(false)
                    .HasColumnName("Avatar");
                entity.Property(e => e.CoverImage)
                .HasDefaultValue("no-image.jpg")
                    .IsRequired(false)
                    .HasMaxLength(256)
                    .HasColumnName("CoverImage");
                entity.Property(e => e.Description)
                    .HasMaxLength(1024)
                    .HasColumnName("Description");
                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("Email");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("Username");
                entity.Property(e => e.Password)
                    .HasMaxLength(512)
                    .HasColumnName("Password");
                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");
                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tel")
                    .IsFixedLength();
            });

            // Relation: one SnackBar has one or many Product
            modelBuilder.Entity<SnackBar>().HasMany(e => e.Products)
                    .WithOne(e => e.SnackBar)
                    .HasForeignKey(e => e.SnackBarId);
            // Relation: one SnackBar has zero or many Address
            modelBuilder.Entity<SnackBar>().HasMany(e => e.Addresses)
                .WithOne(e => e.SnackBar)
                .HasForeignKey(e => e.SnackBarId)
                .IsRequired(false);

            modelBuilder.Entity<Ward>(entity =>
            {
                entity.HasKey(e => new { e.DistrictName, e.Name });

                entity.HasIndex(e => new { e.DistrictName, e.Name }, "WARD_PK")
                    .IsUnique();

                entity.Property(e => e.DistrictName)
                    .HasMaxLength(50)
                    .HasColumnName("District_Name");
                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("Name");
            });

            // Relation: one Ward has zero or many Address 
            modelBuilder.Entity<Ward>().HasMany(e => e.Addresses)
                .WithOne(e => e.Ward)
                .HasPrincipalKey(e => new { e.Name, e.DistrictName })
                .HasForeignKey(e => new { e.WardName, e.DistrictName })
                .IsRequired(false);

            modelBuilder.Entity<Administrator>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "ADMINISTRATOR_PK")
                    .IsUnique();
                entity.HasIndex(e => e.RoleId, "AdministratorHasRole_FK");

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Avatar)
                    .HasMaxLength(256)
                    .HasDefaultValue("no-avatar.jpg")
                    .IsRequired(false)
                    .HasColumnName("Avatar");
                entity.Property(e => e.Dob)
                    .HasColumnType("date")
                    .HasColumnName("DOB");
                entity.Property(e => e.Email)
                    .HasMaxLength(50)
                    .HasColumnName("Email");
                entity.Property(e => e.Username)
                    .HasMaxLength(50)
                    .HasColumnName("Username");
                entity.Property(e => e.Password)
                    .HasMaxLength(512)
                    .HasColumnName("Password");
                entity.Property(e => e.DisplayName)
                    .HasMaxLength(50)
                    .HasColumnName("Display_Name");
                entity.Property(e => e.Tel)
                    .HasMaxLength(10)
                    .IsUnicode(false)
                    .HasColumnName("Tel")
                    .IsFixedLength();
                entity.Property(e => e.City)
                    .HasMaxLength(40)
                    .HasColumnName("City");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.HasIndex(e => e.Id, "ROLE_PK")
                    .IsUnique();

                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Name)
                    .HasMaxLength(64)
                    .HasColumnName("Name");
                entity.Property(e => e.Permission)
                    .HasMaxLength(50)
                    .HasColumnName("Permission");
            });

            // Relation: one Role has zero or many Administrator
            modelBuilder.Entity<Role>().HasMany(e => e.Administrators)
                .WithOne(e => e.Role)
                .HasForeignKey(e => e.RoleId)
                .IsRequired(false);

            OnModelCreatingPartial(modelBuilder);

            modelBuilder.SeedFoundationData();
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
