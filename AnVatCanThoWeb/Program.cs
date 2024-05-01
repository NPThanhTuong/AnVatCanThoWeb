using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Common;
using AnVatCanThoWeb.Common.Authentication;
using DotnetGeminiSDK;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services.AddControllersWithViews();
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

    builder.Services.Configure<GeminiSettings>(
        builder.Configuration.GetSection(GeminiSettings.SectionName));
    builder.Services.AddGeminiClient(c =>
    {
        using var serviceProvider = builder.Services.BuildServiceProvider();
        var geminiSettings = serviceProvider.GetService<IOptions<GeminiSettings>>()!.Value;
        c.ApiKey = geminiSettings.ApiKey;
        c.TextBaseUrl = geminiSettings.TextBaseUrl;
    });
    
    builder.Services.AddAuthentication(o =>
        {
            o.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,options => // For users
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); // auto logout after 30 mins 
            options.SlidingExpiration = true;
            // options.AccessDeniedPath = "/Forbidden/";
        })
        .AddCookie(ApplicationAuthenticationScheme.AdminScheme, options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            options.LoginPath = new PathString("/Admin/Auth/Login");
        })
        .AddCookie(ApplicationAuthenticationScheme.SnackBarScheme, options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30); 
            options.SlidingExpiration = true;
            options.LoginPath = new PathString("/SnackBar/Auth/Login");
        })
        .AddCookie(ApplicationAuthenticationScheme.UserScheme, options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
            options.SlidingExpiration = true;
            options.LoginPath = new PathString("/Auth/Login");
        });
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddFluentValidationClientsideAdapters();
    builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);
    builder.Services.AddFluentValidationAutoValidation(o =>
    {
        o.DisableDataAnnotationsValidation = true;
    });
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseCookiePolicy(new CookiePolicyOptions
    {
        Secure = CookieSecurePolicy.Always,
        HttpOnly = HttpOnlyPolicy.Always,
        MinimumSameSitePolicy = SameSiteMode.Strict
    });

    app.UseRouting();
    
    app.UseAuthentication();
    app.UseAuthorization();
    
    app.MapControllerRoute(
        name: "AppArea",
        pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");

    app.Run();
}

