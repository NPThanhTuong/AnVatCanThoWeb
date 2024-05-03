using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanThoWeb.Areas.SnackBar.ViewModels.Authentication;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using AnVatCanTho.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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
    public async Task<IActionResult> Register()
    {
        var districts = await _db.Districts.ToListAsync();
        var listDistrict = districts
            .Select(x => new SelectListItem(text: x.Name, value: x.Name));
        ViewBag.Districts = listDistrict;
        return View();
    }
    [HttpPost]
    public async Task<ActionResult> Register(CreateAccountViewModel vm)
    {
        var districts = await _db.Districts.ToListAsync();
        var listDistrict = districts
            .Select(x => new SelectListItem(text: x.Name, value: x.Name));
        ViewBag.Districts = listDistrict;
        var email = await _db.SnackBars.SingleOrDefaultAsync(u => u.Email == vm.Email);
        if (email != null)
        {
            ModelState.AddModelError("Email", "Email này đã được đăng ký");
            return View(vm);
        }
        var sdt = await _db.SnackBars.SingleOrDefaultAsync(u => u.Tel == vm.Tel);
        if(sdt != null)
        {
            ModelState.AddModelError("Tel", "Số điện thoại này đã được đăng ký");
            return View(vm);
        }
        if (ModelState.IsValid)
        {
            var snackbar = new AnVatCanTho.Models.SnackBar
            {
                DisplayName = vm.Username,
                Description = "NA",
                Email = vm.Email,
                Tel = vm.Tel,
                Password = BCrypt.Net.BCrypt.HashPassword(vm.Password),
                Dob = (DateTime)vm.Dob,
                Username = vm.Username
            };
            _db.SnackBars.Add(snackbar);
            await _db.SaveChangesAsync();
            if(vm.DistrictName != null) 
            {
                var address = new Address
                {
                    SnackBarId = snackbar.Id,
                    DistrictName = vm.DistrictName,
                    WardName = vm.WardName,
                    NoAndStreet = "NA"
                };
                _db.Addresses.Add(address);
                await _db.SaveChangesAsync();
            }
            ViewBag.RegisterSuccess = true;
            return View(vm);
        }
        return View(vm);
    }

    [HttpGet]
    public async Task<IActionResult> GetWards(string districtName)
    {
        List<Ward> wards = await _db.Wards.Where(w => w.DistrictName == districtName).ToListAsync();
        return Json(wards);
    }
    public async Task<IActionResult> GetDistricts()
    {
        List<District> Districts = await _db.Districts.ToListAsync();
        return Json(Districts);
    }
}
