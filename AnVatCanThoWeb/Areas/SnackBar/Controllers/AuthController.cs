using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanThoWeb.Areas.SnackBar.ViewModels.Authentication;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Area(SnackbarAreaName.VALUE)]
public class AuthController : Controller
{
    private readonly ApplicationDbContext _db;

    public AuthController(ApplicationDbContext db)
    {
        _db = db;
    }

    // GET: /snackbar/auth/login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: /snackbar/auth/login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel loginVm)
    {
        if (!ModelState.IsValid)
        {
            return View(loginVm);
        }

        var user = await _db.SnackBars
            .SingleOrDefaultAsync(x => x.Email == loginVm.Email);

        if (user is null || !BCrypt.Net.BCrypt.Verify(loginVm.Password, user.Password))
        {
            ViewData["ThongBao"] = "Email hoặc mật khẩu không đúng!";
            return View(loginVm);
        }

        var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, loginVm.Email),
            new Claim(ClaimTypes.Role, "SnackBar"),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
            new Claim(ClaimTypes.Name, user.DisplayName),
        };

        var claimsIdentity = new ClaimsIdentity(claims, ApplicationAuthenticationScheme.SnackBarScheme);

        var authProperties = new AuthenticationProperties
        {
            IsPersistent = false,
            AllowRefresh = true,
        };

        await HttpContext.SignInAsync(
            ApplicationAuthenticationScheme.SnackBarScheme,
            new ClaimsPrincipal(claimsIdentity),
            authProperties
        );

        return RedirectToAction(actionName: "Index", controllerName: "Home");
    }
    
}
