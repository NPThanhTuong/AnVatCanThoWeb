using AnVatCanTho.DataAccess.Data;
using AnVatCanTho.Models;
using AnVatCanThoWeb.Areas.SnackBar.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;


namespace AnVatCanThoWeb.Controllers
{
    public class SnackBarController : Controller
    {
        private readonly ApplicationDbContext _dbContext;

        public SnackBarController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult IndexSnackBar(int snackBarId)
        {
            IEnumerable<SnackBar> snackBarsWithProducts;

            snackBarsWithProducts = _dbContext.SnackBars
                .Include(s => s.Products) // Eager loading for products
                .Where(s => s.Id == snackBarId)
                .ToList();

            return View(snackBarsWithProducts);
        }
    }

}
