using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Areas.SnackBar.Common;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Area(SnackbarAreaName.Value)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.SnackBarScheme)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}