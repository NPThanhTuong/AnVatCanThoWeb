using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Common.Authentication;
using AnVatCanThoWeb.ViewModels.Auth;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Security.Principal;

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
            new Claim(ClaimTypes.Name, user.DisplayName),
            new Claim(ClaimTypes.UserData, user.Avatar),
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

            // Gán user đã login
            //var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);
            //Thread.CurrentPrincipal = claimsPrincipal;

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
        [HttpGet]
        public IActionResult Register(string? ReturnUrl = null)
        {
            ViewBag.ReturnUrl = ReturnUrl;

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel vm)
        {
            var email = await _db.Customers.SingleOrDefaultAsync(u => u.Email == vm.Email);
            if (email != null)
            {
                ModelState.AddModelError("Email", "Email này đã được đăng ký");
                return View(vm);
            }
            var sdt = await _db.Customers.SingleOrDefaultAsync(u => u.Tel == vm.Tel);
            if (sdt != null)
            {
                ModelState.AddModelError("Tel", "Số điện thoại này đã được đăng ký");
                return View(vm);
            }
            if(ModelState.IsValid)
            {
                var customer = new AnVatCanTho.Models.Customer
                {
                    Username = vm.Username,
                    Tel = vm.Tel,
                    Email = vm.Email,
                    Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                    Dob = (DateTime)vm.Dob,
                    DisplayName = vm.Username
                };
                _db.Customers.Add(customer);
                await _db.SaveChangesAsync();
                ViewBag.RegisterSuccess = true;
                return View(vm);
            }
            return View(vm);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Logout()
        {
            await HttpContext.SignOutAsync(ApplicationAuthenticationScheme.UserScheme);
            return RedirectToAction(actionName: "Index", controllerName: "Home");
        }
    }
}
