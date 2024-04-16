using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Common.Authentication;
using AnVatCanThoWeb.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AnVatCanThoWeb.Controllers
{
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext _db;

        public AuthController(ApplicationDbContext db)
        {
            _db = db;
        }

        // GET: /auth/login
        [HttpGet]
        public IActionResult Login(string? ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }

        // POST: /auth/login
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginVm, string? ReturnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(loginVm);
            }

            var user = await _db.Customers
                .SingleOrDefaultAsync(x => x.Email == loginVm.Email);

            if (user is null || !BCrypt.Net.BCrypt.Verify(loginVm.Password, user.Password))
            {
                ViewBag.ThongBao = "Email hoặc mật khẩu không đúng!";
                return View(loginVm);
            }

            var claims = new List<Claim>()
        {
            new Claim(ClaimTypes.Email, loginVm.Email),
            new Claim(ClaimTypes.Role, "User"),
            new Claim(ClaimTypes.Sid, user.Id.ToString()),
        };

            var claimsIdentity = new ClaimsIdentity(claims, ApplicationAuthenticationScheme.UserScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = false,
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                ApplicationAuthenticationScheme.UserScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties
            );

            if (!string.IsNullOrEmpty(ReturnUrl) && Url.IsLocalUrl(ReturnUrl))
            {
                return Redirect(ReturnUrl);
            }
            else
            {
                // Redirect to default page
                return RedirectToAction(actionName: "Index", controllerName: "Home");
            }
        }
    }
}
