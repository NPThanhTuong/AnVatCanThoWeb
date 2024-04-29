using AnVatCanTho.DataAccess.Data;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
[Area(SnackbarAreaName.VALUE)]
public class AccountController : Controller
{
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<ActionResult> Logout()
    {
        await HttpContext.SignOutAsync(ApplicationAuthenticationScheme.SnackBarScheme);
        return RedirectToAction("Index", "Home", new { area = "SnackBar" });
    }
}
