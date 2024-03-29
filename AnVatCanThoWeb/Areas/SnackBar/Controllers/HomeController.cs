using Microsoft.AspNetCore.Mvc;

namespace AnVatCanThoWeb.Areas.SnackBar.Controllers;

[Area("SnackBar")]
public class HomeController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}