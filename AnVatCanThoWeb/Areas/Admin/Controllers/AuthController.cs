using System.Security.Claims;
using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.Admin.ViewModels.Authentication;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _dbContext;

    public AuthController(ApplicationDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVm)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVm);
        }
        
        var user = await _dbContext.Administrators
            .SingleOrDefaultAsync(x => x.Email == loginVm.Email);
        if (user is null || !BCrypt.Net.BCrypt.Verify(loginVm.Password, user.Password))
        {
            return View(loginVm);
        }

        var claims = new List<Claim>
        {
            new Claim(ClaimTypes.Email, user.Email),
            new Claim(ClaimTypes.Role, "AddLater"), // TODO: Add roles later
            new Claim(ClaimTypes.Sid, user.Id.ToString()), 
        };
        var claimsIdentity = new ClaimsIdentity(claims, ApplicationAuthenticationScheme.AdminScheme);
        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false,
            AllowRefresh = true,
        };
        await HttpContext.SignInAsync(ApplicationAuthenticationScheme.AdminScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties);

        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }

    [HttpGet]
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(ApplicationAuthenticationScheme.AdminScheme);
        return RedirectToAction("Login");
    }
}