using AnVatCanThoWeb.Areas.Admin.Common;
using AnVatCanThoWeb.Common.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Areas.Admin.Controllers;

[Area(AdminAreaName.Value)]
[Authorize(AuthenticationSchemes = ApplicationAuthenticationScheme.AdminScheme)]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}