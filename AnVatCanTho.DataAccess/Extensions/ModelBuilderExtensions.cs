using AnVatCanTho.Models;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanTho.DataAccess.Extensions;

public static class ModelBuilderExtensions
{
    public static ModelBuilder SeedFoundationData(this ModelBuilder builder)
    {
        builder.SeedAdminAccount();
        return builder;
    }
    
    private static ModelBuilder SeedAdminAccount(this ModelBuilder builder)
    {
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
                Username = "NA"
            });
        
        return builder;
    }
}