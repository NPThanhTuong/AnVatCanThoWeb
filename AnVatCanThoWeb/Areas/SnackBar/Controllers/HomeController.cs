using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.SnackBar.Common;

using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Area(SnackbarAreaName.VALUE)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
public class HomeController : Controller
{
    private readonly ApplicationDbContext _db;
    public HomeController(ApplicationDbContext db)
    {
        _db = db;
    }
    [HttpGet]
    public async Task<IActionResult> Index()
    {
        var SnackBarId = User.FindFirst(ClaimTypes.Sid);
        if (SnackBarId == null || !int.TryParse(SnackBarId.Value, out int snackBarId))
        {
            return BadRequest("ID người dùng không hợp lệ.");
        }
        var snackBar = await _db.SnackBars.Include(s => s.Addresses).FirstOrDefaultAsync(s => s.Id == snackBarId);
        if (snackBar == null)
        {
            return NotFound();
        }
        return View(snackBar);
    }
}